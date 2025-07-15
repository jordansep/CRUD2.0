using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

// Feature is not available. Use newer language version.
interface IAsistencia
{

    void Asist();
    void AddRegister();
    void Mostrar(Asistencia database);
    void ShowLines(string specificDate);
} 

class Asistencia : ArchiveManipulation, IAsistencia
{
    // private Dictionary<int, string> secondDicctionaryLines;
    public Asistencia(string DateTipes) : base(DateTipes)
    {
        this.DataTipes = DateTipes.ToLower().Split(',');
        LlenarDiccionario();
    }


    public override void AddRegister()
    {

        // Sumamos uno ya que la ID ya esta contada.
        // Inicializamos con un array de tamaño cantDatos
        string[] registro = new string[this.DataTipes.Length + 1];
        // El primer dato es la ID asi que no la pedimos
        registro[0] = GetUniqueID().ToString();
        string newLine = "";
        // mientras que los siguientes datos los tendra que ingresar el usuario.
        for (int i = 0; i < this.DataTipes.Length; i++)
        {
            // Controlamos que el tipo de dato a pedir coincida con su posicion pasada al constructor.
            string currentDateTipe = this.DataTipes[i].Trim();
            switch (currentDateTipe)
            {
                case "nombre":
                    registro[i + 1] = Validate.Texto("Ingrese el nombre");
                    break;
                case "asistencia":
                    registro[i + 1] = "Ausente";
                    break;
            }
            newLine = string.Join(";", registro);
        }
            File.AppendAllText(this.secondFilePath, newLine + Environment.NewLine);
    }
    public void Asist()
    {
        Console.WriteLine("--- Tomaremos Asistencia ---\n");
        string[] secondFileLines = File.ReadAllLines(this.secondFilePath);
        Console.WriteLine("Presione [A] para Ausente, [P] para Presente");
        for(int i = 0; i < secondFileLines.Length; i++)
        {
            string[] users = secondFileLines[i].Split(';');
            Console.WriteLine($"{users[0]}) {users[1]}");
            string input;
            bool validInput = false;
            do {
                ConsoleKeyInfo key = Console.ReadKey(true);
                input = key.KeyChar.ToString().ToUpper();
                if (input == "P")
                {
                    users[2] = "Presente";
                    Console.WriteLine($"{users[1]} esta Presente.");
                    validInput = true;
                }
                else if(input == "A")
                {
                    Console.WriteLine($"{users[1]} esta Ausente");
                    validInput = true;
                }
                else
                {
                    Console.WriteLine($"Entrada '{input}' no válida para {users[1]}. Por favor, presione 'P' o 'A'.");
                }
            } while (!validInput);

                string newLine = string.Join(";", users);
            secondFileLines[i] = newLine;
        }
        File.WriteAllLines(this.filePath, secondFileLines);
        Console.WriteLine("\n--- Asistencia tomada correctamente ---");
        LlenarDiccionario();
    }
    public void ShowLines(string specificDate)
    {
        string datePath = Path.Combine(this.folderPath, specificDate + ".txt");
        if (!File.Exists(datePath))
        {
            Console.WriteLine("No se ha tomado asistencia en esa fecha");
            return;
        }
        string[] dateLines = File.ReadAllLines(datePath);
        if (dateLines == null) { return; }
        for (int i = 0; i < dateLines.Length; i++)
        {
            if (dateLines[i] == null)
            {
                Console.WriteLine(dateLines[i]);
                Console.WriteLine("Quisiera agregar alguna?");
            }
            string[] datos = dateLines[i].Split(';');
            // Daremos formato sin importar cuantos datos sean guardados en las lineas.
            for (int j = 0; j < datos.Length; j++)
            {
                if (j == 0) Console.Write($"ID: {datos[j]} - ");
                else if (j == datos.Length - 1) Console.Write(datos[j]);
                else Console.Write(datos[j] + " - ");
            }
            Console.WriteLine();
        }
    }
    public void Mostrar(Asistencia dataBase)
    {
        int opciones;
        do
        {
            Console.WriteLine($"Registro de Asistencias del dia {DateTime.UtcNow.ToString("yyyy-MM-dd")}");
        dataBase.ShowLines();
        Console.WriteLine("1) Eliminar por ID");
        Console.WriteLine("2) Agregar Registro");
        Console.WriteLine("3) Tomar Asistencia");
        Console.WriteLine("4) Modificar Registro");
        Console.WriteLine("5) Buscar registro de asistencias en una fecha.");
        Console.WriteLine("6) Ordenar Registros ");
        Console.WriteLine("7) Salir.");
        opciones = Validate.Entero(1,7,"Ingrese una opcion");
        switch (opciones)
        {
            case 1:
                dataBase.EliminarID(Validate.Entero("Ingrese la ID."));
                break;
            case 2:
                dataBase.AddRegister();
                break;
            case 3: 
                dataBase.Asist(); 
                break;
            case 4:
                dataBase.ModifyRegisterByID(Validate.Entero("Ingrese la ID donde desea modificar los datos"));
                break;
            case 5: ShowLines(Validate.Fecha()); 
                break;
            case 6: dataBase.OrderLines(); break;
            case 7: return;
        }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey(); // Pause to let the user see the output
            Console.Clear();
        } while (opciones != 7);
    }
}

