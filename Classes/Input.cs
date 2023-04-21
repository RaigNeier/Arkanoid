using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {

    public class InputState {
        private PointF mouseLocation = new PointF();
        private readonly List<Keys> keysDownList = new List<Keys>();
        private readonly List<MouseButtons> mouseBtnDownList = new List<MouseButtons>();
        private readonly int wheelPosition = 0;
        public PointF Location => mouseLocation;

        public InputState() { }

        public InputState(List<Keys> _keysDownList, List<MouseButtons> _mouseBtnDownList, PointF _mousePosition, int _wheelPosition) {
            keysDownList = _keysDownList;
            mouseBtnDownList = _mouseBtnDownList;
            mouseLocation = _mousePosition;
            wheelPosition = _wheelPosition;
        }

        public InputState GetCopy() {
            List<Keys> copyKeysDownList = new List<Keys>(keysDownList.ToArray());
            List<MouseButtons> copyMouseBtnDownList = new List<MouseButtons>(mouseBtnDownList.ToArray());
            PointF copyLocation = new PointF(mouseLocation.X, mouseLocation.Y);

            return new InputState(copyKeysDownList, copyMouseBtnDownList, copyLocation, wheelPosition);
        }

        public bool IsMouseDown(MouseButtons gMouseButtons) => mouseBtnDownList.Contains(gMouseButtons);
        public bool IsKeyDown(Keys gKeys) => keysDownList.Contains(gKeys);
        public int WheelPosition => wheelPosition;
    }


    public class InputHandler {

        // base form which we wrap around and catch data
        private Control baseControl;

        // current input data
        // changed immediatelly on input events
        private int curWheelDelta = 0;
        private PointF curMouseLocation = new PointF();
        private readonly List<MouseButtons> curMouseBtnDown = new List<MouseButtons>();
        private List<Keys> curKeysDown = new List<Keys>();

        // states that used to make snapshot of input data each frame
        private InputState currentState;
        private InputState previousState;

        // timer properties
        private System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();
        public int UpdateInterval { get { return updateTimer.Interval; } set { updateTimer.Interval = value; } }
        public void StartUpdateLoop() { updateTimer.Start(); }
        public void StopUpdateLoop() { updateTimer.Stop(); }

        public delegate void FrameEventHadler(InputHandler input, Graphics g);

        // Update Frame event
        public event FrameEventHadler UpdateFrame;
        public event FrameEventHadler DrawFrame;

        public InputHandler(Control _control) {
            baseControl = _control;
            currentState = new InputState();
            previousState = new InputState();


            baseControl.MouseMove += delegate (object sender, MouseEventArgs e) {
                var mp = baseControl.PointToClient(Control.MousePosition);
                curMouseLocation = new PointF(mp.X, mp.Y);
            };
            baseControl.MouseDown += delegate (object sender, MouseEventArgs e) {
                var mp = baseControl.PointToClient(Control.MousePosition);
                curMouseLocation = new PointF(mp.X, mp.Y);
                if (!curMouseBtnDown.Contains(e.Button))
                    curMouseBtnDown.Add(e.Button);
            };
            baseControl.MouseUp += delegate (object sender, MouseEventArgs e) {
                var mp = baseControl.PointToClient(Control.MousePosition);
                curMouseLocation = new PointF(mp.X, mp.Y);
                if (curMouseBtnDown.Contains(e.Button))
                    curMouseBtnDown.Remove(e.Button);
            };
            baseControl.MouseWheel += delegate (object sender, MouseEventArgs e) { curWheelDelta += Math.Sign(e.Delta); };
            baseControl.PreviewKeyDown += delegate (object sender, PreviewKeyDownEventArgs e) { e.IsInputKey = true; };
            baseControl.KeyDown += delegate (object sender, KeyEventArgs e) {
                if (!curKeysDown.Contains(e.KeyCode))
                    curKeysDown.Add(e.KeyCode);
                if (e.Alt && !curKeysDown.Contains(Keys.Alt))
                    curKeysDown.Add(Keys.Alt);
            };
            baseControl.KeyUp += delegate (object sender, KeyEventArgs e) {
                if (curKeysDown.Contains(e.KeyCode))
                    curKeysDown.Remove(e.KeyCode);
                if (!e.Alt && curKeysDown.Contains(Keys.Alt))
                    curKeysDown.Remove(Keys.Alt);
            };

            // set focus to a form if mouse inside the window
            baseControl.MouseEnter += delegate (object sender, EventArgs e) { baseControl.Focus(); };
            // set timer interval to 1ms this will give as maximum frame rate
            updateTimer.Interval = 1;
            // on Timer Tick we call form to redraw ins contents
            updateTimer.Tick += delegate { baseControl.Invalidate(); };
            // on paint we get only graphics and send it to main function
            baseControl.Paint += delegate (object sender, PaintEventArgs e) {


                previousState = currentState.GetCopy(); // store last input state as previous
                currentState = CreateInputState(); // and create new input state out of current input data

                UpdateFrame?.Invoke(this, e.Graphics);
                DrawFrame?.Invoke(this, e.Graphics);
            };
        }


        // Making copy of current input data and use  it to create InputState
        private InputState CreateInputState() {
            return new InputState(new List<Keys>(curKeysDown.ToArray()),
                new List<MouseButtons>(curMouseBtnDown.ToArray()),
                new PointF(curMouseLocation.X, curMouseLocation.Y), curWheelDelta);
        }

        //Properties
        public PointF MouseLocationCurrent => currentState.Location;
        public PointF MouseLocationPrevious => previousState.Location;
        public PointF MouseOffset => new PointF(currentState.Location.X - previousState.Location.X, currentState.Location.Y - previousState.Location.Y);
        public int WheelDelta => currentState.WheelPosition;
        public int WheelDeltaOffset => currentState.WheelPosition - previousState.WheelPosition;
        // Methods
        public bool IsKeyDown(Keys key) => currentState.IsKeyDown(key);
        public bool IsMouseDown(MouseButtons mb) => currentState.IsMouseDown(mb);
        public bool IsJustMouseButtonPressed(MouseButtons mouseButton) => currentState.IsMouseDown(mouseButton) && !previousState.IsMouseDown(mouseButton);
        public bool IsJustMouseButtonReleased(MouseButtons mouseButton) => !currentState.IsMouseDown(mouseButton) && previousState.IsMouseDown(mouseButton);
        public bool IsJustKeyPressed(Keys key) => (currentState.IsKeyDown(key) && !previousState.IsKeyDown(key));
        public bool IsJustKeyReleased(Keys key) => (!currentState.IsKeyDown(key) && previousState.IsKeyDown(key));
    }
}
