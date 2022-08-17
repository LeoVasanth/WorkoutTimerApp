using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WorkoutTimerApp.Models;
using Xamarin.Essentials;

namespace WorkoutTimerApp.Services
{
    public static class DBServices
    {
        static SQLiteAsyncConnection database;

        static async Task init()
        {
            if (database != null)
                return;

            var DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            database = new SQLiteAsyncConnection(DatabasePath);

            await database.CreateTableAsync<WorkoutHistory>();
        }

        public static async Task InsertWorkoutHistory(WorkoutHistory workoutHistory)
        {
            await init();

                await database.InsertAsync(workoutHistory);
            
        }

        public static async Task<List<WorkoutHistory>> GetWorkoutHistoryAsync()
        {
            await init();
            //Get all notes.
             return await database.Table<WorkoutHistory>().ToListAsync();
        }

        

     
    }
}
