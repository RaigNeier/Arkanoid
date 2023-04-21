using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
    public class Ball {
        public Image texture;
        public PointF Location { get { return new PointF(X, Y); } }
        public SizeF Size { get { return new SizeF(R*2, R*2); } }
        public RectangleF Bounds { get { return new RectangleF(X-R, Y-R, R*2, R*2); } }

        public float Speed, DX, DY, X, Y, R;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="texPath"> Texture file path(PNG)</param>
        /// <param name="x">X position </param>
        /// <param name="y">Y position </param>
        /// <param name="dx"> Move Direction by X </param>
        /// <param name="dy"> Move Direction by Y</param>
        /// <param name="r">  Ball radius</param>

        public Ball(string texPath, float x, float y,
            float dx = 1, float dy = 1, float speed = 3.0f,
            float r = 8) {
            texture = Image.FromFile(texPath);
            DX = dx;
            DY = dy;
            Speed = speed;
            X = x;
            Y = y;
            R = r;

        }

        public void Draw(Graphics g) {
            if (texture != null) {
                g.DrawImage(texture, Bounds);
            }
        }
    }
}
