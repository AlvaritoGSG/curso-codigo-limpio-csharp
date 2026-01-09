using System;
using System.Collections.Generic;

namespace ToDo
{
    internal class Program
    {
        public static List<string> Tasks { get; set; }

        static void Main(string[] args)
        {
            Tasks = new List<string>();
            int optionSelectedMenu = 0;
            do
            {
                optionSelectedMenu = ShowMainMenu();
                if ((option)optionSelectedMenu == option.Add)
                {
                    ShowAddOption();
                }
                else if (optionSelectedMenu == (int)option.Remove)
                {
                    ShowRemoveOption();
                }
                else if ((option)optionSelectedMenu == option.ShowList)
                {
                    ShowTasksOption();
                }
            } while (optionSelectedMenu != (int)option.Exit);
        }
        /// <summary>
        /// Show the main menu 
        /// </summary>
        /// <returns>Returns option indicated by user</returns>
        public static int ShowMainMenu()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Ingrese la opción a realizar: ");
            Console.WriteLine("1. Nueva tarea");
            Console.WriteLine("2. Remover tarea");
            Console.WriteLine("3. Tareas pendientes");
            Console.WriteLine("4. Salir");

            // Read line
            string line = Console.ReadLine();
            return Convert.ToInt32(line);
        }

        public static void ShowRemoveOption()
        {
            try
            {
                Console.WriteLine("Ingrese el número de la tarea a remover: ");
                // Show current taks
                for (int i = 0; i < Tasks.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + Tasks[i]);
                }
                Console.WriteLine("----------------------------------------");

                string line = Console.ReadLine();
                // Remove one position
                int indexToRemove = Convert.ToInt32(line) - 1;
                if (indexToRemove > -1)
                {
                    if (Tasks.Count > 0)
                    {
                        string task = Tasks[indexToRemove];
                        Tasks.RemoveAt(indexToRemove);
                        Console.WriteLine("Tarea " + task + " eliminada");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void ShowAddOption()
        {
            try
            {
                Console.WriteLine("Ingrese el nombre de la tarea: ");
                string task = Console.ReadLine();
                Tasks.Add(task);
                Console.WriteLine("Tarea registrada");
            }
            catch (Exception)
            {
            }
        }

        public static void ShowTasksOption()
        {
            if (Tasks == null || Tasks.Count == 0)
            {
                Console.WriteLine("No hay tareas por realizar");
            }
            else
            {
                Console.WriteLine("----------------------------------------");
                for (int i = 0; i < Tasks.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + Tasks[i]);
                }
                Console.WriteLine("----------------------------------------");
            }
        }
    }

    public enum option
    {
        Add = 1,
        Remove = 2,
        ShowList = 3,
        Exit = 4
    }

}