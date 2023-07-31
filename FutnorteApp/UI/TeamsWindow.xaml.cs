using System.Windows;
using System.Windows.Controls;

namespace FutnorteApp.UI
{
    /// <summary>
    /// Interaction logic for TeamsWindow.xaml
    /// </summary>
    public partial class TeamsWindow : Window
    {
        public TeamsWindow()
        {
            InitializeComponent();
            DataContext = new TeamViewModel();
        }
    }
}
