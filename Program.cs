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
            nuevaDB.OrderLines();
            nuevaDB.ShowLines();

        }
    }
}
