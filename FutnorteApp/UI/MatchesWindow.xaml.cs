using FutnorteApp.BusinessLogic;
using FutnorteApp.DataAccess;
using FutnorteApp.Domain;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

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
                    _matchViewModel.AddMatch(matchDate.Value, matchTime, round.RoundId, homeTeam.TeamId, awayTeam.TeamId, field.FieldId);
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
    }
}


