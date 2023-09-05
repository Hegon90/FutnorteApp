using FutnorteApp.BusinessLogic;
using FutnorteApp.DataAccess;
using FutnorteApp.Domain;
using System;
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
            var resultRepository = new ResultRepository(futnorteContext);
            var matchService = new MatchService(matchRepository, roundRepository, teamRepository, fieldRepository);
            var resultService = new ResultService(resultRepository, roundRepository, matchRepository, teamRepository);
            _matchViewModel = new MatchViewModel(matchService, resultService);
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
                // Configure necessary fields
                Round? round = cbRound.SelectedItem as Round;
                Team? homeTeam = cbHomeTeam.SelectedItem as Team;
                Team? awayTeam = cbAwayTeam.SelectedItem as Team;
                
                // Validate if cbRound, cbHomeTeam, and cbAwayTeam are empty
                if (round == null || homeTeam == null || awayTeam == null)
                {
                    MessageBox.Show("Debe seleccionar minimo: Ronda, Equipo Local y Equipo Visitante.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Rest of the fields
                Field? field = cbField.SelectedItem as Field;
                DateTime? matchDateTime = DateTime.Now;
                if (dpMatchDate.SelectedDate.HasValue && cbMatchTime.SelectedItem != null)
                {
                    matchDateTime = dpMatchDate.SelectedDate.Value.Date + ((DateTime)cbMatchTime.SelectedItem).TimeOfDay;
                }

                if (round != null && homeTeam != null && awayTeam != null && homeTeam != awayTeam)
                {
                    var newMatch = new Match
                    {
                        MatchDateTime = matchDateTime,
                        RoundId = round.RoundId,
                        HomeTeamId = homeTeam.TeamId,
                        AwayTeamId = awayTeam.TeamId,
                        FieldId = field?.FieldId
                    };
                    // Validate APLAZADO match
                    if(newMatch.RoundId == 1)
                    {
                        newMatch.MatchDateTime = null;
                        newMatch.FieldId = null;
                    }
                    _matchViewModel.AddMatch(newMatch);

                    // Add match result
                    int resultId = newMatch.MatchId;
                    var matchResult = new Result
                    {
                        ResultId = resultId,
                        MatchId = newMatch.MatchId,
                    };
                    _matchViewModel.AddResult(matchResult);
                    MessageBox.Show("Registro Exitoso!", "Registrar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Fallo en el registro.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                string message = $"Error al registrar partido: {ex.Message}";
                if (ex.InnerException != null)
                {
                    message += $"\nInner Exception: {ex.InnerException.Message}";
                }
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

