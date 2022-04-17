using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHumanity
{
    class Student : Human
    {
        private string FacultyNumber { get; set; }

        public Student(int gender, string firstName, string lastName, string facultyNumber) : base(gender, firstName, lastName)
        {
            FacultyNumber = facultyNumber;
        }
        public Student(object[] vals) : base(Convert.ToInt32(vals[0]), Convert.ToString(vals[1]), Convert.ToString(vals[2]))
        {
            FacultyNumber = Convert.ToString(vals[3]);
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $"Faculty Number\t: {FacultyNumber}\n" ;
        }
    }
}
