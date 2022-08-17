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
    public class TabataTimerViewModel : BaseViewModel, INotifyPropertyChanged
    {

        WorkoutHistory WorkoutHistoryEntry = new WorkoutHistory();


        public TabataTimerViewModel()
        {
            Title = "TABATA Timer";
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
            if (Duration == null)
            {
                Duration = "0";
            }
            else
            {
                WorkoutHistoryEntry.Duration = Duration;
            }
            WorkoutHistoryEntry.WorkoutName = "TABATA";
            WorkoutHistoryEntry.DateTime = DateTime.Now;
            WorkoutHistoryEntry.ID = "AYT5678";
            DBServices.InsertWorkoutHistory(WorkoutHistoryEntry);
            StartTime = TimeSpan.FromSeconds(Convert.ToInt64("0"));

        }

        private void OnStartTimerExecute()
        {
            Pause = true;
            RoundCount = 1;

            LeadTime = TimeSpan.FromSeconds(Convert.ToInt64("10"));
            LeadDuration = LeadTime.ToString();
            RestTimeStart = TimeSpan.FromSeconds(Convert.ToInt64("0"));
            RestTimeEnd = TimeSpan.FromSeconds(Convert.ToInt64(RestTime));
            RestDuration = TimeSpan.FromSeconds(Convert.ToInt64("0")).ToString();

            EndTime = TimeSpan.FromSeconds(Convert.ToInt64(WorkoutTime));

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


                if (StartTime.TotalSeconds < EndTime.TotalSeconds &&  Pause && LeadTime.TotalSeconds == 0 && RoundCount <= Convert.ToInt64(NumOfRounds))
                {
                    StartTime = StartTime + TimeSpan.FromSeconds(1);

                    Duration = StartTime.ToString();

                    return (true);
                }
                else
                {
                    if (StartTime.TotalSeconds < EndTime.TotalSeconds  && Pause && LeadTime.TotalSeconds > 0 && RoundCount <= Convert.ToInt64(NumOfRounds))
                    {

                        LeadTime = LeadTime - TimeSpan.FromSeconds(1);
                        LeadDuration = LeadTime.ToString();
                        return (true);
                    }
                    else
                    {
                        if (StartTime.TotalSeconds == EndTime.TotalSeconds && Pause && LeadTime.TotalSeconds == 0 && RoundCount <= Convert.ToInt64(NumOfRounds))
                        {
                            
                           
                            if (RestTimeStart.TotalSeconds < RestTimeEnd.TotalSeconds && RoundCount <= Convert.ToInt64(NumOfRounds))
                            {
                                RestTimeStart = RestTimeStart + TimeSpan.FromSeconds(1);
                                RestDuration = RestTimeStart.ToString();
                            }
                            else
                            {
                                RoundCount++;
                                StartTime = TimeSpan.FromSeconds(Convert.ToInt64("0"));
                                RestTimeStart = TimeSpan.FromSeconds(Convert.ToInt64("0"));
                            }
                            

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

        public int NumOfRounds { get; set; }

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
        public string WorkoutTime { get; set; }
        public string RestTime { get; set; }

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
        private string _restDuration;
        public string RestDuration
        {
            get { return _restDuration; }
            set
            {
                _restDuration = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan StartTime { get; set; }
        public TimeSpan LeadTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan RestTimeStart { get; set; }
        public TimeSpan RestTimeEnd { get; set; }
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
