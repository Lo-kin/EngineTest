using System;
using System.Numerics;

namespace Cyan.Engine
{
    public static class MathHelper
    {
        public static float DegreesToRadians(float degrees)
        {
            return MathF.PI / 180f * degrees;
        }

        public static float[] VetInRect(int ScrWidth , int ScrHeight , Vector3 CamPos , Vector3 CamFront)
        {

            return Array.Empty<float>();
        }

        public static float MixTwoCos(float RadA , float RadB)
        {
            return (MathF.Cos(RadA) * MathF.Cos(RadB)) - (MathF.Sin(RadA) * MathF.Sin(RadB));
        }

        public static float VecLong(float Vector1 , float Vector2)
        {
            return MathF.Sqrt((Vector1 * Vector1) + Vector2 * Vector2);
        }

        public static float CosValve(float GetLong , float Long1 , float Long2)
        {
            return (Long1 * Long1 + Long2 * Long2 - GetLong * GetLong) / (2 * Long1 * Long2);
        }

        public static float Revolve(Vector2 LastPos , Vector2 Rotain)
        {
            int Sign = 0;
            float spd = 0.04f;
            //后向量与其法线夹角总为90°，而转换向量的值必须在这个区间内
            //分别讨论1234象限内的角度增减
            if (LastPos.X > 0 & LastPos.Y > 0)
            {
                if (Rotain.Y > 0)
                {
                    if (Rotain.X <= LastPos.X && Rotain.X >= -LastPos.Y)
                    {
                        Sign++;
                    }
                    else
                    {
                        Sign--;
                    }
                }
                else if (Rotain.Y < 0)
                {
                    if (Rotain.X <= LastPos.Y && Rotain.X <= -LastPos.X)
                    {
                        Sign--;
                    }
                    else
                    {
                        Sign++;
                    }
                }
            }
            else if (LastPos.X < 0 & LastPos.Y > 0)
            {
                if (Rotain.X > 0)
                {
                    if (Rotain.Y <= LastPos.Y && Rotain.Y >= -LastPos.X)
                    {
                        Sign--;
                    }
                    else
                    {
                        Sign++;
                    }
                }
                else if (Rotain.X < 0)
                {
                    if (Rotain.Y <= LastPos.X && Rotain.Y <= -LastPos.Y)
                    {
                        Sign++;
                    }
                    else
                    {
                        Sign--;
                    }
                }
            }

            else if (LastPos.X < 0 & LastPos.Y < 0)
            {
                if (Rotain.Y > 0)
                {
                    if (Rotain.X <= LastPos.X && Rotain.X >= -LastPos.Y)
                    {
                        Sign--;
                    }
                    else
                    {
                        Sign++;
                    }
                }
                else if (Rotain.Y < 0)
                {
                    if (Rotain.X <= LastPos.Y && Rotain.X <= -LastPos.X)
                    {
                        Sign++;
                    }
                    else
                    {
                        Sign --;
                    }
                }
            }
            else if (LastPos.X > 0 & LastPos.Y < 0)
            {
                if (Rotain.X > 0)
                {
                    if (Rotain.Y <= LastPos.Y && Rotain.Y >= -LastPos.X)
                    {
                        Sign--;
                    }
                    else
                    {
                        Sign++;
                    }
                }
                else if (Rotain.X < 0)
                {
                    if (Rotain.Y <= LastPos.X && Rotain.Y <= -LastPos.Y)
                    {
                        Sign++;
                    }
                    else
                    {
                        Sign--;
                    }
                }
            }
            /*
            else if (Rotain.X == 0 || Rotain.Y == 0 )
            {
                Sign = 0;
            }
            */
            float Rad = MathF.Acos((2 - MathF.Sqrt(VecLong(Rotain.X , Rotain.Y))) / 4);
            
            return Rad * Sign * spd;
        }
    }
}