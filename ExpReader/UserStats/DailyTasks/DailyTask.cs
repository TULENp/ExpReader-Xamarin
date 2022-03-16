using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using ExpReader.Services;
using Xamarin.Essentials;

namespace ExpReader.UserStats.DailyTasks
{
    static class DailyTask
    {
        public static int TodayReadPages;

        public static void UpdateTodayReadPages()
        {
            TodayReadPages++;
            Preferences.Set(nameof(TodayReadPages), TodayReadPages);
            CheckTaskComletion();
        }
        private static void CheckTaskComletion()
        {
            TodayReadPages = Preferences.Get(nameof(TodayReadPages), 0);
            if (TodayReadPages == Settings.DailyTask)
            {
                //userstats.readpages +=  Settings.DailyTask;
                UserDialogs.Instance.Alert($"Вы выполнили ежедневное задание и получаете {Settings.DailyTask} очков", "Поздравляю");
            }
        }

    }
}
