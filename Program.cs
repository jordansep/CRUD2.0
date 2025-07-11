using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace CRUD2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EjCatorce();
        }
        public static void EjOnce()
        {
            ManipulationPath.InitializePath("Data", "usuarios.txt");
            ArchiveManipulation Once = new ArchiveManipulation("nombre,email,edad");
            Mostrar(Once);
        }
        public static void EjDoce()
        {
            ManipulationPath.InitializePath("Tareas", "tareas.txt");
            ArchiveManipulation Doce = new ArchiveManipulation("texto, estado");
            Mostrar(Doce);
        }
        public static void EjTrece()
        {
            ManipulationPath.InitializePath("Inventario", "inventario.csv");
            ArchiveManipulation Trece = new ArchiveManipulation("texto,precio,stock");
            Mostrar(Trece);
        }
        public static void EjCatorce()
        {
            Console.WriteLine($"Registro de Asistencias del dia {DateTime.UtcNow.ToString("yyyy-MM-dd")}");
            ManipulationPath.InitializePath("Asistencias", $"{DateTime.UtcNow.ToString("yyyy-MM-dd")}.txt", "Alumnos.txt");
            Asistencia Catorce = new Asistencia("nombre,asistencia");
            Catorce.Mostrar(Catorce);
        }
        public static void EjQuince()
        {
            ManipulationPath.InitializePath("Registro de Notas", "notas.txt");
            ArchiveManipulation Quince = new ArchiveManipulation("nombre,nota,nota,nota");
            Mostrar(Quince);
        }
        public static void Mostrar(ArchiveManipulation dataBase)
        {
            dataBase.ShowLines();
            Console.WriteLine("1) Eliminar por ID");
            Console.WriteLine("2) Agregar Registro");
            Console.WriteLine("3) Modificar Registro");
            Console.WriteLine("4) Ordenar Registros ");
            int opciones = Validate.Entero("Ingrese una opcion");
            switch (opciones)
            {
                case 1:
                    dataBase.EliminarID(Validate.Entero("Ingrese la ID"));
                    break;
                case 2:
                    dataBase.AddRegister();
                    break;
                case 3:
                    dataBase.ModifyRegisterByID(Validate.Entero("Ingrese la ID donde desea modificar los datos"));
                    break;
                case 4: dataBase.OrderLines(); break;
            }
        }
    }
}
