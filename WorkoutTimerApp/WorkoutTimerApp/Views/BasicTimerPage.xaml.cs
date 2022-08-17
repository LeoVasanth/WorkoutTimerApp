﻿using System;
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
    public partial class BasicTimerPage : ContentPage
    {
        public BasicTimerPage()
        {
            InitializeComponent();
            BindingContext = new BasicTimerViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            // InitializeComponent();
        }

    }
}