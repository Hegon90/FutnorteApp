using System;
using System.Windows;
using System.Windows.Controls;

namespace FutnorteApp.UI
{
    /// <summary>
    /// Interaction logic for TeamsWindow.xaml
    /// </summary>
    public partial class TeamsWindow : Window
    {
        private readonly TeamViewModel _teamViewModel;
        public TeamsWindow()
        {
            InitializeComponent();
            DataContext = new TeamViewModel();
            _teamViewModel = new TeamViewModel();
            DataContext = _teamViewModel;
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            await _teamViewModel.InitializeAsync();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string teamName = txtTeamName.Text;
                string teamGroup = cboTeamGroup.Text;
                if (string.IsNullOrEmpty(teamName) )
                {
                    MessageBox.Show("Fallo en el registro.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    _teamViewModel.CreateTeam(teamName, teamGroup);
                    MessageBox.Show("Registro Exitoso!", "Registrar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear equipo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_teamViewModel.SelectedTeam != null)
                {
                    int teamId = _teamViewModel.SelectedTeam.TeamId;
                    string teamName = txtEditTeamName.Text;
                    string teamGroup = cboEditTeamGroup.Text;

                    _teamViewModel.UpdateTeam(teamId, teamName, teamGroup);
                    MessageBox.Show("Equipo Editado!", "Editar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Edicion NO completada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
