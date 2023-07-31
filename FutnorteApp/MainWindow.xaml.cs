using FutnorteApp.BusinessLogic;
using FutnorteApp.DataAccess;
using FutnorteApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace FutnorteApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TeamViewModel _teamViewModel;
        public MainWindow()
        {
            InitializeComponent();

            //Code to show database content in messagebox

            //_teamViewModel = new TeamViewModel();
            //_teamViewModel.LoadDataAsync();
            //string message = "Teams:\n";
            //foreach (var team in _teamViewModel.Teams)
            //{
            //    message += $"{team.TeamName}\n";
            //}
            //MessageBox.Show(message, "Teams List", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}

