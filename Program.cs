using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using Silk.NET.Maths;
using VAO;
using Controls;
using System.Timers;
namespace EngineTest
using System.Timers;

namespace EngineTest


namespace EngineTest
{
    class Program
    {
        private static IWindow window;
        private static GL Gl;
        

        private static float[] Vertices =
        

        public static float[] Vertices =
        

        public static float[] Vertices =
        {
            //X    Y      Z     R  G  B  A
             0.5f,  0.5f, 0.0f, 1, 1, 1, 0,
             0.5f, -0.5f, 0.0f, 1, 1, 1, 0,
            -0.5f, -0.5f, 0.0f, 1, 1, 1, 0,
            -0.5f,  0.5f, 0.5f, 1, 1, 1, 0
        };

        private static readonly uint[] Indices =
        {
            0, 1, 3,
            GrapicAttribute GA = new GrapicAttribute();

            object[] Vets = GA.GetGA();
            Vertices = Vets[0] as float[];

            options.Size = new Vector2D<int>(GA.ScreenWidth, GA.ScreenHeight);
            options.Title = Vertices[0].ToString() + "/" + Vertices[1].ToString();
        {
            options.Size = new Vector2D<int>(GrapicAttribute.ScreenWidth, GrapicAttribute.ScreenHeight);
            options.Title = "Test";
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(GrapicAttribute.ScreenWidth, GrapicAttribute.ScreenHeight);
            


            window.Update += OnUpdate;
            
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

            Gl = GL.GetApi(window);

            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            Vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            Shader = new Shader(Gl, "shader.vert", "shader.frag");
            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 7, 0);
            Vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 7, 3);
            Shader = new Shader(Gl, "shader.vert", "shader.frag");
            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 7, 0);
            Vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 7, 3);
            Shader = new Shader(Gl, "shader.vert", "shader.frag");
            Gl.Clear((uint)ClearBufferMask.ColorBufferBit);

            Shader = new Shader(Gl, "shader.vert", "shader.frag");
        }
            //Gl.Clear((uint)ClearBufferMask.ColorBufferBit);
            //Setting a uniform.
            //Shader.SetUniform("uBlue", (float)Math.Sin(DateTime.Now.Millisecond / 1000f * Math.PI));

        private static unsafe void OnRender(double obj)
            Gl.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);
            
            //Binding and using our VAO and shader.
            Gl.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);

            Vbo.Dispose();
            Ebo.Dispose();
            Vao.Dispose();
            Shader.Dispose();

            Gl.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);

            Vbo.Dispose();
            Ebo.Dispose();
            Vao.Dispose();
            if (arg2 == Key.Escape)
            {
                window.Close();
            }
        }
    }

    class GrapicAttribute
    {
        //  unit:px 
        public int ScreenWidth = 800;
        public int ScreenHeight = 600;

        float BlockWidth = 32f;
        float BlockHeight = 32f;

        public object[] GetGA()
        {

            float StdBlockPerW = BlockWidth / ScreenWidth;
            float StdBlockPerH = BlockHeight / ScreenHeight;
            
            float[] StrVertices =
            {
             // X                Y                 Z     R  G  B  A
                StdBlockPerW , 0.0f , 0.0f, 1, 1, 1, 0,
                StdBlockPerW , StdBlockPerH , 0.0f, 1, 1, 1, 0,
                0.0f , StdBlockPerH , 0.0f, 1, 1, 1, 0,
                0.0f , 0.0f , 0.0f, 1, 1, 1, 0
            };

            object[] GrpData = new object[] { StrVertices, StdBlockPerW, StdBlockPerH };
            return GrpData;
        }


    }

    class EngCore
    {
        private static void Main(string[] args)
        {
            string pattern = @"";
            bool Console = false;
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
            }

            Thread BGCore = new Thread(new ThreadStart(OnLoad));
        }

        private static void OnLoad()
        {
            while (true)
            {

                Thread.Sleep(5);
            }
        }
    }
}            float StdBlockPerH = BlockHeight / ScreenHeight;
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
*/            float StdBlockPerH = BlockHeight / ScreenHeight;
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