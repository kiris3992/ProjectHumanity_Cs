using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHumanity
{
    class Human
    {
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private Gender gender { get; set; }
        private enum Gender
        {
            Female = 0,
            Male = 1
        }

        public Human(int _gender, string firstName, string lastName)
        {
            gender = (Gender)_gender;
            FirstName = firstName;
            LastName = lastName;
        }

        public virtual string GetInfo()
        {
            return $"Type\t\t: ({this.GetType().Name})\nFirst Name\t: {FirstName}\nLast Name\t: {LastName}\nGender\t\t: {gender.ToString()}\n";
        }

        public static int CountHumansInList(List<Human> humansList, Type type = null)
        {
            if (type == null)
            {
                return humansList.Count;
            }
            else
            {
                return humansList.Count(item => item.GetType() == type);
            }
           
        }
    }
}
