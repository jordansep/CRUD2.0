﻿using System;
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
            Mostrar();
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
            ArchiveManipulation Trece = new ArchiveManipulation("nombre,precio,stock");
            Mostrar(Trece);
        }
        public static void EjCatorce()
        {
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
        public static void Mostrar()
        {
            int opciones;
            do
            {
                Console.WriteLine("1) Ejercicio Once");
                Console.WriteLine("2) Ejercicio Doce");
                Console.WriteLine("3) Ejercicio Trece");
                Console.WriteLine("4) Ejercicio Catorce");
                Console.WriteLine("5) Ejercicio Quince.");
                Console.WriteLine("6) Salir.");
                opciones = Validate.Entero(1, 5, "Ingrese una opcion");
                Console.Clear();
                switch (opciones)
                {
                    case 1:
                        EjOnce();
                        break;
                    case 2:
                        EjDoce();
                        break;
                    case 3:
                        EjTrece();
                        break;
                    case 4:
                        EjCatorce();
                        break;
                    case 5:
                        EjQuince();
                        break;
                    case 6: Console.WriteLine("Saliendo del menu..."); break;

                }
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey(); // Pause to let the user see the output
                Console.Clear();
            } while (opciones != 6);
        }

        public static void Mostrar(ArchiveManipulation dataBase)
        {
            int opciones;
            do
            {
                dataBase.ShowLines();
                Console.WriteLine("1) Eliminar por ID");
                Console.WriteLine("2) Agregar Registro");
                Console.WriteLine("3) Modificar Registro");
                Console.WriteLine("4) Ordenar Registros ");
                Console.WriteLine("5) Volver al menu principal.");
                Console.WriteLine("6) Salir.");
                opciones = Validate.Entero(1, 5, "Ingrese una opcion");
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
                    case 4:
                        dataBase.OrderLines();
                        break;
                    case 5: Mostrar(); break;
                    case 6: Console.WriteLine("Saliendo del menu..."); break;
                }
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey(); // Pause to let the user see the output
                Console.Clear();
            } while (opciones != 6);
        }
    }
}
