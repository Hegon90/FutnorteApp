using FutnorteApp.BusinessLogic;
using FutnorteApp.DataAccess;
using FutnorteApp.Domain;
using FutnorteApp.ViewModel;
using System;
using System.Windows;

namespace FutnorteApp.UI
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        private readonly ResultViewModel _resultViewModel;
        public ResultsWindow()
        {
            InitializeComponent();
            var futnorteContext = new FutnorteContext();
            var roundRepository = new RoundRepository(futnorteContext);
            var matchRepository = new MatchRepository(futnorteContext);
            var teamRepository = new TeamRepository(futnorteContext);
            var resultRepository = new ResultRepository(futnorteContext);
            var resultService = new ResultService(resultRepository, roundRepository, matchRepository, teamRepository);
            _resultViewModel = new ResultViewModel(resultService);
            DataContext = _resultViewModel;
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            await _resultViewModel.InitializeAsync();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
