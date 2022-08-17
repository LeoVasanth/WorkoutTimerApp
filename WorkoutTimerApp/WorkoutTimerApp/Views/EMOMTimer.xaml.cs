using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTimerApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutTimerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EMOMTimer : ContentPage
    {
        public EMOMTimer()
        {
            InitializeComponent();
            BindingContext = new EMOMTimerViewModel();
        }
    }
}