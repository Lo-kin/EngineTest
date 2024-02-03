using System.Collections;
using System.Numerics;

namespace Cyan.Engine
{
    class Cyan
    {
        static public bool IsGraphicExit = false;
        static public bool IsGameStop = false;
        static public bool IsLoaded = false;

        static public ArrayList LogSave = new ArrayList();

        private static void Main(string[] args)
        {
            string pattern = @"";
            bool Console = false;
            Loger(1, "Arg:" + args);
            foreach (string x in args)
            {
                if (x == "console")
                {
                    Console = true;
                }
            }
            if (Console == false)
            {
                OnLoad();
                Thread GUI = new Thread(new ThreadStart(Graphic.Start));
                GUI.Name = "Graphic";
                GUI.Start();

            }

            Thread CyanCore = new Thread(new ThreadStart(Engine));
            CyanCore.Name = "Engine";
            CyanCore.Start();

        }

        private static void OnLoad()
        {
            Rectangle Rect = new Rectangle();
            Transform[] Transforms = new Transform[65536];
            float ScaleWidth = Rect.Width / (GrapicAttribute.ScreenWidth / 2);
            float ScaleHeight = Rect.Height / (GrapicAttribute.ScreenHeight / 2);
            float WidthPer = Rect.Width / GrapicAttribute.ScreenWidth ;
            float HeightPer = Rect.Height / GrapicAttribute.ScreenHeight ;

            for (int y = 0; y <= 15; y ++)
            {
                for (int x = 0; x <= 15; x ++)
                {
                    Transforms[y * 16 + x] = new Transform();
                    Transforms[y * 16 + x].Position = new Vector3(Rect.Width * x * WidthPer * 800, Rect.Height * y * HeightPer * 600, (y * 16 + x) * 0.00f);
                    Transforms[y * 16 + x].Scales = new float[] { 0.1f, 0.1f };
                }

            }
            Graphic.Transforms = Transforms;
        }

        private static void Engine()
        {
            Loger(1, "CoreEngine Thread Start");

            object[] LoadObject = new object[] { };
            int time = 0;
            int ExitCode = 1;
            IsLoaded = true;

            while (true)
            {
                time++;
                if (IsGraphicExit)
                {
                    ExitCode = 0;
                    break;

                }
                if (IsGameStop)
                {
                    Thread.Sleep(20000);
                }

                if (time % 20 == 0)
                {
                    Loger(1, "> w <");
                }
                Console.WriteLine(Graphic.CameraPosition.Z);
                Thread.Sleep(500);
            }
            Loger(1, "ExitCode With :" + ExitCode);
        }
        public static void Loger(int State, string msg)
        {

            if (State == 1)
            {
                string OutLog = @"INFO " + DateTime.Now + " " + msg;
                LogSave.Add(OutLog);
                Console.WriteLine(OutLog);
            }
            else if (State == 2)
            {
                string OutLog = @"WARN " + DateTime.Now + " " + msg;
                LogSave.Add(OutLog);
                Console.WriteLine(OutLog);
            }
            else if (State == 3)
            {
                string OutLog = @"ERROR " + DateTime.Now + " " + msg;
                LogSave.Add(OutLog);
                Console.WriteLine(OutLog);
            }
            else
            {
                string OutLog = @"Unexcepted Info " + DateTime.Now + " " + msg;
                LogSave.Add(OutLog);
                Console.WriteLine(OutLog);
            }
        }


    }
    /*
    public sealed class Worker
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger) =>
            _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);
                await Task.Delay(1_000, stoppingToken);
            }
        }
    }
    */
}