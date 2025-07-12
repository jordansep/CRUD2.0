using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// Borrar cualquiera de estos metodos podria romper una que otra funcion.
public class Validate
{
    public static string Texto(string msg = "Ingrese una cadena de texto")
        {
            bool esTexto = false;
            string respuesta = "";
            do
            {
                try
                {
                    Console.Write(msg + ": ");
                    respuesta = Console.ReadLine();
                    if (string.IsNullOrEmpty(respuesta)) throw new ArgumentNullException($"El texto no puede ser nulo");
                    if (string.IsNullOrWhiteSpace(respuesta)) throw new ArgumentNullException($"El texto no puede ser un espacio");
                    esTexto = true;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("El texto no puede ser nulo");
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Error en el formato {respuesta}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            } while (!esTexto);
            return respuesta;
        }

    public static string Directorio(string msg = "Ingrese la ruta donde trabajaremos")
        {
            bool esTexto = false;
            string respuesta = "";
            do
            {
                try
                {
                    Console.Write(msg + ": ");
                    respuesta = Console.ReadLine();
                    if (!Directory.Exists(respuesta)) throw new DirectoryNotFoundException($"El directorio: {respuesta}\n    No fue encontrado");
                    if (string.IsNullOrWhiteSpace(respuesta)) throw new ArgumentNullException($"El directorio no puede ser nulo");
                    esTexto = true;
                }
                catch (ArgumentNullException) { }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            } while (!esTexto);
            return respuesta;
        }

    // Valida numeros enteros.
    public static int Entero(string msg = "Ingrese un entero")
        {
            bool esN = false;
            int numero = 0;
            do
            {
                Console.Write(msg + ": ");
                string n = Console.ReadLine();
                try
                {
                    if (!int.TryParse(n, out numero))
                        throw new Exception("Ingrese un numero valido.");
                    esN = true;
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (!esN);
            return numero;
        }

    // Sobrescribir el metodo para que use uno acorde a lo necesario
    // En caso de que validar entero solo necesite ser llamado y/o pasarle el string usa el metodo que contiene el parametro string
    // En caso de querer agregar un rango entre dos numeros, llamara a el que contenga esos parametros extra
    public static int Entero(int min, int max, string msg = "Ingrese un entero")
        {
            bool esN = false;
            int numero = 0;
            do
            {
                Console.Write(msg + ": ");
                string n = Console.ReadLine();
                try
                {
                    if (!int.TryParse(n, out numero))
                        throw new Exception("Ingrese un numero valido.");
                    // Extra: Si numero es menor que min o mayor que max, devolver un OverflowException.
                    if (numero < min || numero > max)
                        throw new OverflowException($"Ingrese un número entre {min} y {max} (inclusive).");

                    esN = true;
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (!esN);
            return numero;
        }
    public static float Flotante(float min, float max, string msg = "Ingrese un entero")
    {
        bool esN = false;
        float numero = 0;
        do
        {
            Console.Write(msg + ": ");
            string n = Console.ReadLine();
            try
            {
                if (!float.TryParse(n,NumberStyles.Float, CultureInfo.InvariantCulture, out numero))
                    throw new Exception("Ingrese un numero valido.");
                // Extra: Si numero es menor que min o mayor que max, devolver un OverflowException.
                if (numero < min || numero > max)
                    throw new OverflowException($"Ingrese un número entre {min} y {max} (inclusive).");

                esN = true;
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } while (!esN);
        return numero;
    }
    public static string Email(string msg = "Ingrese su email")
        {
        string reg = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
        Regex policiaDelMail = new Regex(reg, RegexOptions.IgnoreCase);
        string email = "";
        bool esMail = false;
        do
        {
            Console.Write(msg + ": ");
                email = Console.ReadLine();
            try
            {
                if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException($"El email no puede ser nulo");
                
                if (policiaDelMail.IsMatch(email))
                {
                    esMail = true;
                    
                }
                else Console.WriteLine("El mail debe contener (alfanumerico@texto.texto)");

            }
            catch (FormatException)
            {
                Console.WriteLine($"Error en el formato {email}");
            }
            catch (ArgumentNullException) { }
            catch (Exception) { Console.WriteLine("Email Invalido"); }

            } while (!esMail);
            return email;
        }
    public static string Fecha(string msg = "Ingrese una fecha")
    {
        string[] formatosAceptados = new string[]
        {
            "yyyy-MM-dd",    // Ej: 2025-08-20
            "yyyy/MM/dd",    // Ej: 2025/08/20
            "dd-MM-yyyy",    // Ej: 20-08-2025
            "dd/MM/yyyy",    // Ej: 20/08/2025
            "M/d/yyyy",      // Ej: 8/20/2025
            "M-d-yyyy",      // Ej: 8-20-2025
            "MM/dd/yyyy",    // Ej: 08/20/2025
            "MM-dd-yyyy",    // Ej: 08-20-2025
        };

        DateTime fechaParseada;
        bool fechaValida = false;
        string fechaIngresada;

        do
        {
            Console.WriteLine($"{msg} (ej. 2025-08-20 o 20/08/2025):");
            fechaIngresada = Console.ReadLine();
            fechaValida = DateTime.TryParseExact(
                fechaIngresada,
                formatosAceptados,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out fechaParseada
            );

            if (!fechaValida)
            {
                Console.WriteLine("La fecha ingresada no es válida o no está en un formato reconocido. Por favor, inténtelo de nuevo.");
            }

        } while (!fechaValida);

        // Una vez que se tiene una fecha válida (fechaParseada), se formatea
        // a la salida deseada "yyyy-MM-dd".
        return fechaParseada.ToString("yyyy-MM-dd");
    }
}
