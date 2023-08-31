using FutnorteApp.Domain;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Text;
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

            // Retrieve match details based on matchId
            var selectedMatch = _matchViewModel.SelectedMatch;
            if (selectedMatch != null)
            {
                // Find the item in the ComboBox's list of items that corresponds to the time of day
                cbMatchTime.ItemsSource = _matchViewModel.TimePicker;
                var timeOfDay = ((DateTime)selectedMatch.MatchDateTime).TimeOfDay;
                var selectedTime = cbMatchTime.Items.Cast<DateTime>().FirstOrDefault(dt => dt.TimeOfDay == timeOfDay);

                // Set the SelectedItem property to the found item
                dpMatchDate.SelectedDate = selectedMatch.MatchDateTime;
                cbMatchTime.SelectedItem = selectedTime;
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
            // Retrieve updated match properties from UI elements
            DateTime? matchDateTime = dpMatchDate.SelectedDate;
            Round? round = cbRound.SelectedItem as Round;
            Team? homeTeam = cbHomeTeam.SelectedItem as Team;
            Team? awayTeam = cbAwayTeam.SelectedItem as Team;
            Field? field = cbField.SelectedItem as Field;

            if (homeTeam != null && awayTeam != null)
            {
                // Find the existing match entity within the Matches collection
                Match? existingMatch = _matchViewModel.Matches.FirstOrDefault(match => match.MatchId == _selectedMatchId);

                if (existingMatch != null)
                {
                    // Update the existing match properties
                    existingMatch.MatchDateTime = matchDateTime;
                    existingMatch.RoundId = round?.RoundId;
                    existingMatch.HomeTeamId = homeTeam.TeamId;
                    existingMatch.AwayTeamId = awayTeam.TeamId;
                    existingMatch.FieldId = field?.FieldId;

                    // Update the match in the ViewModel
                    _matchViewModel.UpdateMatch(existingMatch);
                    await _matchViewModel.LoadMatchesAsync();
                    Close();
                    MessageBox.Show("Partido editado!", "Editar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

    }
}



