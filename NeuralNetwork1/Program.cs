using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NeuralNetworksStand(new Dictionary<string, Func<int[], BaseNetwork>>
            {
                // Тут можно добавить свои нейросети
                {"Accord.Net Perseptron", structure => new AccordNet(structure)},
                {"Студентческий персептрон", structure => new StudentNetwork(structure)},
            }));
        }
        //Размер обработанного изображения
        public const int imgSize = 48;

        public static Dictionary<FigureType, string> titles = new Dictionary<FigureType, string>
        { 
            {FigureType.Play, "Старт"},
            {FigureType.Pause, "Пауза"},
            {FigureType.Stop, "Стоп"},
            {FigureType.Rec, "Запись"},
            {FigureType.SpeedUp, "Перемотка вперед"},
            {FigureType.SpeedDown, "Перемотка назад"},
            {FigureType.SkipForward, "Следующие видео"},
            {FigureType.SkipBackward, "Предыдущее видео"},
            {FigureType.NextFrame, "Следующий кадр"},
            {FigureType.PrevFrame, "Предыдущий кадр"},
        };
        public static Dictionary<string, FigureType> folders = new Dictionary<string, FigureType>
        {
            { "play", FigureType.Play},
            { "pause", FigureType.Pause },
            { "stop", FigureType.Stop },
            { "rec", FigureType.Rec },
            { "speed_up", FigureType.SpeedUp },
            { "speed_down", FigureType.SpeedDown  },
            { "skip_backward", FigureType.SkipBackward },
            { "skip_forward", FigureType.SkipForward },
            { "frame_next", FigureType.NextFrame },
            { "frame_prev" , FigureType.PrevFrame}
        };
    }
}