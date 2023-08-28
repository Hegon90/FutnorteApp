using FutnorteApp.BusinessLogic;
using FutnorteApp.DataAccess;
using FutnorteApp.Domain;
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
        public EditMatchWindow(int matchId)
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
            _selectedMatchId = matchId;

            // Retrieve match details based on matchId
            var selectedMatch = _matchViewModel.GetMatchById(matchId);
            if (selectedMatch != null)
            {
                dpMatchDate.SelectedDate = selectedMatch.MatchDate;
                cbMatchTime.SelectedItem = selectedMatch.MatchTime;
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve updated match properties from UI elements
            DateTime? matchDate = dpMatchDate.SelectedDate;
            string? matchTime = cbMatchTime.SelectedItem as string;
            Round? round = cbRound.SelectedItem as Round;
            Team? homeTeam = cbHomeTeam.SelectedItem as Team;
            Team? awayTeam = cbAwayTeam.SelectedItem as Team;
            Field? field = cbField.SelectedItem as Field;

            if (homeTeam != null && awayTeam != null)
            {
                // Create a new Match instance with updated properties
                Match updatedMatch = new Match
                {
                    MatchId = _selectedMatchId,
                    MatchDate = matchDate,
                    MatchTime = matchTime,
                    RoundId = round?.RoundId,
                    HomeTeamId = homeTeam.TeamId,
                    AwayTeamId = awayTeam.TeamId,
                    FieldId = field?.FieldId
                };
                _matchViewModel.UpdateMatch(updatedMatch);
            }
            
            // Close the EditMatchWindow
            Close();
        }
    }
}



