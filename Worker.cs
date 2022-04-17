using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHumanity
{
    class Worker : Human
    {
        private float WeekSalary { get; set; }
        private int WorkHoursPerDay { get; set; }
        private int WorkWeekDays { get; set; } // Auto den to zitaei alla to ebala na yparxei gia mellontikh xrhsh. To pernei karfota ston Constructor ths Class pros to paron.
        private float SalaryPerHour
        {
            get
            {
                return WeekSalary / WorkWeekDays / WorkHoursPerDay;
            }
        }

        public Worker(int gender, string firstName, string lastName, float weekSalary, int workHoursPerDay) : base(gender, firstName, lastName)
        {
            WorkWeekDays = 5;
            WeekSalary = weekSalary;
            WorkHoursPerDay = workHoursPerDay;           
        }
        public Worker(object[] vals) : base(Convert.ToInt32(vals[0]), Convert.ToString(vals[1]), Convert.ToString(vals[2]))
        {
            WorkWeekDays = 5;
            WeekSalary = float.Parse(vals[3].ToString());
            WorkHoursPerDay = Convert.ToInt32(vals[4]);
        }
        public override string GetInfo()
        {
            return base.GetInfo() + $"Week Salary\t: {WeekSalary.ToString("N2")}\nHours Per Day\t: {WorkHoursPerDay}\nSalary Per Hour\t: {SalaryPerHour.ToString("N2")}\n";
        }
    }
}
