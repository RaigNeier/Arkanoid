
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
    public class Block {
        // location
        public Point Location = new Point(0, 0);
        public Color Color = Color.CornflowerBlue;
        public Size Size = new Size(54, 24);

        public bool Border = false;
        public bool Shadow = false;
        [Newtonsoft.Json.JsonIgnore]
        public bool IsVisible  = true;

        public Block() { }
        public void Draw(Graphics g) {
            if (IsVisible) {
                if (Shadow) {

                    g.FillRectangle(new SolidBrush(Color.Black), Location.X + 2, Location.Y + 2, Size.Width, Size.Height);
                }

                g.FillRectangle(new SolidBrush(Color), GetBounds());
                if (Border) {
                    g.DrawRectangle(Pens.Black, Rectangle.Inflate(GetBounds(), -1, -1));
                }
            }

        }
        public Rectangle GetBounds() {
            return new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
        }
        public RectangleF GetBoundsF() {
            return new RectangleF(Location.X, Location.Y, Size.Width, Size.Height);
        }
    }
}
