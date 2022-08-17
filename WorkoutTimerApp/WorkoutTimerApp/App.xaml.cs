using System;
using WorkoutTimerApp.Services;
using WorkoutTimerApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutTimerApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
