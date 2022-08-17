using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using WorkoutTimerApp.Models;
using WorkoutTimerApp.Services;

namespace WorkoutTimerApp.ViewModels
{
    public class BasicTimerViewModel : BaseViewModel,INotifyPropertyChanged
    {
        WorkoutHistory WorkoutHistoryEntry = new WorkoutHistory();


        public BasicTimerViewModel()
        {
            Title = "Basic Timer";
            StartTimerCommand = new Command(OnStartTimerExecute);
            PauseTimerCommand = new Command(OnPauseTimerExecute);
            StopTimerCommand = new Command(OnStopTimerExecute);
            StartTime = TimeSpan.FromSeconds(Convert.ToInt64("0"));
         
        }

        public void OnAppear()
        {
            Min = "0";
            Sec = "0";
            Duration = "0";

        }

        private void OnPauseTimerExecute()
        {
            Pause = false;
          
        }
        private async void OnStopTimerExecute()
        {
            Pause = false;
            StartTime = EndTime;
            if (Duration == null)
            {
                Duration = "0";
            }
            else
            {
                WorkoutHistoryEntry.Duration = Duration;
            }
            
            WorkoutHistoryEntry.WorkoutName = "Basic";
            WorkoutHistoryEntry.DateTime = DateTime.Now;
            WorkoutHistoryEntry.ID = "AYT5678";
            await DBServices.InsertWorkoutHistory(WorkoutHistoryEntry);
            List<WorkoutHistory> WH = new List<WorkoutHistory>();
            WH= await DBServices.GetWorkoutHistoryAsync();
        }

        public void OnStartTimerExecute()
        {
            Pause = true;
            
            LeadTime = TimeSpan.FromSeconds(Convert.ToInt64("10"));
            LeadDuration= LeadTime.ToString();

            EndTime = TimeSpan.FromSeconds(Convert.ToInt64(Min)*60 +Convert.ToInt64(Sec));

            if (StartTime == EndTime)
            {
                Duration = StartTime.ToString();
            }
            else
            {
                TimeSpan.FromSeconds(Convert.ToInt64("0"));
            }
            
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {

                if (StartTime.TotalSeconds < EndTime.TotalSeconds && Pause && LeadTime.TotalSeconds == 0)
                {
                    StartTime = StartTime + TimeSpan.FromSeconds(1);
                    
                    Duration = StartTime.ToString();
                    
                    return (true);
                }
                else
                {
                    if (StartTime.TotalSeconds < EndTime.TotalSeconds && Pause && LeadTime.TotalSeconds > 0)
                    {

                        LeadTime = LeadTime - TimeSpan.FromSeconds(1);
                        LeadDuration = LeadTime.ToString();
                        return(true);
                    }
                    else
                    {
                        WorkoutHistoryEntry.Duration = Duration;
                        WorkoutHistoryEntry.WorkoutName = "Basic";
                        WorkoutHistoryEntry.DateTime = DateTime.Now;
                        WorkoutHistoryEntry.ID = "AYT5678";
                        DBServices.InsertWorkoutHistory(WorkoutHistoryEntry);

                        return (false);
                    }
                }

            } );
            
        }


        public bool Pause { get; set; }
        
        public string Min { get; set; }
        public string Sec { get; set; }

        private string _duration;
        public string Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }
        private string _leadDuration;
        public string LeadDuration
        {
            get { return _leadDuration; }
            set
            {
                _leadDuration = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan LeadTime { get; set; }
        public ICommand StartTimerCommand { get; set; }
        public ICommand PauseTimerCommand { get; set; }
        public ICommand StopTimerCommand { get; set; }


        public PropertyChangedEventHandler PropertyChanged;
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnpropertyChanged([CallerMemberName] string PropertName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertName));
        }

    }
}
