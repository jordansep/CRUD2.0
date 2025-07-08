using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

internal class ArchiveManipulation
{
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

    // Ya que todos tendran ID, haremos que las clases creadas hereden este atributo
    public ArchiveManipulation() {
        this.id = 1;
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
                if (j == 0) { 
                    Console.Write($"ID: {datos[j]} - ");
                    int.TryParse( datos[j], out this.id );
                }
                else if(j == datos.Length - 1) { Console.Write(datos[j]);}
                else Console.Write(datos[j] + " - ");
            }
            Console.WriteLine();
        }
    }
    public void OrderLines()
    {
        Dictionary<int, string> Identificar = new Dictionary<int, string>();
        foreach (var item in this.lines)
        {
            string[] separar = item.Split(';');
            int.TryParse(separar[0], out int id);
            Identificar.Add(id, string.Join(";", separar.Skip(1)));
        }
        // Nuevo diciconario con todo ordenado
        Dictionary<int, string> ordenar = Identificar.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
        // Array ordenado                               Vuelvo a unir la ID al Value, con el formato anterior.
        string[] arrayOrdenado = ordenar.Select(x => $"{x.Key};{x.Value}").ToArray();
        File.WriteAllLines(filePath, arrayOrdenado);
    }
    private int GetUniqueID()
    {
        return this.id++;
    }
    
}
