using System;

namespace DueDateCalculator
{
    public class DueDateCalculator
    {
        const int workHour = 8;
        const int weekendDays = 2;
        const int weekDays = 5;
        const int weekLength = 7;

        const int startHour = 9;
        const int endHour = 17;
        const int aDayInHours = 24;

        public DateTime CalculateDueDate(DateTime date, float turnaround)
        {
            DateTime dueDate = date;

            int workDaysToAdd = CalculateWorkDaysToAdd(turnaround);
            int weeksToAdd = CalculateWeeksToAdd(workDaysToAdd);
            dueDate += TimeSpan.FromDays(weekLength * weeksToAdd);

            int daysToAdd = CalculateDaysToAdd(dueDate, workDaysToAdd);
            dueDate += TimeSpan.FromDays(daysToAdd);

            turnaround -= workDaysToAdd * workHour;
            AddHoursToDueDate(ref dueDate, turnaround);

            return dueDate;
        }

        int CalculateWorkDaysToAdd(float turnaround)
        {
            return (int)(turnaround / workHour);
        }

        int CalculateWeeksToAdd(int workDaysToAdd)
        {
            return workDaysToAdd / weekDays;
        }

        int CalculateDaysToAdd(DateTime date, int workDaysToAdd)
        {
            int daysToAdd = workDaysToAdd % weekDays;
            if (daysToAdd > 0 && IsInWeekendRange(date, daysToAdd))
            {
                daysToAdd += weekendDays;
            }

            return daysToAdd;
        }

        bool IsInWeekendRange(DateTime date, int daysToAdd)
        {
            return (int)date.DayOfWeek + daysToAdd > weekDays;
        }

        void AddHoursToDueDate(ref DateTime dueDate, float turnaround)
        {
            if (dueDate.Hour + turnaround >= endHour)
            {
                turnaround += aDayInHours - endHour + startHour;
            }
            dueDate += TimeSpan.FromHours(turnaround);

            if ((int)dueDate.DayOfWeek > weekDays)
            {
                dueDate += TimeSpan.FromDays(weekendDays);
            }
        }
    }
}
