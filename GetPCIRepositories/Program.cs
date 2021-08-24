using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GetPCIRepositories
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = Path.Combine(Environment.CurrentDirectory.ToString(),"input.txt");
            Console.WriteLine(inputPath);
            string outputPath = Path.Combine(Environment.CurrentDirectory.ToString(), "input.csv");
            using (StreamReader Reader =new StreamReader(inputPath))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                string vendorCode = "";
                string vendorName = "";
                string deviceCode = "";
                string deviceName = "";
                string subvendorCode = "";
                string subvendorName = "";
                using (StreamWriter Writer =new StreamWriter(outputPath))
                {
                    Writer.WriteLine("Level,vendorCode,vendorName,deviceCode,deviceName,subvendorCode,subvendorName");
                    try
                    {
                        while ((line = Reader.ReadLine()) != null)
                        {
                            //Skipthe C class because it use a different type of pattern
                            if (line.Contains("# C class	class_name")) break;
                            
                            if (line.Length > 0 && line[0] != '#')
                            {
                                if (line.Length > 0 && line[1] == '\t')
                                {
                                    line = line.Replace("\t", "").Replace("\"","'");
                                    subvendorCode = line.Substring(0, 9);
                                    subvendorName = line.Substring(11, line.Length - 11);

                                    Writer.WriteLine("\"3\",\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"", vendorCode, vendorName, deviceCode, deviceName, subvendorCode, subvendorName);
                                    Console.WriteLine("3,{0},{1},{2},{3},{4},{5}", vendorCode, vendorName, deviceCode, deviceName, subvendorCode, subvendorName);

                                    //Console.WriteLine("Level 3: " + line.Replace("\t", ""));

                                }
                                else if (line[0] == '\t')
                                {
                                    line = line.Replace("\t", "").Replace("\"", "'");

                                    deviceCode = line.Substring(0, 4);
                                    deviceName = line.Substring(6, line.Length - 6);
                                    Writer.WriteLine("\"2\",\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"N/A\",\"N/A\"", vendorCode, vendorName, deviceCode, deviceName);
                                    Console.WriteLine("2,{0},{1},{2},{3},N/A,N/A", vendorCode, vendorName, deviceCode, deviceName);

                                }
                                else
                                {
                                    vendorCode = line.Substring(0, 4);
                                    vendorName = line.Substring(6, line.Length - 6);
                                    //Console.WriteLine("Level 1: " + line);
                                    Writer.WriteLine("\"1\",\"{0}\",\"{1}\",\"N/A\",\"N/A\",\"N/A\",\"N/A\"", vendorCode, vendorName);
                                    Console.WriteLine("1,{0},{1},N/A,N/A,N/A,N/A", vendorCode, vendorName);

                                }

                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Read File ERROR:");
                        Console.WriteLine(ex.Message.ToString());
                    }
                    

                }
            }

        }
    }
}
