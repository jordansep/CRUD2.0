using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

internal class ArchiveManipulation
{
    // TODO: NombrarArchivos
    // TODO: Modificacion de datos --
    // TODO: Tipos de datos que reciben.
    // Ej: al poner 4 datos, especificar que tipo de datos van a recibir cada uno
    // Nombre(string): Jordan, Email(email): jordanmonier4@gmail.com, edad(numero): 24
    // Entonces al modificar los datos, pedir el tipo de dato necesario.

    // Obtenemos los datos necesarios para comenzar a trabajar con archivos.
    private string folderPath = ManipulationPath.folderPath;
    private string filePath = ManipulationPath.filePath;
    // Las lineas se agregaran en formato |id;dato;dato;dato;dato|
    private string[] lines
    {
        get { return File.ReadAllLines(filePath); }
    }
    // Todos los ejercicios me solicitan ID, ademas siempre es bueno tener un identificador unico
    private int id;
    private Dictionary<int, string> DiccionarioLineas = new Dictionary<int, string>();
    // Ya que todos tendran ID, haremos que las clases creadas hereden este atributo
    public ArchiveManipulation() {
        this.id = 1;
        LlenarDiccionario();
    }

    // Este metodo funciona, pero para pedir datos especificos como por ej: Email, Numeros, no podra ser validado desde aqui.
    // Por lo tanto tendremos que sobreescribirlo en las subclases
    public void AddRegister(int cantDatos)
    {
        // Sumamos uno ya que la ID ya esta contada.
        cantDatos += 1;
        // Inicializamos con un array de tamaño cantDatos
        string[] registro = new string[cantDatos];
        // El primer dato es la ID asi que no la pedimos
        // mientras que los siguientes datos los tendra que ingresar el usuario.
        for (int i = 0; i < registro.Length; i++) { 
            if (i == 0) { registro[i] = GetUniqueID().ToString(); }
            if (i != 0) { registro[i] = Validate.Texto();}
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

        for (int i = 0; i < this.lines.Length; i++)
        {
            if (this.lines[i] == null) {
                Console.WriteLine(this.lines[i]);
                Console.WriteLine("Quisiera agregar alguna?");
            }
            string[] datos = this.lines[i].Split(';');
            // Daremos formato sin importar cuantos datos sean guardados en las lineas.
            for (int j = 0; j < datos.Length; j++) {
                if (j == 0) Console.Write($"ID: {datos[j]} - "); 
                else if(j == datos.Length - 1) Console.Write(datos[j]);
                else Console.Write(datos[j] + " - ");
            }
            Console.WriteLine();
        }
    }
    // Llena un diccionario que luego sera usado
    // para operaciones donde necesitemos buscar usuarios por ID.
    private void LlenarDiccionario()
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
    private int GetUniqueID()
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
    public void OrderLines()
    {
        // Nuevo diciconario con todo ordenado
        Dictionary<int, string> ordenar = this.DiccionarioLineas.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
        // Array ordenado                               Vuelvo a unir la ID al Value, con el formato anterior.
        string[] arrayOrdenado = ordenar.Select(x => $"{x.Key};{x.Value}").ToArray();
        File.WriteAllLines(filePath, arrayOrdenado);
    }
    // Modifica un registro especificado por el usuario segun su ID.
    public void ModifyRegisterByID(int id)
    {
        // Trae el registro relacionado al ID
        string registro = this.DiccionarioLineas[id];
        string[] datos = registro.Split(';');
        // Imprime para mostrarlo como opciones
        for (int i = 0; i < datos.Length; i++) {
            Console.WriteLine($"{i + 1}) {datos[i]} ");
        }
        int op = Validate.Entero("Que dato desea modificar?") - 1;
        datos[op] = Validate.Texto("Ingrese el nuevo valor");
        registro = string.Join(";", datos);
        this.DiccionarioLineas[id] = registro;
        // Actualizo el registro.
        string[] registroActualizado = DiccionarioLineas.Select(x => $"{x.Key};{x.Value}").ToArray();
        File.WriteAllLines(filePath, registroActualizado);
    }
    
}
