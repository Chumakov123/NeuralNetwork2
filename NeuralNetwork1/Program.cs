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
        public static Dictionary<FigureType, string> folders = new Dictionary<FigureType, string>
        {
            { FigureType.Play, "play"},
            { FigureType.Pause, "pause" },
            { FigureType.Stop, "stop" },
            { FigureType.Rec, "rec" },
            { FigureType.SpeedUp, "speed_up" },
            { FigureType.SpeedDown, "speed_down" },
            { FigureType.SkipBackward, "skip_backward" },
            { FigureType.SkipForward, "skip_forward" },
            { FigureType.NextFrame, "frame_next" },
            { FigureType.PrevFrame, "frame_prev" }
        };
    }
}