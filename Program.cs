using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ProjectHumanity
{
    class Program
    {
        static void Main(string[] args)
        {
            App.Start();
        }

        static class App
        {
            static List<Human> humans;
            static InputValidator validator;

            public static void Start()
            {
                humans = new List<Human>();
                ManageUserInput("/?");
            }

            public static void AddHuman(Type typeOfHuman) {
                validator = new InputValidator();

                if (typeOfHuman == typeof(Student))
                {
                    validator.Add(new InputValidator.IntNumber("Gender", "Give gender (0=Female, 1=Male) : ", 0, 1));
                    validator.Add(new InputValidator.CapString("First Name", "Give first name : ", 3, 50));
                    validator.Add(new InputValidator.CapString("Last Name", "Give last name : ", 4, 50));
                    validator.Add(new InputValidator.FucNumber("Faculty Number", "Give faculty number : ", 5, 10));
                }
                else
                {
                    validator.Add(new InputValidator.IntNumber("Gender", "Give gender (0=Female, 1=Male) : ", 0, 1));
                    validator.Add(new InputValidator.CapString("First Name", "Give first name : ", 3, 50));
                    validator.Add(new InputValidator.CapString("Last Name", "Give last name : ", 4, 50));
                    validator.Add(new InputValidator.FloatNumber("Week Salary", "Give week salary : ", 10f));
                    validator.Add(new InputValidator.IntNumber("Work Hours", "Give work hours (1 to 12) : ", 1, 12));
                }

                bool cancelAction = false;
                try
                {
                    for (int i = 0; i < validator.Items.Count; i++)
                    {
                        Console.WriteLine(validator.Items[i].Query);
                        string userInput = Console.ReadLine();
                        if (userInput == "cancel")
                        {
                            cancelAction = true;
                            break;
                        }
                        if (!validator.Items[i].Validate(userInput))
                        {
                            Console.WriteLine(validator.Items[i].Message); i--;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong ...\nPlease try again.\n" + ex.Message);
                }

                if (cancelAction)
                {
                    Console.WriteLine("Action is canceled by user.");
                    ManageUserInput(Console.ReadLine());
                    return;
                }

                if (validator.IsValid())
                {
                    if (typeOfHuman == typeof(Student))
                    {
                        humans.Add(new Student(validator.GetParamValuesToArray()));
                    }
                    else
                    {
                        humans.Add(new Worker(validator.GetParamValuesToArray()));
                    }

                    Console.WriteLine($"\nYou have saccesfully add a {humans[humans.Count - 1].GetType().Name} :");
                    Console.WriteLine(humans[humans.Count - 1].GetInfo());
                    Console.WriteLine($"Total Humans : {Human.CountHumansInList(humans)}, Workers : {Human.CountHumansInList(humans, typeof(Worker))}, Students : {Human.CountHumansInList(humans, typeof(Student))}");
                    ManageUserInput(Console.ReadLine());
                }
                else
                {
                    ManageUserInput(Console.ReadLine());
                }
            }

            public static void ManageUserInput(string inputValue = "")
            {
                switch (inputValue)
                {
                    case "/?":
                        Console.WriteLine(" a.\tAdd a Human :\n\t\t- For Worker, type '1'\n\t\t- For Student, type '2'");
                        Console.WriteLine(" b.\tView Humans :\n\t\t- For Workers, type '3'\n\t\t- For Students, type '4'\n\t\t- For Humans, type '5'");
                        Console.WriteLine(" c.\tInfo :\n\t\t- Exit App, type 'exit'\n\t\t- Clear screen, type '0'\n\t\t- View Help, type '/?'");
                        Console.WriteLine("\n");
                        ManageUserInput(Console.ReadLine());
                        break;

                    case "1":
                        Console.WriteLine(" ** Add a Worker ** Info : To cancel the action type 'cancel'.\n");
                        AddHuman(typeof(Worker));
                        break;

                    case "2":
                        Console.WriteLine(" ** Add a Student ** Info : To cancel the action type 'cancel'.\n");
                        AddHuman(typeof(Student));
                        break;

                    case "3":
                        Console.WriteLine($"\nAll Workers, total : {Human.CountHumansInList(humans, typeof(Worker))} --> Start\n");
                        humans.ForEach(human => {
                            if (human.GetType().Name == "Worker") { Console.WriteLine(human.GetInfo()); }
                        });
                        Console.WriteLine("\nAll Workers --^ End\n");
                        ManageUserInput(Console.ReadLine());
                        break;

                    case "4":
                        Console.WriteLine($"\nAll Students, total : {Human.CountHumansInList(humans, typeof(Student))} --> Start\n");
                        humans.ForEach(human => {
                            if (human.GetType().Name == "Student") { Console.WriteLine(human.GetInfo()); }
                        });
                        Console.WriteLine("\nAll Students --^ End\n");
                        ManageUserInput(Console.ReadLine());
                        break;

                    case "5":
                        Console.WriteLine($"\nAll Humans, total : {Human.CountHumansInList(humans)} --> Start\n");
                        humans.ForEach(human => { Console.WriteLine(human.GetInfo()); });
                        Console.WriteLine("\nAll Humans --^ End\n");
                        ManageUserInput(Console.ReadLine());
                        break;

                    case "0":
                        Console.Clear();
                        Console.WriteLine("Type /? to show help screen.");
                        ManageUserInput(Console.ReadLine());
                        break;

                    case "exit":
                        return;

                    default:
                        Console.Beep();
                        Console.WriteLine("\nWrong input ... To view Help screen type /? and hit enter.\n");
                        ManageUserInput(Console.ReadLine());
                        break;
                }
            }
        }
    }
}
