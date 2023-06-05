using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var calendarDays = new string[31];
            var months = new string[12];

            return new HeatmapData(
                "Пример карты интенсивностей",
                CountBirthsPerDate(names), 
                ComputeCalendarDate(calendarDays), 
                ComputeCalendarDate(months));
        }

        public static double[,] CountBirthsPerDate(NameData[] names)
        {
            var heatmap = new double[30, 12];
            foreach (var name in names)
                if (name.BirthDate.Day != 1) heatmap[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;
            return heatmap;
        }
        
        public static string[] ComputeCalendarDate(string[] date)
        {
            for (int i = 0; i < date.Length; i++)
                date[i] = (i + 1).ToString();
            if (date.Length == 31) return date.Skip(1).ToArray();
            return date;
        }
    }
}


