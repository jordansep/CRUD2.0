using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRUD2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ManipulationPath.InitializePath("Contenedor", "Archivo.txt");
            ArchiveManipulation nuevaDB = new ArchiveManipulation();
            nuevaDB.ShowLines();
            Console.WriteLine("1) Eliminar por ID");
            Console.WriteLine("2) Agregar Registro");
            Console.WriteLine("3) Modificar Registro");
            Console.WriteLine("5) Ordenar Registros ");
            int opciones = Validate.Entero("Ingrese una opcion");
            switch (opciones)
            {
                case 1: nuevaDB.EliminarID(Validate.Entero("Ingrese la ID.")); 
                    break;
                case 2: nuevaDB.AddRegister(Validate.Entero("Cuantos datos necesitas? (Sin contar el campo: ID)"));
                    break;
                case 3: nuevaDB.ModifyRegisterByID(Validate.Entero("Ingrese la ID donde desea modificar los datos"));
                    break;
                case 5: nuevaDB.OrderLines(); break;
            }
            
        }
    }
}
