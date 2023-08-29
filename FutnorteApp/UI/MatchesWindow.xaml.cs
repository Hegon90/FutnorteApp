using FutnorteApp.BusinessLogic;
using FutnorteApp.DataAccess;
using FutnorteApp.Domain;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace FutnorteApp.UI
{
    /// <summary>
    /// Interaction logic for MatchesWindow.xaml
    /// </summary>
    public partial class MatchesWindow : Window
    {
        private readonly MatchViewModel _matchViewModel;
        public MatchesWindow()
        {
            InitializeComponent();
            var futnorteContext = new FutnorteContext();
            var matchRepository = new MatchRepository(futnorteContext);
            var roundRepository = new RoundRepository(futnorteContext);
            var teamRepository = new TeamRepository(futnorteContext);
            var fieldRepository = new FieldRepository(futnorteContext);
            var matchService = new MatchService(matchRepository, roundRepository, teamRepository, fieldRepository);
            _matchViewModel = new MatchViewModel(matchService);
            DataContext = _matchViewModel;
            _matchViewModel.SelectedMatch = null;
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            await _matchViewModel.InitializeAsync();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? matchDate = dpMatchDate.SelectedDate;
                Round? round = cbRound.SelectedItem as Round;
                Team? homeTeam = cbHomeTeam.SelectedItem as Team;
                Team? awayTeam = cbAwayTeam.SelectedItem as Team;
                Field? field = cbField.SelectedItem as Field;
                string? matchTime = cbMatchTime.SelectedItem as string;
                if (matchDate != null && round != null && homeTeam != null && awayTeam != null && field != null)
                {
                    var newMatch = new Match
                    {
                        MatchDate = matchDate,
                        MatchTime = matchTime,
                        RoundId = round.RoundId,
                        HomeTeamId = homeTeam.TeamId,
                        AwayTeamId = awayTeam.TeamId,
                        FieldId = field.FieldId
                    };
                    _matchViewModel.AddMatch(newMatch);
                    MessageBox.Show("Registro Exitoso!", "Registrar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Fallo en el registro.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar partido: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditMatchButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedMatch = _matchViewModel.SelectedMatch;
            if (selectedMatch != null)
            {
                int matchId = selectedMatch.MatchId;

                var editMatchWindow = new EditMatchWindow(matchId, _matchViewModel);
                editMatchWindow.ShowDialog();
            }
        }

        private void DeleteMatchButton_Click(Object sender, RoutedEventArgs e)
        {
            var matchToDelete = _matchViewModel.SelectedMatch;
            if (matchToDelete != null)
            {
                int matchId = matchToDelete.MatchId;
                _matchViewModel.DeleteMatch(matchId);
            }
            else
            {
                MessageBox.Show("Seleccione un partido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

