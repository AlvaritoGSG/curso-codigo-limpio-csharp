using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDo
{
    internal class Program
    {
        public static List<string> Tasks { get; set; }

        static void Main(string[] args)
        {
            Tasks = new List<string>();
            int optionSelectedMenu = 0;
            /// <summary>
            ///     Show the main menu
            /// </summary>
            /// <returns>
            ///     Returns option indicated by user
            /// </returns>

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
        const string separator = "==========================";
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
                ShowTasksOption();
                string line = Console.ReadLine();
                // Remove one position
                int indexToRemove = Convert.ToInt32(line) - 1;
                if (indexToRemove > -1 && Tasks.Count > 0)
                {
                    string task = Tasks[indexToRemove];
                    Tasks.RemoveAt(indexToRemove);
                    Console.WriteLine(separator);
                    Console.WriteLine("Tarea " + task + " eliminada");
                    Console.WriteLine(separator);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al eliminar la tarea");
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
                Console.WriteLine("Error al agregar la tarea");
            }
        }

        public static void ShowTasksOption()
        {
            if (Tasks == null || Tasks.Count == 0)
            {
                Console.WriteLine(separator);
                Console.WriteLine("No hay tareas por realizar");
                Console.WriteLine(separator);
            }
            else
            {
                Console.WriteLine(separator);
                // foreach (var (t, i) in Tasks.Select((t, i) => (t, i + 1)))
                foreach (var (t, i) in Tasks.ToIndexedEnumerable())
                {
                    Console.WriteLine($"{i + 1}. {t}");
                }
                Console.WriteLine(separator);
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

    public static class EnumerableExtensions
    {
        public static IEnumerable<(T item, int index)> ToIndexedEnumerable<T>(this IEnumerable<T> source)
        {
            int index = 0;
            foreach (T item in source)
            {
                yield return (item, index);
                // 'yield return' es clave: devuelve el par y "pausa" el método.
                // No crea una lista nueva en memoria, solo entrega un dato a la vez.
                index++;
            }
        }
    }
}