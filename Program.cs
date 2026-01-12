List<string> Tasks = new List<string>();
int optionSelectedMenu = 0;

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

int ShowMainMenu()
{
    Console.WriteLine(" ______________________________________");
    Console.WriteLine("|   Ingrese la opción a realizar:      |");
    Console.WriteLine("|   1. Nueva tarea                     |");
    Console.WriteLine("|   2. Remover tarea                   |");
    Console.WriteLine("|   3. Tareas pendientes               |");
    Console.WriteLine("|   4. Salir                           |");
    Console.WriteLine("|______________________________________|");
    string line = Console.ReadLine();
    return int.TryParse(line, out int result) ? result : 0;
}

void ShowAddOption()
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
            responder(" -> El nombre de la tarea no puede estar vacío, ¡Ingrese un dato válido!");
            ShowAddOption();
        }
    }
    catch (Exception)
    {
        Console.WriteLine("Error al agregar la tarea.");
    }
}

void ShowRemoveOption()
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
        Console.WriteLine(" -> Ingrese el número de la tarea a remover: ");
        string line = Console.ReadLine();
        if (line == "c" || line == "C")
        {
            responder("¡Operación cancelada!");
            return;
        }
        if (int.TryParse(line, out int indexToRemove))
        {
            indexToRemove = indexToRemove - 1;
            if (indexToRemove < 0 || indexToRemove > Tasks.Count - 1)
            {
                responder("Opción seleccionada fuera de rango.");
                ShowRemoveOption();
            }
            else if (indexToRemove > -1 && Tasks.Count > 0)
            {
                string task = Tasks[indexToRemove];
                Tasks.RemoveAt(indexToRemove);
                responder($"Tarea: {task} ¡Eliminada!");
                ShowTasksOption();
            }
        }
        else
        {
            responder("Opción inválida. Debe ingresar un número válido.");
            ShowRemoveOption();
        }
    }
    catch (Exception)
    {
        responder("Error al eliminar la tarea. Ingrese un opción válida.");
    }
}

void ShowTasksOption()
{
    // if (Tasks == null || Tasks.Count == 0)
    if (Tasks?.Count == 0)
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

void responder(string mensaje)
{
    Console.WriteLine("================================");
    Console.WriteLine(mensaje);
    Console.WriteLine("================================");
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