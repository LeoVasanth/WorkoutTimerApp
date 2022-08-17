using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace WorkoutTimerApp.Models
{
    public class WorkoutHistory
    {
        [PrimaryKey,AutoIncrement]
        public int Sno { get; set; }
        public DateTime DateTime { get; set; }
        public string WorkoutName { get; set; }
        public string Duration { get; set; }
        public string ID { get; set; }

    }
}
