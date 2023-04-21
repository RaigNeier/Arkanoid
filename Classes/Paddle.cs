using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
    public class Paddle {
        public Image texture;
        public PointF Location { get { return new PointF(X, Y); } }
        public SizeF Size { get { return new SizeF(W, H); } }
        public RectangleF BoundsF { get { return new RectangleF(X, Y, W, H); } }
        public Rectangle Bounds { get { return new Rectangle((int)X, (int)Y, (int)W, (int)H); } }

        public float Speed, X, Y, W, H;


        public Paddle(string texPath, float x, float y,
            float speed = 3.0f,
            float w = 75, float h = 24) {
            texture = Image.FromFile(texPath);
   
            Speed = speed;
            X = x;
            Y = y;
            W = w;
            H = h;

        }

        public void Draw(Graphics g) {
            if (texture != null) {
                g.DrawImage(texture, Bounds);
            }
        }
    }
}
