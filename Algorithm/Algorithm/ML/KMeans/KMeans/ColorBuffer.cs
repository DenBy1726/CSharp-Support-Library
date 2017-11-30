using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    [Serializable]
    public class ColorBuffer
    {
        public int R;
        public int G;
        public int B;

        public ColorBuffer()
        {

        }

        public ColorBuffer(bool initRandom)
        {
            if(initRandom == true)
            {
                Random rnd = new Random();
                R = rnd.Next() % 255;
                G = rnd.Next() % 255;
                B = rnd.Next() % 255;
            }

        }

        public override string ToString()
        {
            return R + " " + G + " " + B;
        }

        public System.Drawing.Color ToColor()
        {
            return System.Drawing.Color.FromArgb(255, R, G, B);
        }

        public void FromColor(System.Drawing.Color c)
        {
            R = c.R;
            G = c.G;
            B = c.B;
        }
    }
}
