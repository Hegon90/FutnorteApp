using FutnorteApp.BusinessLogic;
using FutnorteApp.DataAccess;
using FutnorteApp.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FutnorteApp
{
    internal class TeamViewModel : INotifyPropertyChanged
    {
        private readonly TeamService _teamService;

        public TeamViewModel(TeamService teamService)
        {
            _teamService = teamService;
            _teams = new ObservableCollection<Team>();
            Teams = _teams;
        }


        public async Task InitializeAsync()
        {
            await LoadDataAsync();
        }

        public ObservableCollection<Team> _teams;
        public ObservableCollection<Team> Teams
        {
            get { return _teams; }
            set
            {
                _teams = value;
                OnPropertyChanged();
            }
        }

        //Create the collections to display the teams filtered by group 
        public CollectionViewSource GroupAViewSource { get; } = new CollectionViewSource();
        public CollectionViewSource GroupBViewSource { get; } = new CollectionViewSource();
        public CollectionViewSource NoGroupViewSource { get; } = new CollectionViewSource();
        public async Task LoadDataAsync()
        {
            try
            {
                var teams = await _teamService.GetAllTeams();
                Teams = new ObservableCollection<Team>(teams);

                GroupAViewSource.Source = Teams;
                GroupBViewSource.Source = Teams;
                NoGroupViewSource.Source = Teams;
                GroupAViewSource.View.Filter = item => ((Team)item).TeamGroup == "A";
                GroupBViewSource.View.Filter = item => ((Team)item).TeamGroup == "B";
                NoGroupViewSource.View.Filter = item => ((Team)item).TeamGroup == "N/A";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Create new team
        public void CreateTeam(string teamName, string teamGroup, string teamColor, string teamManager, string teamPhoneNumber)
        {
            var newTeam = new Team
            {
                TeamName = teamName,
                TeamGroup = teamGroup,
                TeamColor = teamColor,
                TeamManager = teamManager,
                TeamPhoneNumber = teamPhoneNumber
            };

            _teamService.AddTeam(newTeam);
            Teams.Add(newTeam);
            UpdateTeamGroups();
        }

        // Update team
        public void UpdateTeam(int teamId, string teamName, string teamGroup, string teamColor, string teamManager, string teamPhoneNumber)
        {
            var existingTeam = Teams.FirstOrDefault(t => t.TeamId == teamId);
            if (existingTeam != null)
            {
                existingTeam.TeamName = teamName;
                existingTeam.TeamGroup = teamGroup;
                existingTeam.TeamColor = teamColor;
                existingTeam.TeamManager = teamManager;
                existingTeam.TeamPhoneNumber = teamPhoneNumber;
                _teamService.UpdateTeam(existingTeam);
                UpdateTeamGroups();
            }
            else
            {
                MessageBox.Show("Seleccione un equipo.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Delete Team
        public void DeleteTeam(int teamId)
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Get the values of the selected row in datagrids
        private Team? _selectedTeam;
        public Team? SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {
                _selectedTeam = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> GroupOptions { get; } = new ObservableCollection<string> {"A", "B", "N/A" };

        // Update CollectionViewSources
        public void UpdateTeamGroups()
        {
            GroupAViewSource.View.Refresh();
            GroupBViewSource.View.Refresh();
            NoGroupViewSource.View.Refresh();
        }
    }
}

