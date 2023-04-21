using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Game {
    public class GameMenu {

        public Color BackColor = Color.DimGray;
        public Color ForeColor = Color.WhiteSmoke;
        public Color ForeHover = Color.YellowGreen;
        public Color ForePress = Color.DarkOrange;

        public List<MString> Options = new List<MString>();

        public Font FontNormal = new Font(new FontFamily("Consolas"), 24);
        public Font FontHover = new Font(new FontFamily("Consolas"), 30);
        public PointF Location { get; set; } = new PointF(50, 50);
        public event EventHandler OptionSelected;
        public int OptionPadding = 10;
        public int CurrentOption { get; private set; } = -1;
        private bool pressed = false;


        public GameMenu(string[] options) {
            foreach (string option in options) {
                Options.Add(new MString(option, FontNormal));
            }
        }

        public void Update(InputHandler input, Graphics g) {

            for (int i = 0; i < Options.Count; i++) {
                var ds = Options[i].DrawSize;
                if (new RectangleF(Location.X, Location.Y + (i * (ds.Height + OptionPadding)),
                    ds.Width, ds.Height).Contains(input.MouseLocationCurrent)) {

                    CurrentOption = i;
                    if (input.IsJustMouseButtonPressed(MouseButtons.Left)) {

                        pressed = true;
                    }

                }

            }
            if (input.IsJustMouseButtonReleased(MouseButtons.Left)) {
                if (CurrentOption>=0)
                OptionSelected?.Invoke(this, new EventArgs());
                pressed = false;
            }
        }

        public void Draw(Graphics g) {
            for (int i = 0; i < Options.Count; i++) {
                var ds = Options[i].DrawSize;
                g.DrawString(Options[i].Text, (CurrentOption == i) ? FontHover : FontNormal,
                 (CurrentOption == i) ?
                 ((pressed) ? new SolidBrush(ForePress) : new SolidBrush(ForeHover))
                 : new SolidBrush(ForeColor),
                    new PointF(Location.X, Location.Y + (i * (ds.Height + OptionPadding))));
            }


        }

    }

    public class MString {

        public SizeF DrawSize { get; private set; }
        public string Text { get; private set; }

        public MString(string text) {
            Text = text;
            UpdateSize(SystemFonts.DefaultFont);

        }
        public MString(string text, Font font) {
            Text = text;
            UpdateSize(font);

        }

        public SizeF UpdateSize(Font font) {
            var g = Graphics.FromImage(new Bitmap(Level.DefaultDimensions.Width, Level.DefaultDimensions.Height));
            return (DrawSize = g.MeasureString(Text, font));
        }

        public void SetText() {
            UpdateSize(SystemFonts.DefaultFont);
        }
    }
}
