using System;
using System.Threading;
using WorkoutTimerApp.ViewModels;
using Xunit;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace WorkoutApplicationTestProject1
{
    public class BasicTimerUnitTest
    {
       // [Fact]
        //public void NotNullTest()
        //{
        //    var duration = new BasicTimerViewModel();
        //   // duration = 5;
        //    Assert.NotNull(duration);
        //}

       [Fact]
        public void BasicTimerApp()
        {
            var BtvmObj = new BasicTimerViewModel();
            
            // duration = 5;
            BtvmObj.Min = "1";
            BtvmObj.Sec = "20";

            BtvmObj.OnStartTimerExecute();

            Thread.Sleep(80000);


            Assert.Equal(80,Convert.ToInt64(BtvmObj.StartTime.TotalSeconds));
        }
    }
}
