using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Sort
{
    public class TestGen
    {
        public static void genTest(string filename, int testSize)
        {
            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(filename))
            {
                for (int i = 0; i < testSize; ++i) writer.WriteLine(random.Next());
            }
        }
    }

    public class QuickSort
    {
        private static int Partition(List<int> array, int low, int high)
        {
            int pivot = array[high];
            int lowIndex = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot)
                {
                    ++lowIndex;
                    int temp = array[lowIndex];
                    array[lowIndex] = array[j];
                    array[j] = temp;
                }
            }
            int temp1 = array[lowIndex + 1];
            array[lowIndex + 1] = array[high];
            array[high] = temp1;
            return lowIndex + 1;
        }
        private static void SortRecursive(List<int> array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(array, low, high);
                SortRecursive(array, low, partitionIndex - 1);
                SortRecursive(array, partitionIndex + 1, high);
            }
        }
        public static void Sort(List<int> array)
        {
            QuickSort.SortRecursive(array, 0, array.Count - 1);
        }
    }

    public class FileUtils
    {
        public static List<string> SplitFile(string inFile, int fileLength, int numFiles, string outFolder)
        {
            if (fileLength % numFiles != 0)
            {
                throw new ArgumentException("numFiles must divide fileLength");
            }
            List<string> files = new List<string>(numFiles);
            int smallFileLength = fileLength / numFiles;
            Directory.CreateDirectory("temp");
            using (StreamReader reader = File.OpenText(inFile))
            {
                for (int i = 0; i < numFiles; ++i)
                {
                    string file = String.Format("{0}/temp{1}.txt", outFolder, i);
                    files.Add(file);
                    StreamWriter writer = new StreamWriter(file);
                    for (int j = 0; j < smallFileLength; ++j)
                    {
                        writer.WriteLine(reader.ReadLine());
                    }
                    writer.Close();
                }
            }
            return files;
        }
        public static void SortFile(string inFile, string outFile = null)
        {
            List<int> list = ExtractFile(inFile);
            QuickSort.Sort(list);
            if (outFile == null) outFile = inFile;
            StreamWriter writer = new StreamWriter(outFile);
            foreach (var item in list)
            {
                writer.WriteLine(item);
            }
            writer.Close();
        }
        public static int GetNumFromReader(StreamReader reader)
        {
            string line = reader.ReadLine();
            if (line == null) return int.MaxValue;
            return int.Parse(line);
        }
        public static List<int> ExtractFile(string file)
        {
            StreamReader reader = File.OpenText(file);
            List<int> list = new List<int>();
            while (true)
            {
                int num = GetNumFromReader(reader);
                if (num == int.MaxValue) break;
                list.Add(num);
            }
            reader.Close();
            return list;
        }
        public static bool CheckSorted(string file)
        {
            StreamReader reader = File.OpenText(file);
            int curNum = int.MinValue;
            while (true)
            {
                int num = GetNumFromReader(reader);
                if (num == int.MaxValue) break;
                if (curNum > num) return false;
            }
            reader.Close();
            return true;
        }
    }

    class Program
    {
        private const string inFile = "RawData.txt";
        private const string tempFolder = "temp";
        private const string outFile = "SortedData.txt";
        private const int inFileLength = 1000000000;
        private const int numFiles = 20;

        static void Main()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Generating Test...");
            TestGen.genTest(inFile, inFileLength);
            Console.WriteLine(String.Format("Time Elapsed {0}", stopwatch.Elapsed));

            Console.WriteLine();

            Console.WriteLine("Commence Splitting...");
            List<string> files = FileUtils.SplitFile(inFile, inFileLength, numFiles, tempFolder);
            Console.WriteLine("Split File Successfully.");
            Console.WriteLine(String.Format("Time Elapsed {0}", stopwatch.Elapsed));

            Console.WriteLine();

            files.ForEach(file =>
            {
                FileUtils.SortFile(file);
            });
            Console.WriteLine("Sort All Temp Files Successfully.");
            Console.WriteLine(String.Format("Time Elapsed {0}", stopwatch.Elapsed));

            Console.WriteLine();

            Console.WriteLine("Min Mixing...");
            List<StreamReader> readers = new List<StreamReader>(numFiles);
            List<int> MinVals = new List<int>(numFiles);
            files.ForEach(file =>
            {
                StreamReader reader = File.OpenText(file);
                readers.Add(reader);
                MinVals.Add(FileUtils.GetNumFromReader(reader));
            });
            StreamWriter writer = new StreamWriter(outFile);
            while (true)
            {
                int curMin = MinVals.Min();
                if (curMin == int.MaxValue) break;
                writer.WriteLine(curMin);
                int index = MinVals.IndexOf(curMin);
                MinVals[index] = FileUtils.GetNumFromReader(readers[index]);
            }
            writer.Close();
            readers.ForEach(reader =>
            {
                reader.Close();
            });

            Console.WriteLine("Min Mixing All Temp Files Successfully.");
            Console.WriteLine(String.Format("Time Elapsed {0}", stopwatch.Elapsed));

            Console.WriteLine();

            Console.WriteLine("Cleaning...");
            Directory.Delete(tempFolder, true);
            Console.WriteLine(String.Format("Time Elapsed {0}", stopwatch.Elapsed));

            Console.WriteLine();

            Console.WriteLine("Checking...");
            Console.WriteLine(FileUtils.CheckSorted(outFile) ? "Successfully Sorted" : "Data is not sorted");
            Console.WriteLine(String.Format("Time Elapsed {0}", stopwatch.Elapsed));

            stopwatch.Stop();

        }

    }
}

