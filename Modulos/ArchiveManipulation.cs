using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

internal class ArchiveManipulation
{
    // TODO: NombrarArchivos
    // TODO: Modificacion de datos --
    // TODO: Tipos de datos que reciben. --
    // Ej: al poner 4 datos, especificar que tipo de datos van a recibir cada uno
    // Nombre(string): Jordan, Email(email): jordanmonier4@gmail.com, edad(numero): 24
    // Entonces al modificar los datos, pedir el tipo de dato necesario.

    // Obtenemos los datos necesarios para comenzar a trabajar con archivos.
    protected string folderPath = ManipulationPath.folderPath;
    protected string filePath = ManipulationPath.filePath;
    protected string secondFilePath = ManipulationPath.secondFilePath;
    // Las lineas se agregaran en formato |id;dato;dato;dato;dato|
    protected string[] lines
    {
        get { return File.ReadAllLines(filePath); }
    }
    // Todos los ejercicios me solicitan ID, ademas siempre es bueno tener un identificador unico
    private int id;
    private Dictionary<int, string> DiccionarioLineas = new Dictionary<int, string>();
    protected string[] DataTipes;
    public ArchiveManipulation(string DataTipes) {
        this.DataTipes = DataTipes.ToLower().Split(',');
        LlenarDiccionario();

    }

    // Este metodo funciona, pero para pedir datos especificos como por ej: Email, Numeros, no podra ser validado desde aqui.
    // Por lo tanto tendremos que sobreescribirlo en las subclases
    public virtual void AddRegister()
    {
        
        // Sumamos uno ya que la ID ya esta contada.
        // Inicializamos con un array de tamaño cantDatos
        string[] registro = new string[this.DataTipes.Length + 1];
        // El primer dato es la ID asi que no la pedimos
        registro[0] = GetUniqueID().ToString();
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
                    case "email":
                        registro[i + 1] = Validate.Email("Ingrese el email");
                        break;
                    case "entero":
                        registro[i + 1] = Validate.Entero("Ingrese un numero").ToString();
                        break;
                    case "edad":
                        registro[i + 1] = Validate.Entero(0, 110, "Ingrese la edad").ToString();
                        break;
                    case "precio":
                        registro[i + 1] = Validate.Entero(0, 9999999, "Ingrese el precio del producto").ToString();
                        break;
                    case "stock":
                        registro[i + 1] = Validate.Entero(0, 9999999, "Ingrese el precio del producto").ToString();
                        break;
                    case "asistencia": 
                        registro[i + 1] ="Ausente"; 
                        break;
                    default: registro[i + 1] = Validate.Texto("Tipo de dato invalido, por default podra asignar cualquier dato.\nTenga en cuenta que la validacion sera incorrecta."); 
                        break; 
                }
        }
            string newLine = string.Join(";", registro);
            File.AppendAllText(this.filePath, newLine + Environment.NewLine);
    }
    // Cambia el nombre del archivo.
    public void ChangeFileName(string newName)
    {
        File.Move(filePath, Path.Combine(folderPath, newName));
        this.filePath = Path.Combine(folderPath, newName);
    }

    // Mostramos las lineas existentes en el archivo, con un formato predeterminado.
    public void ShowLines()
    {

        if (this.lines == null) { return; }
        for (int i = 0; i < this.lines.Length; i++)
        {
            if (this.lines[i] == null)
            {
                Console.WriteLine(this.lines[i]);
                Console.WriteLine("Quisiera agregar alguna?");
            }
            string[] datos = this.lines[i].Split(';');
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
    // Llena un diccionario que luego sera usado
    // para operaciones donde necesitemos buscar usuarios por ID.
    protected void LlenarDiccionario()
    {
        // Si lines[] esta vacio entonces termina la ejecucion para evitar problemas
        if (lines == null || !lines.Any()) return; 
        foreach (var item in this.lines)
        {
            string[] separar = item.Split(';');
            // intentamos parsear a int el primer elemento en separar[], si se puede devolvemos int id
            int.TryParse(separar[0], out int id);
            // Añadimos la ID como key al diccionario y
            // luego volvemos a conectar los strings al array saltandonos la ID para pasarselo al Value
            this.DiccionarioLineas.Add(id, string.Join(";", separar.Skip(1)));
        }
    }
    // Obtiene una ID unica,
    // Sí borramos un registro esa ID ahora sera utilizable para el siguiente registro añadido
    // Asi evitamos tener saltos de ID en caso de borrar registros.
    protected int GetUniqueID()
    {
        // Mientras el diccionario contenga la ID entonces ID + 1
        while (DiccionarioLineas.ContainsKey(this.id)) {
            this.id++;
        }
        // Retornamos cuando encuentre una ID disponible.
        return this.id;
    }
    // Elimina registros por ID
    public void EliminarID(int id)
    {
        // Remueve donde este la ID pasada por el usuario
        this.DiccionarioLineas.Remove(id);
        // Vuelve a convertir el diccionario en array con el formato anterior
        string[] array = this.DiccionarioLineas.Select(x => $"{x.Key};{x.Value}").ToArray();
        File.WriteAllLines(filePath, array);
    }
    // Ordena las lineas por ID
    public void OrderLinesByID()
    {
        // Nuevo diciconario con todo ordenado
        Dictionary<int, string> ordenar = this.DiccionarioLineas.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
        // Array ordenado                               Vuelvo a unir la ID al Value, con el formato anterior.
        string[] arrayOrdenado = ordenar.Select(x => $"{x.Key};{x.Value}").ToArray();
        File.WriteAllLines(filePath, arrayOrdenado);
    }
    // Ordenar registros Alfabeticamente.
    public void OrderLinesByAlphabetic()
    {
        Console.WriteLine("1) Orden Alfabetico Ascendente");
        Console.WriteLine("2) Orden Alfabetico Descendente");
        int op = Validate.Entero(1,2,"Ingrese una opcion");
        Dictionary<int, string> ordenar;
        if (op == 1) {
            ordenar =this.DiccionarioLineas.OrderBy(x => x.Value.ToLower()).ToDictionary(x => x.Key, y => y.Value);
        } 
        else{ 
            ordenar = this.DiccionarioLineas.OrderByDescending(x => x.Value.ToLower()).ToDictionary(x => x.Key, y => y.Value);
        }
        string[] arrayOrdenado = ordenar.Select(x => $"{x.Key};{x.Value}").ToArray();
        File.WriteAllLines(filePath, arrayOrdenado);
    }
    public void OrderLines()
    {
        Console.WriteLine("1) Ordenar por ID");
        Console.WriteLine("2) Ordenar Alfabeticamente");
        int op = Validate.Entero(1, 2, "Ingrese una opcion");
        if (op == 1) { OrderLinesByID(); }
        else OrderLinesByAlphabetic();
    }
    // Modifica un registro especificado por el usuario segun su ID.
    public void ModifyRegisterByID(int id)
    {
        // Trae el registro relacionado al ID
        string registro = this.DiccionarioLineas[id];
        string[] datos = registro.Split(';');
            // Si no tiene el tamaño necesario, entonces le hacemos un resize.
        if (datos.Length < this.DataTipes.Length)
        {
            Array.Resize(ref datos, this.DataTipes.Length);
        }
        // Imprime para mostrarlo como opciones
        for (int i = 0; i < this.DataTipes.Length; i++) {
            if (datos[i] == null)
            {
                Console.WriteLine($"{i + 1}) {this.DataTipes[i]}");
            }
            else Console.WriteLine($"{i + 1}) {datos[i]} ");
        }
        // Validamos que los datos coincidan con los pedidos por el usuario.
            int op = Validate.Entero(1,this.DataTipes.Length,"Que dato desea modificar?") - 1;
            string currentDateTipe = this.DataTipes[op].Trim();
            switch (currentDateTipe)
            {
                case "nombre":
                    datos[op] = Validate.Texto("Ingrese el nombre");
                    break;
                case "email":
                    datos[op] = Validate.Email("Ingrese el email");
                    break;
                case "nota":
                    datos[op] = Validate.Entero(1,10,"Ingrese la nota").ToString();
                    break;
                case "edad":
                    datos[op] = Validate.Entero(0, 110, "Ingrese la edad").ToString();
                    break;
                case "precio":
                    datos[op] = Validate.Entero(0, 9999999, "Ingrese el precio del producto").ToString();
                    break;
                case "stock":
                    datos[op] = Validate.Entero(0, 9999999, "Ingrese el precio del producto").ToString();
                    break;
                case "asistencia":
                    datos[op] = datos[op] == "Ausente" ? "Presente" : "Ausente";
                    break;
                case "estado":
                    datos[op] = datos[op] == "Incompleto" ? "Completo" : "Incompleto";
                    break;
                default:
                    datos[op] = Validate.Texto("Estamos aca");
                    break;
            }
        registro = string.Join(";", datos);
        this.DiccionarioLineas[id] = registro;
        // Actualizo el registro.
        string[] registroActualizado = DiccionarioLineas.Select(x => $"{x.Key};{x.Value}").ToArray();
        File.WriteAllLines(filePath, registroActualizado);
    }
}
