using System;
using System.Collections.Generic;
using System.Text;
using WorkoutTimerApp.Services;
using WorkoutTimerApp.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

namespace WorkoutTimerApp.ViewModels
{
    public class WorkoutLogsViewModel: BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<WorkoutHistory> _workoutHistories;
        private bool isRefereshing;

        public Command RefreshCommand { get; set; }
        public bool IsRefereshing
        {
            get { return isRefereshing; }
            set => SetProperty(ref isRefereshing, value);
        }
        public ObservableCollection<WorkoutHistory> WorkoutHistories
        {
            get { return _workoutHistories; }
            set
            {
                _workoutHistories = value;
                OnPropertyChanged();
            }
        }

        public void OnRefresh()
        {
            isRefereshing = true;
            WorkoutHistories = new ObservableCollection<WorkoutHistory>();
            List<WorkoutHistory> WorkoutHistoriesList = new List<WorkoutHistory>();
            GetData(WorkoutHistoriesList);

            Refresh();
            isRefereshing = false;
        }

        public  WorkoutLogsViewModel()
        {
            Title = "Workout History";
            
            WorkoutHistories = new ObservableCollection<WorkoutHistory>();
            List<WorkoutHistory> workouthistorieslist = new List<WorkoutHistory>();
            GetData(workouthistorieslist);

            Refresh();
            RefreshCommand = new Command(OnRefresh);



        }

        public async Task GetData(List<WorkoutHistory> WorkoutHistoriesList)
        {
             WorkoutHistoriesList = await DBServices.GetWorkoutHistoryAsync();
             foreach (var history in WorkoutHistoriesList)
             {
                 WorkoutHistories.Add(history);
             }
             
        }
        async Task Refresh()
        {
            await Task.Delay(2000);
        }

        public PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnpropertyChanged([CallerMemberName] string PropertName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertName));
        }
    }
}
