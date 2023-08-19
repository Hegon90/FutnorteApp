using FutnorteApp.BusinessLogic;
using FutnorteApp.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FutnorteApp
{
    internal class MatchViewModel : INotifyPropertyChanged
    {
        private readonly MatchService _matchService;
        public MatchViewModel(MatchService matchService)
        {
            _matchService = matchService;
            _matches = new ObservableCollection<Match>();
            Matches = _matches;
            _teams = new ObservableCollection<Team>();
            Teams = _teams;
            _rounds = new ObservableCollection<Round>();
            Rounds = _rounds;
            _fields = new ObservableCollection<Field>();
            Fields = _fields;
        }

        // Load data async
        public async Task InitializeAsync()
        {            
            await LoadTeamsAsync();
            await LoadRoundsAsync();
            await LoadFieldsAsync();
            await LoadMatchesAsync();
        }

        // Get the matches list
        private ObservableCollection<Match> _matches;
        public ObservableCollection<Match> Matches
        {
            get { return _matches; }
            set
            {
                _matches = value;
                OnPropertyChanged();
            }
        }

        // List of matches 
        public CollectionViewSource MatchesViewSource { get; } = new CollectionViewSource();
        public async Task LoadMatchesAsync()
        {
            try
            {
                var matches = await _matchService.GetAllMatches();
                Matches = new ObservableCollection<Match>(matches);
                MatchesViewSource.Source = Matches;
            }
            catch(System.Exception ex)
            {
                MessageBox.Show($"Error al cargar los partidos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Get the teams list
        private ObservableCollection<Team> _teams;
        public ObservableCollection<Team> Teams
        {
            get { return _teams; }
            set
            {
                _teams = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadTeamsAsync()
        {
            try
            {
                var teams = await _matchService.GetAllTeams();
                Teams = new ObservableCollection<Team>(teams);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar los equipos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Get the rounds list
        private ObservableCollection<Round> _rounds;
        public ObservableCollection<Round> Rounds
        {
            get { return _rounds; }
            set
            {
                _rounds = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadRoundsAsync()
        {
            try
            {
                var rounds = await _matchService.GetAllRounds();
                Rounds = new ObservableCollection<Round>(rounds);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar las rondas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Get the fields list
        private ObservableCollection<Field> _fields;
        public ObservableCollection<Field> Fields
        {
            get { return _fields; }
            set
            {
                _fields = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadFieldsAsync()
        {
            try
            {
                var fields = await _matchService.GetAllFields();
                Fields = new ObservableCollection<Field>(fields);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar las canchas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
