using FutnorteApp.BusinessLogic;
using FutnorteApp.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FutnorteApp
{
    internal class MatchViewModel : INotifyPropertyChanged
    {
        private readonly MatchService _matchService;
        public List<DateTime> TimePicker { get; set; }

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

            // Custom TimePicker
            TimePicker = new List<DateTime>();
            for (int i = 6; i < 23; i++)
            {
                TimePicker.Add(new DateTime(1, 1, 1, i, 0, 0));
                TimePicker.Add(new DateTime(1, 1, 1, i, 15, 0));
                TimePicker.Add(new DateTime(1, 1, 1, i, 30, 0));
                TimePicker.Add(new DateTime(1, 1, 1, i, 45, 0));
            }
        }

        // Load data async
        public async Task InitializeAsync()
        {            
            await LoadTeamsAsync();
            await LoadRoundsAsync();
            await LoadFieldsAsync();
            await LoadMatchesAsync();
        }

        // Get match by Id
        public Match? GetMatchById(int matchId)
        {
            return _matchService.GetMatchById(matchId);
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

        // Add new match
        public void AddMatch(Match newMatch)
        {
            _matchService.AddMatch(newMatch);
            Matches.Add(newMatch);
        }

        // Update existing match
        public void UpdateMatch(Match match)
        {
            _matchService.UpdateMatch(match);
        }

        // Delete match by Id
        public void DeleteMatch(int matchId)
        {
            MessageBoxResult result = MessageBox.Show("Esta seguro de eliminar este partido?", "Confirmation", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _matchService.DeleteMatch(matchId);
                // Find and remove the deleted match from the Matches collection
                Match? match = Matches.FirstOrDefault(m => m.MatchId == matchId);
                if (match != null)
                {
                    Matches.Remove(match);
                }
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

        // Get the values of the selected row in datagrids
        private Match? _selectedMatch;
        public Match? SelectedMatch
        {
            get { return _selectedMatch; }
            set
            {
                _selectedMatch = value;
                OnPropertyChanged();
            }
        }

        // OnPropertyChanged Method
        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
