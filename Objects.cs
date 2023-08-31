using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyan.Engine
{
    internal class Objects
    {

        //对象的正中央
        float PosY = 0;
        float PosX = 0;
        // 0 是对象类型 ， 1 是否可穿过 , 2 是对象是否具有ai
        int[] ObjAtb = { 0, 0, 0 };
        public float PostionX
        {
            get { return PosX; }
            set { PosX = value;  }
        }
        public float PostionY
        {
            get { return PosY; }
            set { PosY = value; }
        }
        public int[] ObjAttributes
        {
            get { return ObjAtb; }
            set { ObjAtb = value; }
        }
    }

    internal class Rectangle : Objects
    {
        // unit 10px
        public float Width = 1;
        public float Height = 1;

        public float[] Crashbox()
        {
            float StartPosX = PostionX - (Width / 2);
            float StartPosY = PostionY - (Height / 2);
            float EndPosX = PostionX + (Width / 2);
            float EndPosY = PostionY + (Height / 2);
            return new float[] { StartPosX, StartPosY, EndPosX, EndPosY };
        }
    }
}
