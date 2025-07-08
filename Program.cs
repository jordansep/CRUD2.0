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
            string texto = Validate.Email();
            Console.WriteLine(texto);
        }
    }
}
