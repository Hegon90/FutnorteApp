using FutnorteApp.UI;
using System.Windows;

namespace FutnorteApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TeamsWindow_Click(object sender, RoutedEventArgs e)
        {
            var teamsWindow = new TeamsWindow();
            teamsWindow.Show();
        }

        private void MatchesWindow_Click(object sender, RoutedEventArgs e)
        {
            var matchesWindow = new MatchesWindow();
            matchesWindow.Show();
        }
    }
}

