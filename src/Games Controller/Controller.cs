using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Games_Controller
{
    public class Controller
    {
        private int DaysInCurrentMonth { get; } = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

        public void DoIt()
        {
            var daysNumber = GetDaysNumber();
            foreach (var day in GetDays(daysNumber))
                Console.WriteLine(day.ToString("d", new CultureInfo("uk-UA")));
        }

        private int GetDaysNumber()
        {
            int daysNumber;
            Console.WriteLine("How many days do you want?");
            var input = Console.ReadLine();
            while (!int.TryParse(input, out daysNumber) || !CheckForJura(daysNumber))
            {
                Console.Clear();
                Console.WriteLine(!CheckForJura(daysNumber)
                    ? $"You are trying to be rude. In current month only {DaysInCurrentMonth} not {daysNumber}"
                    : $"Don't joke with me!{Environment.NewLine}\"{input}\" is not an integer");
                Thread.Sleep(5000);
                Console.Clear();
                Console.WriteLine("How many days do you want?");
                input = Console.ReadLine();
            }

            return daysNumber;
        }

        private bool CheckForJura(int daysNumber)
        {
            return DaysInCurrentMonth >= daysNumber;
        }

        private IEnumerable<DateTime> GetDays(int numberOfDays)
        {
            var days = new List<DateTime>(numberOfDays);
            var random = new Random();

            while (days.Count < numberOfDays)
            {
                var anotherDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1 + random.Next(DaysInCurrentMonth));
                if (!days.Contains(anotherDay))
                    days.Add(anotherDay);
            }

            days.Sort();
            return days;
        }
    }
}
