using System;
using System.Collections.Generic;
using WorkoutTimerApp.ViewModels;
using WorkoutTimerApp.Views;
using Xamarin.Forms;

namespace WorkoutTimerApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AMRAPTimerPage), typeof(AMRAPTimerPage));        
            Routing.RegisterRoute(nameof(BasicTimerPage), typeof(BasicTimerPage));
            Routing.RegisterRoute(nameof(EMOMTimer), typeof(EMOMTimer));
            Routing.RegisterRoute(nameof(TabataTimerPage), typeof(TabataTimerPage));
            Routing.RegisterRoute(nameof(WorkoutLogsPage), typeof(WorkoutLogsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
