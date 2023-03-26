using System;
using System.Diagnostics;
using System.IO;

namespace CsvSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = args[1];
            Console.WriteLine($"Input file: {inputFilePath}");
            string outputDirectoryPath = args[2];
            Console.WriteLine($"Output directory: {outputDirectoryPath}");
            const int batchSize = 1000;
            string FileName = Path.GetFileNameWithoutExtension(inputFilePath);
            int fileNumber = 1, batchCount = 0;
            string outputfileFullPath = Path.Combine(outputDirectoryPath, $"{FileName}_{fileNumber}.csv");
            StreamWriter outputwriter = new StreamWriter(outputfileFullPath);
            using (StreamReader inputReader = new StreamReader(inputFilePath))
            {
                // Write the headers to the first output file
                string headers = inputReader.ReadLine();
                outputwriter.WriteLine(headers);
                while (!inputReader.EndOfStream)
                {
                    string line = inputReader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    outputwriter.WriteLine(line);
                    batchCount++;
                    if (batchCount == batchSize)
                    {
                        outputwriter.Close();
                        fileNumber++;
                        outputfileFullPath = Path.Combine(outputDirectoryPath, $"{FileName}_{fileNumber}.csv");
                        outputwriter = new StreamWriter(outputfileFullPath);
                        outputwriter.WriteLine(headers);
                        batchCount = 0;
                        Console.WriteLine($"Batch {fileNumber} done.");
                    }
                }
            }
            outputwriter.Close();
            Console.WriteLine($"Batch {fileNumber} done.");
            Console.WriteLine("Done.");
        }
    }
}


