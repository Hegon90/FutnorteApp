using FutnorteApp.BusinessLogic;
using FutnorteApp.DataAccess;
using System;
using System.Windows;

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
    }
}


