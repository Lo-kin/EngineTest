using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Maths;
using VAO;
using System.Collections;
using System.Numerics;
using System.Drawing;

namespace Cyan.Engine
{
    class Graphic
    {
        private static IWindow window;
        private static GL Gl;
        private static IKeyboard primaryKeyboard;

        private const int Width = 800;
        private const int Height = 600;

        //Our new abstracted objects, here we specify what the types are.
        private static BufferObject<float> Vbo;
        private static BufferObject<uint> Ebo;
        private static VertexArrayObject<float, uint> Vao;
        private static Shader TextrueColor;
        private static Shader SolidColor;
        public static Texture Texture;

        //Setup the camera's location, directions, and movement speed
        public static Vector3 CameraPosition = new Vector3(0.0f, 0.0f, 1.0f);
        private static Vector3 CameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        private static Vector3 CameraUp = Vector3.UnitY;
        private static Vector3 CameraDirection = Vector3.Zero;
        private static float CameraYaw = -90f;
        private static float CameraPitch = 0f;
        private static float CameraZoom = 45f;

        //Used to track change in mouse movement to allow for moving of the Camera
        private static Vector2 LastMPos;
        private static Vector2 MPosSubtract;
        public static Transform[] Transforms = new Transform[1];

        public static float[] VertexArray =
        {
             0.5f,  0.5f, 0.0f, 1, 0, 1, 1 , 64 * 3 / 2048,0,
             0.5f, -0.5f, 0.0f, 0, 1, 0, 1 , 64 * 3 / 2048,64 * 3 / 2048,
            -0.5f, -0.5f, 0.0f, 1, 0, 1, 1 , 0,64 * 3 / 2048,
            -0.5f,  0.5f, 0.0f, 0, 1, 0, 1 , 0,0,
        };

        public static uint[] Indices =
        {
            0, 1, 3,
            1, 2, 3,
        };

        public static float[] RenderVet = { };
        public static uint[] RenderInd = { };

        public static uint IndLength = 6;

        public static float[][] RenderObjects = new float[][] { };

        public static float[] RevolveArray = new float[] { 0 };

        public static void Start()
        {
            Cyan.Loger(1, "Graphic Thread Start");
            GrapicAttribute GA = new GrapicAttribute();

            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(GrapicAttribute.ScreenWidth, GrapicAttribute.ScreenHeight);
            options.Title = "Texture";
            window = Window.Create(options);
            window.FramesPerSecond = 60;
            window.Load += OnStaticLoad;
            window.Closing += OnClose;
            window.Update += OnUpdate;
            window.Render += OnRender;
            window.Run();
            window.Dispose();
        }

        private static unsafe void OnUpdate(double deltaTime)
        {
            var moveSpeed = 0.5f * (float)deltaTime;

            if (primaryKeyboard.IsKeyPressed(Key.W))
            {
                //Move forwards
                CameraPosition.Y = CameraPosition.Y + moveSpeed;
            }
            if (primaryKeyboard.IsKeyPressed(Key.S))
            {
                //Move backwards
                CameraPosition.Y = CameraPosition.Y - moveSpeed;
            }
            if (primaryKeyboard.IsKeyPressed(Key.A))
            {
                //Move left
                CameraPosition -= Vector3.Normalize(Vector3.Cross(CameraFront, CameraUp)) * moveSpeed;
            }
            if (primaryKeyboard.IsKeyPressed(Key.D))
            {
                //Move right
                CameraPosition += Vector3.Normalize(Vector3.Cross(CameraFront, CameraUp)) * moveSpeed;
            }
        }

        private static void OnStaticLoad()
        {
            IInputContext input = window.CreateInput();
            primaryKeyboard = input.Keyboards.FirstOrDefault();
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                input.Keyboards[i].KeyDown += KeyDown;
                input.Keyboards[i].KeyUp += KeyUp;
            }
            if (primaryKeyboard != null)
            {
                primaryKeyboard.KeyDown += KeyDown;
            }
            for (int i = 0; i < input.Mice.Count; i++)
            {
                input.Mice[i].Cursor.CursorMode = CursorMode.Raw;
                input.Mice[i].MouseMove += OnMouseMove;
                input.Mice[i].DoubleClick += OnMouseClick;
                input.Mice[i].Scroll += OnMouseWheel;
                
            }


            Gl = GL.GetApi(window);
            Gl.ClearColor(Color.CornflowerBlue);

            Transforms[0] = new Transform();
            Transforms[0].Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, RevolveArray[0]);

            RenderInd = Indices;
            RenderVet = VertexArray;
            Ebo = new BufferObject<uint>(Gl, RenderInd, BufferTargetARB.ElementArrayBuffer, 1);
            Vbo = new BufferObject<float>(Gl, RenderVet, BufferTargetARB.ArrayBuffer, 1);
            Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);

            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 9, 0);
            Vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 9, 3);
            Vao.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 9, 7);
            SolidColor = new Shader(Gl, "shader.vert", "Solid.frag");
            TextrueColor = new Shader(Gl, "shader.vert", "Textrue.frag");

            Texture = new Texture(Gl, "textrue.png");
            IndLength = (uint)Indices.Length;

            Cyan.Loger(1, "Game Graphic Loaded");

        }
        
        private static void TextrueUV()
        {
            var SingleTextrueXPer = 64 / 2048;
            var SingleTextrueYPer = 64 / 2048;
            VertexArray[7] = SingleTextrueXPer * 3;
            VertexArray[8] = 0;
            VertexArray[16] = SingleTextrueXPer * 3;
            VertexArray[17] = SingleTextrueYPer;
            VertexArray[25] = 0;
            VertexArray[26] = SingleTextrueYPer;
            Vao.VertexAttributePointer(2, 2, VertexAttribPointerType.Float, 9, 7);
        }

        private static unsafe void OnRender(double obj)
        {
            TextrueUV();
            Gl.Clear((uint)ClearBufferMask.ColorBufferBit);
            Vao.Bind();
            //SolidColor.Use();

            TextrueColor.Use();
            TextrueColor.SetUniform("uTexture", 0);
            
            var view = Matrix4x4.CreateLookAt(CameraPosition, CameraPosition + CameraFront, CameraUp);
            var projection = Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(CameraZoom), Width / Height, 0.1f, 100.0f);

            TextrueColor.SetUniform("uView", view);
            TextrueColor.SetUniform("uProjection", projection);

            for (int i = 0; i <= 255; i++)
            {

                TextrueColor.SetUniform("uModel", Transforms[i].ViewMatrix);
                Gl.DrawElements(PrimitiveType.Triangles, IndLength, DrawElementsType.UnsignedInt, null);
            }

        }

        private static unsafe void OnMouseMove(IMouse mouse, Vector2 position)
        {
            

            if (LastMPos == default) { LastMPos = position; }
            else
            {

                var xOffset = position.X - LastMPos.X;
                var yOffset = position.Y - LastMPos.Y;
                MPosSubtract = Vector2.Subtract(position , LastMPos);
                LastMPos = Vector2.Normalize(position);
                Console.WriteLine(LastMPos);
                mouse.Position = LastMPos; //防止数值过大

                float tmpRevolve = MathHelper.Revolve(MPosSubtract, LastMPos);
                RevolveArray[0] += tmpRevolve;
                Transforms[0].Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, RevolveArray[0]);
                //Transforms[0].Scales = { 1f , 1f};
                /*
                CameraYaw += xOffset;
                CameraPitch -= yOffset;

                //We don't want to be able to look behind us by going over our head or under our feet so make sure it stays within these bounds
                CameraPitch = Math.Clamp(CameraPitch, -89.0f, 89.0f);

                CameraDirection.X = MathF.Cos(MathHelper.DegreesToRadians(CameraYaw)) * MathF.Cos(MathHelper.DegreesToRadians(CameraPitch));
                CameraDirection.Y = MathF.Sin(MathHelper.DegreesToRadians(CameraPitch));
                CameraDirection.Z = MathF.Sin(MathHelper.DegreesToRadians(CameraYaw)) * MathF.Cos(MathHelper.DegreesToRadians(CameraPitch));
                CameraFront = Vector3.Normalize(CameraDirection);
                */
            }
        }

        private static unsafe void OnMouseWheel(IMouse mouse, ScrollWheel scrollWheel)
        {
            //We don't want to be able to zoom in too close or too far away so clamp to these values
            CameraZoom = Math.Clamp(CameraZoom - scrollWheel.Y, 1.0f, 45f);
        }

        private static void OnClose()
        {
            //Remember to dispose all the instances.
            Vbo.Dispose();
            Ebo.Dispose();
            Vao.Dispose();
            SolidColor.Dispose();
            Texture.Dispose();
            Cyan.IsGraphicExit = true;
        }

        private static void OnMouseClick(IMouse arg1, MouseButton arg2, Vector2 arg3)
        {
            if (arg2.ToString() == "Left")
            {
                Cyan.Loger(1, arg2.ToString());
            }

        }
        private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
        {
            if (arg2 == Key.Enter)
            {
                bool IsTalk = true;
            }
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
             // X                      Y                     Z     R      G      B      A      U      V
                -1.0f                , 1.0f + StdBlockPerH , 0.0f, 1.0f , 0.0f , 0.0f , 1.0f , 1.0f , 0.0f ,
                -1.0f                , 1.0f                , 0.0f, 0.0f , 1.0f , 0.0f , 1.0f , 1.0f , 1.0f ,
                -1.0f - StdBlockPerW , 1.0f                , 0.0f, 0.0f , 0.0f , 1.0f , 1.0f , 0.0f , 1.0f ,
                -1.0f - StdBlockPerW , 1.0f + StdBlockPerH , 0.0f, 1.0f , 0.0f , 0.0f , 1.0f , 0.0f , 0.0f ,

            };
            return StdVertices;
        }


    }

}
