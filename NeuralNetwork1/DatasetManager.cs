using System;
using System.Collections.Generic;
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

        public static SamplesSet trainSamples = new SamplesSet();
        public static SamplesSet testSamples = new SamplesSet();
        private static Controller controller = new Controller();

        private static void ClearImage()
        {
            for ( int i = 0; i < imgSize; ++i )
                for (int j = 0; j < imgSize; ++j)
                    img[i * imgSize + j] = false;
        }
        public static void LoadDataset()
        {
            var path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\Dataset\\";
        }
        public static void CreateDataset() 
        {
            var path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\Dataset\\";
            string[] directories = Directory.GetDirectories(path);
            foreach ( string directory in directories )
            {
                string[] files = Directory.GetFiles(directory);
                foreach ( string file in files )
                {
                    ClearImage();
                    Bitmap bmp = new Bitmap(Image.FromFile(file));
                    controller.processor.ProcessImage(bmp);

                }
            }
        }
    }
}
