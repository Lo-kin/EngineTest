using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using Silk.NET.Maths;
using VAO;
using Controls;
using System.Timers;
using System.Collections;


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
        

        public static float[] Vertices =
        {
         // X      Y      Z     R  G  B  A
             0.5f,  0.5f, 0.0f, 1, 0, 0, 0,
             0.5f, -0.5f, 0.0f, 0, 1, 0, 0,
            -0.5f, -0.5f, 0.0f, 0, 0, 1, 0,
            -0.5f,  0.5f, 0.5f, 0, 0, 0, 1
        };

        private static readonly uint[] Indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        public static void Start()
        {
            GrapicAttribute GA = new GrapicAttribute();

            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(GrapicAttribute.ScreenWidth, GrapicAttribute.ScreenHeight);
            options.Title = "Test";
            window = Window.Create(options);
            window.FramesPerSecond = 60;
            window.Load += OnLoad;
            window.Render += OnRender;
            window.Closing += OnClose;
            window.Update += OnUpdate;
            
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

            //Instantiating our new abstractions
            Ebo = new BufferObject<uint>(Gl, Indices, BufferTargetARB.ElementArrayBuffer);
            Vbo = new BufferObject<float>(Gl, Vertices, BufferTargetARB.ArrayBuffer);
            Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);

            //Telling the VAO object how to lay out the attribute pointers
            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 7, 0);
            Vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 7, 3);
            Shader = new Shader(Gl, "shader.vert", "shader.frag");

        }

        private static unsafe void OnRender(double obj)
        {
            //Gl.Clear((uint)ClearBufferMask.ColorBufferBit);
            //Binding and using our VAO and shader.
            Vao.Bind();
            Shader.Use();

            Gl.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);

            Vbo.Dispose();
            Ebo.Dispose();
            Vao.Dispose();
            Shader.Dispose();
        }

        private static unsafe void OnUpdate(double dt)
        {

            //Instantiating our new abstractions
            Ebo = new BufferObject<uint>(Gl, Indices, BufferTargetARB.ElementArrayBuffer);
            Vbo = new BufferObject<float>(Gl, Vertices, BufferTargetARB.ArrayBuffer);
            Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);

            //Telling the VAO object how to lay out the attribute pointers

            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 7, 0);
            Vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 7, 3);

            Shader = new Shader(Gl, "shader.vert", "shader.frag");
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

        float BlockWidth = 32f;
        float BlockHeight = 32f;

        public float[] GetLayPt()
        {

            float StdBlockPerW = BlockWidth / ScreenWidth;
            float StdBlockPerH = BlockHeight / ScreenHeight;

            float[] GrpData = new float[] { StdBlockPerW, StdBlockPerH };
            return GrpData;
        }

        public float[] GetStdVertices()
        {
            float StdBlockPerW = BlockWidth / ScreenWidth;
            float StdBlockPerH = BlockHeight / ScreenHeight;
            float[] StrVertices =
{
             // X                Y                   Z     R      G      B      A
                -1+StdBlockPerW , 1.0f             , 0.0f, 1.0f , 1.0f , 1.0f , 0.0f,
                -1+StdBlockPerW , 1-StdBlockPerH   , 0.0f, 1.0f , 1.0f , 1.0f , 0.0f,
                -1.0f           , 1-StdBlockPerH   , 0.0f, 1.0f , 1.0f , 1.0f , 0.0f, 
                -1.0f           , 1.0f             , 0.0f, 1.0f , 1.0f , 1.0f , 0.0f,

            };
            return StrVertices;
        }


    }

    class EngCore
    {
        static public bool IsGraphicExit = false;
        static public bool IsGameStop = false;

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
                Thread Gui = new Thread(new ThreadStart(Graphic.Start));
                Gui.Start();
                Loger(1, "GUI Thread Start");
            }

            Thread BGCore = new Thread(new ThreadStart(OnLoad));
            BGCore.Start();
            Loger(1, "CoreEngine Thread Start");
        }

        private static void Loger(int State , string msg)
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
            GrapicAttribute GA = new GrapicAttribute();
            float[] Vertices = GA.GetStdVertices();
            int time = 0;
            bool right = true;

            //0 正常, 1 错误
            int ExitCode = 1;
            
            while (true)
            {
                if (IsGraphicExit)
                {
                    break;
                    ExitCode = 1;
                }
                if (IsGameStop)
                {
                    Thread.Sleep(20000);
                }

                time++;

                if (Vertices[14] <= -1)
                {
                    right = true;
                }
                else if (Vertices[0] >= 1)
                {
                    right = false;
                }

                if (right)
                {
                    Vertices[0] += 0.001f;
                    Vertices[7] += 0.001f;
                    Vertices[14] += 0.001f;
                    Vertices[21] += 0.001f;
                    Vertices[1] -= 0.001f;
                    Vertices[8] -= 0.001f;
                    Vertices[15] -= 0.001f;
                    Vertices[22] -= 0.001f;
                }
                else
                {
                    Vertices[0] -= 0.001f;
                    Vertices[7] -= 0.001f;
                    Vertices[14] -= 0.001f;
                    Vertices[21] -= 0.001f;
                    Vertices[1] += 0.001f;
                    Vertices[8] += 0.001f;
                    Vertices[15] += 0.001f;
                    Vertices[22] += 0.001f;
                }
                if (time % 100 == 0)
                {
                    Loger(1 , "X:" + Vertices[0] + @"/Y:" + Vertices[1]);
                }


                Graphic.Vertices = Vertices;
                Thread.Sleep(5);
            }
            Loger(1, "ExitCode With :" + ExitCode);
        }
    }
}

/* time++;

                if (Vertices[14] <= -1)
                {
                    right = true;
                }
                else if (Vertices[0] >= 1)
                {
                    right = false;
                }

                if (right)
                {
                    Vertices[0] += 0.001f;
                    Vertices[7] += 0.001f;
                    Vertices[14] += 0.001f;
                    Vertices[21] += 0.001f;
                    Vertices[1] -= 0.001f;
                    Vertices[8] -= 0.001f;
                    Vertices[15] -= 0.001f;
                    Vertices[22] -= 0.001f;
                }
                else
                {
                    Vertices[0] -= 0.001f;
                    Vertices[7] -= 0.001f;
                    Vertices[14] -= 0.001f;
                    Vertices[21] -= 0.001f;
                    Vertices[1] += 0.001f;
                    Vertices[8] += 0.001f;
                    Vertices[15] += 0.001f;
                    Vertices[22] += 0.001f;
                }
                if (time % 100 == 0)
                {
                    Console.WriteLine("X:" + Vertices[0] + @"/Y:" + Vertices[1]);
                    Console.WriteLine("Load:" + Vertices);
                }
*/