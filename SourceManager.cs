using System;
using System.IO;
namespace OptiHeatPro
{
    public class HeatingData
    {
        public static void Read()
        {
            string path = "datacsv.csv";
            StreamReader reader = null;
            if(File.Exists(path))
            {
                reader = new StreamReader(File.OpenRead(path));
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var data = line.Split(';');
                    foreach(var item in data)
                    {
                        Console.Write(item + " ");
                    }
                    Console.WriteLine();
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }

        }
    }

}