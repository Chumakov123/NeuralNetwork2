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
        static Dictionary<FigureType, string> names = new Dictionary<FigureType, string>
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
    }
}