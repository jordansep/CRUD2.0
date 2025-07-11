using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class ManipulationPath
{
    private static string directoryPath;
    public static string filePath { get; private set; }
    public static string folderPath { get; private set; }
    public static string secondFilePath { get; private set; }
    
    public static void InitializePath(string folderName, string fileName)
    {
        directoryPath = Directory.GetCurrentDirectory();
        folderPath = Path.Combine(directoryPath, folderName);
        filePath = Path.Combine(folderPath, fileName);
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        if (!File.Exists(filePath)) File.Create(filePath);
    }
    public static void InitializePath(string folderName, string fileName, string secondFileName)
    {
        directoryPath = Directory.GetCurrentDirectory();
        folderPath = Path.Combine(directoryPath, folderName);
        filePath = Path.Combine(folderPath, fileName);
        secondFilePath = Path.Combine(folderPath, secondFileName);
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        if (!File.Exists(filePath)) File.Create(filePath).Close();
        if (!File.Exists(secondFilePath)) File.Create(secondFilePath).Close(); 
    }
    public string GetFolderPath() => folderPath;
    public string GetFilePath() => filePath;

}
