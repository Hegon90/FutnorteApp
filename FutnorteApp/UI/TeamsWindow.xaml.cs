using FutnorteApp.BusinessLogic;
using FutnorteApp.DataAccess;
using System;
using System.Windows;

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
            var teamService = new TeamService(new TeamRepository(new FutnorteContext()));
            _teamViewModel = new TeamViewModel(teamService);
            DataContext = _teamViewModel;
            _teamViewModel.SelectedTeam = null;
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            await _teamViewModel.InitializeAsync();
        }

        // Create team
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string teamName = txtTeamName.Text;
                string teamGroup = cboTeamGroup.Text;
                string teamColor = txtTeamColor.Text;
                string teamManager = txtTeamManager.Text;
                string teamPhoneNumber = txtTeamPhoneNumber.Text;
                if (string.IsNullOrEmpty(teamName) )
                {
                    MessageBox.Show("Fallo en el registro.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    _teamViewModel.CreateTeam(teamName, teamGroup, teamColor, teamManager, teamPhoneNumber);
                    MessageBox.Show("Registro Exitoso!", "Registrar", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar equipo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Edit team
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_teamViewModel.SelectedTeam != null)
                {
                    int teamId = _teamViewModel.SelectedTeam.TeamId;
                    string teamName = txtEditTeamName.Text;
                    string teamGroup = cboEditTeamGroup.Text;
                    string teamColor = txtEditTeamColor.Text;
                    string teamManager = txtEditTeamManager.Text;
                    string teamPhoneNumber = txtEditTeamPhoneNumber.Text; 

                    _teamViewModel.UpdateTeam(teamId, teamName, teamGroup, teamColor, teamManager, teamPhoneNumber);
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

        // Clear fields
        public void ClearFields()
        {
            txtTeamName.Text = string.Empty;
            txtTeamColor.Text = string.Empty;
            txtTeamManager.Text = string.Empty;
            txtTeamPhoneNumber.Text = string.Empty;
            cboTeamGroup.Text = "N/A";
        }
    }
}
