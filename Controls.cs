using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cyan.Engine
{
    public class Controls
    {
        float StdPosX = 0f;
        float StdPosY = 0f;

        float[] Color = new float[] {1 , 1 , 1 , 0 };

        int ControlLevel = 5;
        bool IsFixed = false;
        

        public static float[] DynamicControlReLoad()
        {
            return new float[0];
        }

        public static float[] AddControl()
        {
            return new float[0];
        }
    }

    class Canvas : Controls
    {
        ArrayList Children = new ArrayList();
        
        public object Child
        {
            get
            {
                return Children;
            }
            set
            {
                Children.Add(value);
            }
        }
    }
}
