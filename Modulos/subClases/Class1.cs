/* using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

internal class Alumno:ArchiveManipulation
{
    private string name;
    private string email;
    private string password;
    private List<Alumno> listaAlumnos = new List<Alumno>();
    public Alumno(string name, string email, string password)
    {   
        this.name = string.Empty;
        this.email = string.Empty;
        this.password = string.Empty;
    }
    public void AddRegister()
    {
        Alumno alumno = new Alumno(
            Validate.Texto("Ingrese el nombre del alumno"),
            Validate.Email("Ingrese el email del alumno"),
            Validate.Texto("Ingrese la contraseña del alumno")
        );
        listaAlumnos.Add(alumno);
        string newLine = string.Join(";", this.name, this.email, this.password);
        File.AppendAllText(this.filePath, newLine + Environment.NewLine);
    }

}*/
