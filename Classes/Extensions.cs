using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {

    public static class Extension {
        public static Point Int(this PointF floatPoint) {
            return new Point((int)floatPoint.X, (int)floatPoint.Y);
        }
        public static Point Center(this Rectangle rect) {
            return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
        }
    }
}
