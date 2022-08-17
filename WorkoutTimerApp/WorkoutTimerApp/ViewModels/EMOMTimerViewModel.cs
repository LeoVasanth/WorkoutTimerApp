using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using JetBrains.Annotations;
using WorkoutTimerApp.Models;
using WorkoutTimerApp.Services;

namespace WorkoutTimerApp.ViewModels
{

    public class EMOMTimerViewModel : BaseViewModel, INotifyPropertyChanged
    {

        WorkoutHistory WorkoutHistoryEntry = new WorkoutHistory();

        public EMOMTimerViewModel()
        {
            Title = "EMOM Timer";
            StartTimerCommand = new Command(OnStartTimerExecute);
            PauseTimerCommand = new Command(OnPauseTimerExecute);
            StopTimerCommand = new Command(OnStopTimerExecute);
            StartTime = TimeSpan.FromSeconds(Convert.ToInt64("0"));



        }
        private void OnPauseTimerExecute()
        {
            Pause = false;

        }
        private void OnStopTimerExecute()
        {
            Pause = false;
            WorkoutHistoryEntry.Duration = Duration;
            WorkoutHistoryEntry.WorkoutName = "EMOM";
            WorkoutHistoryEntry.DateTime = DateTime.Now;
            WorkoutHistoryEntry.ID = "AYT5678";
            DBServices.InsertWorkoutHistory(WorkoutHistoryEntry);
            StartTime = EndTime;
        }

        private void OnStartTimerExecute()
        {
            Pause = true;
            RoundCount = 0;

            LeadTime = TimeSpan.FromSeconds(Convert.ToInt64("10"));
            LeadDuration = LeadTime.ToString();

            EndTime = TimeSpan.FromSeconds(Convert.ToInt64("60"));

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

                    if (StartTime.TotalSeconds < EndTime.TotalSeconds && Pause && LeadTime.TotalSeconds == 0 && RoundCount < Convert.ToInt64(NumOfRounds))
                    {
                        StartTime = StartTime + TimeSpan.FromSeconds(1);

                        Duration = StartTime.ToString();

                        return (true);
                    }
                    else
                    {
                        if (StartTime.TotalSeconds < EndTime.TotalSeconds && Pause && LeadTime.TotalSeconds > 0 && RoundCount < Convert.ToInt64(NumOfRounds))
                        {

                            LeadTime = LeadTime - TimeSpan.FromSeconds(1);
                            LeadDuration = LeadTime.ToString();
                            return (true);
                        }
                        else
                        {
                            if (StartTime.TotalSeconds == EndTime.TotalSeconds && Pause && LeadTime.TotalSeconds == 0 && RoundCount < Convert.ToInt64(NumOfRounds))
                            {
                                RoundCount++;
                                StartTime = TimeSpan.FromSeconds(Convert.ToInt64("0"));
                                return (true);
                            }
                            else
                            {
                                WorkoutHistoryEntry.Duration = Duration;
                                WorkoutHistoryEntry.WorkoutName = "EMOM";
                                WorkoutHistoryEntry.DateTime = DateTime.Now;
                                WorkoutHistoryEntry.ID = "AYT5678";
                                DBServices.InsertWorkoutHistory(WorkoutHistoryEntry);
                                return (false);
                            }
                        }
                    }

                });
            

        }


        public bool Pause { get; set; }

        public string NumOfRounds { get; set; }

        private int _roundCount;
        public int RoundCount
        {
            get { return _roundCount; }
            set
            {
                _roundCount = value;
                OnPropertyChanged();
            }
        }


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
