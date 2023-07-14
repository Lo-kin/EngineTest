using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Maths;
using VAO;
using System.Collections;
using System.Numerics;
using System.Drawing;

namespace EngineTest
{
    class Graphic
    {
        private static IWindow window;
        private static GL Gl;

        //Our new abstracted objects, here we specify what the types are.
        private static BufferObject<float> Vbo;
        private static BufferObject<uint> Ebo;
        private static VertexArrayObject<float, uint> Vao;
        private static Shader Shader;

        public static float[] VertexArray =
        {
            
            //X    Y      Z     R  G  B  A
             0.5f,  0.5f, 0.0f, 1, 0, 1, 0.5f,
             0.5f, -0.5f, 0.0f, 0, 1, 0, 0.5f,
            -0.5f, -0.5f, 0.0f, 1, 0, 1, 0.5f,
            -0.5f,  0.5f, 0.0f, 0, 1, 0, 0.5f,
            
             0.1f,  0.1f, 0.0f, 1, 1, 1, 0.5f,
             0.1f, -0.1f, 0.0f, 1, 1, 1, 0.5f,
            -0.1f, -0.1f, 0.0f, 1, 1, 1, 0.5f,
            -0.1f,  0.1f, 0.0f, 1, 1, 1, 0.5f,
        };

        private static readonly uint[] Indices =
        {
            0, 1, 3,
            1, 2, 3,
            4, 5, 7,
            5 , 6,7,
        };

        public static int[] RenderObjects =
        {
            0,
        };

        public static void Start()
        {
            EngCore.Loger(1, "Graphic Thread Start");
            GrapicAttribute GA = new GrapicAttribute();

            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(GrapicAttribute.ScreenWidth, GrapicAttribute.ScreenHeight);
            options.Title = "Texture";
            window = Window.Create(options);
            window.FramesPerSecond = 60;
            window.Load += OnLoad;
            window.Render += OnRender;
            window.Closing += OnClose;
            
            window.Run();
            window.Dispose();
        }


        private static void OnLoad()
        {
            IInputContext input = window.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                input.Keyboards[i].KeyDown += KeyDown;
            }
            
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                input.Keyboards[i].KeyUp += KeyUp;
            }

            Gl = GL.GetApi(window);
            Gl.ClearColor(Color.CornflowerBlue);

            Ebo = new BufferObject<uint>(Gl, Indices, BufferTargetARB.ElementArrayBuffer);
            Vbo = new BufferObject<float>(Gl, VertexArray, BufferTargetARB.ArrayBuffer);
            Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);

            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 7, 0);
            Vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 7, 3);
            Shader = new Shader(Gl, "shader.vert", "shader.frag");
            EngCore.Loger(1, "Graphic Loaded");

        }

        private static unsafe void OnRender(double obj)
        {
            Gl.Clear((uint)ClearBufferMask.ColorBufferBit);
            Vao.Bind();
            Shader.Use();
            Gl.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);

        }

        private static void OnClose()
        {
            //Remember to dispose all the instances.
            Vbo.Dispose();
            Ebo.Dispose();
            Vao.Dispose();
            Shader.Dispose();
            EngCore.IsGraphicExit = true;
        }


        private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
        {
            if (arg2 == Key.Enter)
            {
                bool IsTalk = true;
            }
            /*
            if (IsUseMenu)
            {
                if (IsStartMenu)
                {

                }
                if (IsGameMenu)
                {

                }
                if (IsInput)
                {

                }
            }
            */
            else
            {
                if (arg2 == Key.Escape)
                {
                    window.Close();
                }
            }
        }

        private static void KeyUp(IKeyboard arg1, Key arg2, int arg3)
        {
            if (arg2 == Key.Enter)
            {
                Console.WriteLine("Test");
            }
        }
    }

    class GrapicAttribute
    {


        //  unit:px 
        static public int ScreenWidth = 800;
        static public int ScreenHeight = 600;

        static float BlockWidth = 64f;
        static float BlockHeight = 64f;

        public int[] GetLayPt()
        {

            int StdBlockPerW = (int)Math.Ceiling(ScreenWidth / BlockWidth) + 2;
            int StdBlockPerH = (int)Math.Ceiling(ScreenHeight / BlockHeight) + 2;

            int[] GrpData = new int[] { StdBlockPerW, StdBlockPerH };
            return GrpData;
        }

        public float[] GetBlockPt()
        {
            float StdBlockPerW = BlockWidth / ScreenWidth;
            float StdBlockPerH = BlockHeight / ScreenHeight;
            return new float[] { StdBlockPerW, StdBlockPerH };
        }

        public float[] GetStdVertices()
        {
            float StdBlockPerW = BlockWidth / ScreenWidth;
            float StdBlockPerH = BlockHeight / ScreenHeight;
            float[] StdVertices =
            {
             // X                Y                   Z     R      G      B      A
                -1+StdBlockPerW , 1.0f             , 0.0f, 1.0f , 0.0f , 0.0f , 1.0f ,
                -1+StdBlockPerW , 1-StdBlockPerH   , 0.0f, 0.0f , 1.0f , 0.0f , 1.0f ,
                -1.0f           , 1-StdBlockPerH   , 0.0f, 0.0f , 0.0f , 1.0f , 1.0f ,
                -1.0f           , 1.0f             , 0.0f, 1.0f , 0.0f , 0.0f , 1.0f ,

            };
            return StdVertices;
        }


    }

    class EngCore
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
                Thread GUI = new Thread(new ThreadStart(Graphic.Start));
                GUI.Name = "Graphic";
                GUI.Start();
                
            }

            Thread BGCore = new Thread(new ThreadStart(OnLoad));
            BGCore.Name = "Engine";
            //BGCore.Start();
            
        }

        public static void Loger(int State , string msg)
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

        private static void OnLoad()
        {
            Loger(1, "CoreEngine Thread Start");
            GrapicAttribute GA = new GrapicAttribute();
            int[] LayPPt = GA.GetLayPt();

            float[] StdBP = GA.GetBlockPt();
            int time = 0;

            //0 正常, 1 错误
            int ExitCode = 1;

            int[] tmpObj = new int[LayPPt[0] * LayPPt[1]];
            float[] tmpVet = new float[LayPPt[0] * LayPPt[1] * 28];

            for (int x = 0; x <= LayPPt[0] * LayPPt[1] - 1; x++)
            {
                Random r = new Random();
                tmpObj[x] = r.Next(0, 20);
                    
            }
            Graphic.RenderObjects = tmpObj;

            float[] StdVertices =
            {
             // X                 Y           Z     R      G      B      A
                -1.0f             , 1.0f + StdBP[1] , 0.0f, 1.0f , 0.0f , 0.0f , 1.0f ,
                -1.0f             , 1.0f            , 0.0f, 0.0f , 1.0f , 0.0f , 1.0f ,
                -1.0f - StdBP[0]  , 1.0f            , 0.0f, 0.0f , 0.0f , 1.0f , 1.0f ,
                -1.0f - StdBP[0]  , 1.0f + StdBP[1] , 0.0f, 1.0f , 0.0f , 0.0f , 1.0f ,

            };

            for (int j = 0; j <= LayPPt[0] * LayPPt[1] - 1; j++)
            {
                tmpVet[0 + j * 28] += StdVertices[0] + (StdBP[0] * j);
                tmpVet[7 + j * 28] += StdVertices[0] + (StdBP[0] * j);
                tmpVet[14 + j * 28] += StdVertices[0] + (StdBP[0] * j);
                tmpVet[21 + j * 28] += StdVertices[0] + (StdBP[0] * j);

                tmpVet[1 + j * 28] += StdVertices[1] + (StdBP[1] * j);
                tmpVet[8 + j * 28] += StdVertices[1] + (StdBP[1] * j);
                tmpVet[15 + j * 28] += StdVertices[1] + (StdBP[1] * j);
                tmpVet[22 + j * 28] += StdVertices[1] + (StdBP[1] * j);       

            }
            Graphic.VertexArray = tmpVet;
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
                    Loger(1 , "> w <");
                }

                Thread.Sleep(500);
            }
            Loger(1, "ExitCode With :" + ExitCode);
        }
    }
}
