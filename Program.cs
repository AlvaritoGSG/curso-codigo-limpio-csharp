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
                if ((Option)optionSelectedMenu == Option.Add)
                {
                    ShowAddOption();
                }
                else if (optionSelectedMenu == (int)Option.Remove)
                {
                    ShowRemoveOption();
                }
                else if ((Option)optionSelectedMenu == Option.ShowList)
                {
                    ShowTasksOption();
                }
            } while (optionSelectedMenu != (int)Option.Exit);
        }
        public static int ShowMainMenu()
        {
            Console.WriteLine(" ______________________________________");
            Console.WriteLine("|   Ingrese la opción a realizar:      |");
            Console.WriteLine("|   1. Nueva tarea                     |");
            Console.WriteLine("|   2. Remover tarea                   |");
            Console.WriteLine("|   3. Tareas pendientes               |");
            Console.WriteLine("|   4. Salir                           |");
            Console.WriteLine("|______________________________________|");
            // Read line
            string line = Console.ReadLine();
            return Convert.ToInt32(line);
        }
        public static void ShowRemoveOption()
        {
            if (Tasks == null || Tasks.Count == 0)
            {
                responder("No hay tareas por eliminar.");
                return;
            }
            try
            {
                ShowTasksOption();
                Console.WriteLine("[ c ] . Para cancelar operación de eliminar.");
                // Personalizar respuesta para opción [ c ]. Aquí en try (No catch)
                Console.WriteLine(" -> Ingrese el número de la tarea a remover: ");
                string line = Console.ReadLine();
                int indexToRemove = Convert.ToInt32(line) - 1;
                if (indexToRemove < 0 || indexToRemove > Tasks.Count - 1)
                {
                    responder("Opción seleccionada fuera de rango.");
                    ShowRemoveOption();
                }
                else if (indexToRemove > -1 && Tasks.Count > 0)
                {
                    string task = Tasks[indexToRemove];
                    Tasks.RemoveAt(indexToRemove);
                    responder("Tarea: " + task + " ¡Eliminada!");
                    ShowTasksOption();
                }
            }
            catch (Exception)
            {
                responder("Error al eliminar la tarea. Ingrese un opción válida.");
            }
        }
        public static void ShowAddOption()
        {
            try
            {
                Console.WriteLine("-> Ingrese el nombre de la tarea: ");
                string task = Console.ReadLine();
                if (task != null && task.Trim() != "")
                {
                    Tasks.Add(task);
                    responder($"Tarea: {task}, registrada con éxito!");
                }
                else
                {
                    responder("-> El nombre de la tarea no puede estar vacío, ¡Ingrese un dato válido!");
                    ShowAddOption();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al agregar la tarea.");
            }
        }
        public static void ShowTasksOption()
        {
            if (Tasks == null || Tasks.Count == 0)
            {
                responder("No hay tareas por pendientes/por realizar.");
            }
            else
            {
                Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                // foreach (var (t, i) in Tasks.Select((t, i) => (t, i + 1)))
                foreach (var (t, i) in Tasks.ToIndexedEnumerable())
                {
                    Console.WriteLine($"{i + 1}. {t}");
                }
                Console.WriteLine($"<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            }
        }
        public static void responder(string mensaje)
        {
            Console.WriteLine("================================");
            Console.WriteLine(mensaje);
            Console.WriteLine("================================");
        }
    }

    public enum Option
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