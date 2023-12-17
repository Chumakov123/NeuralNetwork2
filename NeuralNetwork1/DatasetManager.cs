using Accord.Math;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork1
{ 
    internal class DatasetManager
    {
        private static Random rnd = new Random();
        public const int imgSize = Program.imgSize;

        public static bool[] img = new bool[imgSize * imgSize];

        public static List<SamplesSet> samplesSets = new List<SamplesSet>();

        public static SamplesSet TrainSet = new SamplesSet();
        public static SamplesSet TestSet = new SamplesSet();

        public static string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\Dataset\\";
        public static string hashfile = "samples.txt";

        public static int FigureCount = 10;
        public static int SetSize = 700;

        private static Controller controller = new Controller();


        public static FigureType currentFigure = FigureType.Undef;

        private static int threshold = 50; 

        public static void CreateDataset() 
        {
            var samples = new SamplesSet();
            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories) 
            {
                currentFigure = Program.folders[Path.GetFileName(directory)];
                string[] files = Directory.GetFiles(directory);
                foreach (string file in files) 
                {
                    img.Clear();
                    using (Bitmap bmp = new Bitmap(Image.FromFile(file)))
                    {
                        controller.processor.ProcessImage(bmp);
                        for (int x = 0; x < imgSize; x++)
                        {
                            for (int y = 0; y < imgSize; y++)
                            {
                                Color newColor = bmp.GetPixel(x, y);
                                if (newColor.R < threshold || newColor.G < threshold || newColor.B < threshold)
                                {
                                    img[x * imgSize + y] = true;
                                }
                            }
                        }
                        samples.AddSample(LineSum(true));
                    }
                }
            }
            Debug.WriteLine("Датасет создан");
            SaveSamples(samples);
            LoadDataset();
        }
        public static void SaveSamples(SamplesSet samples)
        {
            string pathFile = path + hashfile;
            using (StreamWriter sw = File.CreateText(pathFile))
            {
                foreach (Sample sample in samples)
                {
                    string inputText = string.Join(" ", sample.input.Select(i => i.ToString()));
                    string text = $"{Enum.Format(typeof(FigureType), sample.actualClass, "D")};{inputText}";
                    sw.WriteLine(text);
                }
            }
            Debug.WriteLine("Датасет сохранен");
        }
        public static void LoadDataset()
        {
            SamplesSet samples = new SamplesSet();
            string pathFile = path + hashfile;
            string[] lines = File.ReadAllLines(pathFile);
            
            samplesSets = new List<SamplesSet>();
            for (int i = 0; i < 10; ++i)
            {
                samplesSets.Add(new SamplesSet());
            }

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (Int32.Parse(parts[0]) >= FigureCount)
                    continue;
                double[] input = Array.ConvertAll(parts[1].Split(' '), double.Parse);

                currentFigure = (FigureType)Int32.Parse(parts[0]);
                samplesSets[(int)currentFigure].AddSample(new Sample(input, FigureCount, currentFigure));
            }
            Debug.WriteLine("Датасет загружен");
            GenerateSets();
        }
        public static void GenerateSets()
        {
            TrainSet = new SamplesSet();
            TestSet = new SamplesSet();

            int minCnt = int.MaxValue;
            for (int i = 0; i < FigureCount; ++i)
            {
                if (samplesSets[i].samples.Count < minCnt)
                    minCnt = samplesSets[i].samples.Count;
            }
            int n = SetSize / FigureCount;
            if (n > minCnt)
                n = minCnt;
            n /= 2;

            for (int i = 0; i < FigureCount; ++i)
            {
                samplesSets[i].samples.Shuffle();

                TrainSet.samples.AddRange(samplesSets[i].samples.Take(n).ToList());
                TestSet.samples.AddRange(samplesSets[i].samples.Skip(n).Take(n).ToList());
            }
            Debug.WriteLine("Сгенерированы обучающий и тестовый наборы");
        }
        private static Sample LineSum(bool created = false)
        {
            double[] inputSum = new double[imgSize * 2];

            for (int x = 0; x < imgSize; x++)
                for (int y = 0; y < imgSize; y++)
                {
                    if (!img[x * imgSize + y])
                    {
                        inputSum[x] += 1;
                        inputSum[imgSize + y] += 1;
                    }
                }

            return created? new Sample(inputSum, 10, currentFigure) : 
                new Sample(inputSum, FigureCount, currentFigure);
        }
    }
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
