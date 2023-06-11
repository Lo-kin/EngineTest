using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using Silk.NET.Maths;
using VAO;
using Controls;

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
        

        private static float[] Vertices =
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

            object[] Vets = GA.GetGA();
            Vertices = Vets[0] as float[];
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(GA.ScreenWidth, GA.ScreenHeight);
            options.Title = Vertices[0].ToString() + "/" + Vertices[1].ToString();
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

            Gl = GL.GetApi(window);

            //Instantiating our new abstractions
            Ebo = new BufferObject<uint>(Gl, Indices, BufferTargetARB.ElementArrayBuffer);
            Vbo = new BufferObject<float>(Gl, Vertices, BufferTargetARB.ArrayBuffer);
            Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);

            //Telling the VAO object how to lay out the attribute pointers
            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            Vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            Shader = new Shader(Gl, "shader.vert", "shader.frag");

        }

        private static unsafe void OnRender(double obj)
        {
            Gl.Clear((uint)ClearBufferMask.ColorBufferBit);

            //Binding and using our VAO and shader.
            Vao.Bind();
            Shader.Use();
            //Setting a uniform.
            //Shader.SetUniform("uBlue", (float)Math.Sin(DateTime.Now.Millisecond / 1000f * Math.PI));


            Gl.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);
            
        }

        private static void OnClose()
        {
            //Remember to dispose all the instances.
            Vbo.Dispose();
            Ebo.Dispose();
            Vao.Dispose();
            Shader.Dispose();
        }

        private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
        {
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
}