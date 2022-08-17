using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using WorkoutTimerApp.Models;
using WorkoutTimerApp.Services;
using Xamarin.Forms;

namespace WorkoutTimerApp.ViewModels
{
    public class AMRAPTimerViewModel : BaseViewModel, INotifyPropertyChanged
    {

        WorkoutHistory WorkoutHistoryEntry = new WorkoutHistory();


        public AMRAPTimerViewModel()
        {
            Title = "AMRAP Timer";
            StartTimerCommand = new Command(OnStartTimerExecute);
            PauseTimerCommand = new Command(OnPauseTimerExecute);
            StopTimerCommand = new Command(OnStopTimerExecute);
            StartTime = TimeSpan.FromSeconds(Convert.ToInt64("0"));



        }
        private void OnPauseTimerExecute()
        {
            Pause = false;
            Min = StartTime.Minutes.ToString();
            Sec = StartTime.Seconds.ToString();

        }
        private void OnStopTimerExecute()
        {
            Pause = false;
            if (Duration == null)
            {
                Duration = "0";
            }
            else
            {
                WorkoutHistoryEntry.Duration = Duration;
            }
            WorkoutHistoryEntry.WorkoutName = "AMRAP";
            WorkoutHistoryEntry.DateTime = DateTime.Now;
            WorkoutHistoryEntry.ID = "AYT5678";
            DBServices.InsertWorkoutHistory(WorkoutHistoryEntry);
            StartTime = TimeSpan.FromSeconds(Convert.ToInt64("0"));

        }

        private void OnStartTimerExecute()
        {
            Pause = true;

            LeadTime = TimeSpan.FromSeconds(Convert.ToInt64("10"));
            LeadDuration = LeadTime.ToString();

            StartTime = TimeSpan.FromSeconds(Convert.ToInt64(Min) * 60 + Convert.ToInt64(Sec));

            Duration = StartTime.ToString();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {

                if (StartTime.TotalSeconds > 0 && Pause && LeadTime.TotalSeconds == 0)
                {
                    StartTime = StartTime - TimeSpan.FromSeconds(1);

                    Duration = StartTime.ToString();

                    return (true);
                }
                else
                {
                    if (StartTime.TotalSeconds > 0 && Pause && LeadTime.TotalSeconds > 0)
                    {

                        LeadTime = LeadTime - TimeSpan.FromSeconds(1);
                        LeadDuration = LeadTime.ToString();
                        return (true);
                    }
                    else
                    {
                        WorkoutHistoryEntry.Duration = Duration;
                        WorkoutHistoryEntry.WorkoutName = "AMRAP";
                        WorkoutHistoryEntry.DateTime = DateTime.Now;
                        WorkoutHistoryEntry.ID = "AYT5678";
                        DBServices.InsertWorkoutHistory(WorkoutHistoryEntry);
                        return (false);
                    }
                }

            });

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
