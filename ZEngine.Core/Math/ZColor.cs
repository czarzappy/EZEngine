using System;

namespace ZEngine.Core.Math
{
    [Serializable]
    public struct ZColor
    {
        public static ZColor red = new ZColor(1f, 0f, 0f, 1f);
        public static ZColor magenta = new ZColor(1f, 0f, 1f, 1f);
        public static ZColor blue = new ZColor(0f, 0f, 1f, 1f);
        public static ZColor yellow = new ZColor(1f, 1f, 0f, 1f);
        public static ZColor green = new ZColor(0f, 1f, 0f, 1f);
        
        public float r;
        public float g;
        public float b;
        public float a;

        private ZColor(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }


        public ZColor NoAlpha()
        {
            return new ZColor(this.r, this.g, this.b, 0f);
        }
        
        public ZColor LowerAlpha(float multiplier)
        {
            return new ZColor(r, g, b, a * multiplier);
        }
        
        public ZColor Darken(float fadeMulitplier)
        {
            return new ZColor(r * fadeMulitplier, 
                g * fadeMulitplier, 
                b * fadeMulitplier, 
                a);
        }
        
        public ZColor Brighten(float fadeMulitplier)
        {
            return new ZColor(r + 1 * fadeMulitplier, 
                g + 1 * fadeMulitplier, 
                b + 1 * fadeMulitplier, 
                a);
        }
    }
}