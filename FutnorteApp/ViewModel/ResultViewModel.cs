using FutnorteApp.BusinessLogic;
using FutnorteApp.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FutnorteApp.ViewModel
{
    internal class ResultViewModel: INotifyPropertyChanged
    {
        private readonly ResultService _resultService;

        public ResultViewModel(ResultService resultService)
        {
            _resultService = resultService;
            _results = new ObservableCollection<Result>();
            Results = _results;
            _matches = new ObservableCollection<Match>();
            Matches = _matches;
        }

        // Load data async
        public async Task InitializeAsync()
        {
            await LoadMatchesAsync();
            await LoadResultsAsync();
        }

        // Get result by Id
        public Result? GetResultById(int resultId)
        {
            return _resultService.GetResultById(resultId);
        }

        // Get the results list
        private ObservableCollection<Result> _results;
        public ObservableCollection<Result> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged();
            }
        }

        // List of results
        public CollectionViewSource ResultsViewSource { get; } = new CollectionViewSource();
        public async Task LoadResultsAsync()
        {
            try
            {
                var results = await _resultService.GetAllResults();
                Results = new ObservableCollection<Result>(results);
                ResultsViewSource.Source = Results;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar los resultados: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Add new result
        public void AddResult(Result newResult)
        {
            _resultService.AddResult(newResult);
            Results.Add(newResult);
        }

        // Update existing result
        public void UpdateResult(Result result)
        {
            _resultService.UpdateResult(result);
        }

        // Delete result by Id
        public void DeleteResult(int resultId)
        {
            MessageBoxResult message = MessageBox.Show("Esta seguro de eliminar este resultado?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                _resultService.DeleteResult(resultId);
                Result? result = Results.FirstOrDefault(m => m.ResultId == resultId);
                if (result != null)
                {
                    Results.Remove(result);
                }
            }
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

        public async Task LoadMatchesAsync()
        {
            try
            {
                var matches = await _resultService.GetAllMatches();
                Matches = new ObservableCollection<Match>(matches);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar los partidos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Get the values of the selected row in datagrids
        private Result? _selectedResult;
        public Result? SelectedResult
        {
            get { return _selectedResult; }
            set
            {
                _selectedResult = value;
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
