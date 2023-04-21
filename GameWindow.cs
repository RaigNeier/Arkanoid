
using System.Drawing.Design;

namespace Game {
    // We will keep code unrelated to the the actual game here
    public partial class GameWindow: Form {
        InputHandler InputHandler;
        public GameWindow() {
            InitializeComponent();

            this.DoubleBuffered = true; // this will remove flickering

            this.Load += delegate (object? sender, EventArgs e) {
                InputHandler = new InputHandler(this);
                InputHandler.UpdateFrame += InputHandler_UpdateFrame;
                InputHandler.DrawFrame += InputHandler_DrawFrame;
                InputHandler.StartUpdateLoop();
            };
        }
    }
}