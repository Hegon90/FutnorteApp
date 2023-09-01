using FutnorteApp.Domain;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows;

namespace FutnorteApp.UI
{
    /// <summary>
    /// Interaction logic for EditMatchWindow.xaml
    /// </summary>
    public partial class EditMatchWindow : Window
    {
        private readonly MatchViewModel _matchViewModel;
        private int _selectedMatchId;
        internal EditMatchWindow(int matchId, MatchViewModel matchViewModel)
        {
            InitializeComponent();
            _matchViewModel = matchViewModel;
            DataContext = _matchViewModel;
            _selectedMatchId = matchId;

            Match? selectedMatch = _matchViewModel.SelectedMatch;
            if (selectedMatch != null)
            {
                // Find the item in the ComboBox's list of items that corresponds to the time of day
                cbMatchTime.ItemsSource = _matchViewModel.TimePicker;

                // Check if selectedMatch.MatchDateTime is not null before accessing its properties
                var timeOfDay = selectedMatch.MatchDateTime?.TimeOfDay;

                if (timeOfDay != null)
                {
                    DateTime selectedTime = cbMatchTime.Items.Cast<DateTime>().FirstOrDefault(dt => dt.TimeOfDay == timeOfDay);
                    dpMatchDate.SelectedDate = selectedMatch.MatchDateTime;
                    cbMatchTime.SelectedItem = selectedTime;
                }

                cbHomeTeam.SelectedItem = selectedMatch.HomeTeam;
                cbHomeTeam.DisplayMemberPath = "TeamName";
                cbAwayTeam.SelectedItem = selectedMatch.AwayTeam;
                cbAwayTeam.DisplayMemberPath = "TeamName";
                cbRound.SelectedItem = selectedMatch.Round;
                cbRound.DisplayMemberPath = "RoundName";
                cbField.SelectedItem = selectedMatch.Field;
                cbField.DisplayMemberPath = "FieldName";
            }

        }


        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            await _matchViewModel.InitializeAsync();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
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
                // Find the existing match entity within the Matches collection
                Match? existingMatch = _matchViewModel.Matches.FirstOrDefault(match => match.MatchId == _selectedMatchId);

                if (existingMatch != null)
                {
                    UpdateMatchProperties(existingMatch, matchDateTime.Value, round.RoundId, homeTeam.TeamId, awayTeam.TeamId, field?.FieldId);
                    if (existingMatch.RoundId == 1)
                    {
                        existingMatch.MatchDateTime = null;
                        existingMatch.FieldId = null;
                    }
                    _matchViewModel.UpdateMatch(existingMatch);
                    await _matchViewModel.LoadMatchesAsync();
                    Close();
                    MessageBox.Show("Partido editado!", "Editar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBox.Show("Fallo en la edición.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void UpdateMatchProperties(Match existingMatch, DateTime matchDateTime, int roundId, int homeTeamId, int awayTeamId, int? fieldId)
        {
            existingMatch.MatchDateTime = matchDateTime;
            existingMatch.RoundId = roundId;
            existingMatch.HomeTeamId = homeTeamId;
            existingMatch.AwayTeamId = awayTeamId;
            existingMatch.FieldId = fieldId;
        }
    }
}



