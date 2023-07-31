using FutnorteApp.BusinessLogic;
using FutnorteApp.DataAccess;
using FutnorteApp.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace FutnorteApp
{
    internal class TeamViewModel : INotifyPropertyChanged
    {
        // Access to the below slayer to get the Team's methods
        private readonly TeamService _teamService;

        public TeamViewModel()
        {
            // Inject the TeamService into the ViewModel
            _teamService = new TeamService(new TeamRepository(new FutnorteContext()));
            Teams = new ObservableCollection<Team>();
            LoadDataAsync();
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

        // Load teams asynchronously from the database
        public async Task LoadDataAsync()
        {
            try
            {
                var teams = await _teamService.GetAllTeams();
                Teams = new ObservableCollection<Team>(teams);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Implement the INotifyPropertyChanged interface to notify the UI of property changes
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
