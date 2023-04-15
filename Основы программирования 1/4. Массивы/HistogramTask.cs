using System;
using System.Linq;
using System.Windows.Forms;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            return new HistogramData(string.Format("Рождаемость людей с именем '{0}'", name),
                ComputeCalendarDays(), CountDays(names, name));
        }
        public static string[] ComputeCalendarDays()
        {
            string[] calendarDays = new string[31];

            for (int day = 0; day < calendarDays.Length; day++)
                calendarDays[day] = (day + 1).ToString();
            return calendarDays;
        }
        public static double[] CountDays(NameData[] names, string name)
        {
            double[] daysCount = new double[32];
            foreach (var nameData in names)
                if (nameData.BirthDate.Day != 1 && nameData.Name == name) daysCount[nameData.BirthDate.Day]++;
            var daysCountWOZero = daysCount.Skip(1).ToArray();
            return daysCountWOZero;
        }
    }
}