using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace Game {
    public partial class LevelEditorWindow: Form {

        int CellSize = 32;
        Level currentLevel = new Level();
        Block currentBlock = null;
        bool isBlockHeld = false;
        bool IsCreateMode = false;

        InputHandler inputHandler;

        string ResPath = "..\\..\\..\\Data\\";

        Dictionary<string, string> LevelsList;


        public LevelEditorWindow() {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, pLevelCanvas, new object[] { true });

            inputHandler = new InputHandler(pLevelCanvas);

            inputHandler.UpdateFrame += InputHandler_UpdateFrame;
            inputHandler.DrawFrame += InputHandler_DrawFrame;
            this.Load += LevelEditor_Load;
            this.FormClosing += LevelEditor_FormClosing;

            inputHandler.StartUpdateLoop();
        }

        private void LevelEditor_FormClosing(object? sender, FormClosingEventArgs e) {
            if (MessageBox.Show("Do you want to save current level?", "Save Level?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                SaveLevel();
            }
            List<string> order = new List<string>();
            foreach (var item in lbLevelsOrder.Items) {
                order.Add(item.ToString());
            }
            File.WriteAllText(ResPath + "LevelsOrder.json", JsonConvert.SerializeObject(order, Formatting.Indented));

        }

        private void LevelEditor_Load(object? sender, EventArgs e) {

            pLevelCanvas.Size = Level.DefaultDimensions;
            currentLevel = new Level();
            pLevelBackColor.BackColor = currentLevel.BackgroundColor;
            pLevelCanvas.BackColor = currentLevel.BackgroundColor;
            lEditorMode.Text = (IsCreateMode) ? "Create Mode" : "Select Mode";
            UpdateLevelsList();
            List<string> order = new List<string>();
            if (!File.Exists(ResPath + "LevelsOrder.json")) {

                foreach (string levelName in LevelsList.Keys) {
                    order.Add(levelName);
                }
                File.WriteAllText(ResPath + "LevelsOrder.json", JsonConvert.SerializeObject(order, Formatting.Indented));
            }
            else {
                order = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(ResPath + "LevelsOrder.json"));
            }
            lbLevelsOrder.Items.Clear();
            lbLevelsOrder.Items.AddRange(order.ToArray());
        }

        public void UpdateLevelsList() {
            lbLevels.Items.Clear();
            LevelsList = new Dictionary<string, string>();
            var levelsPaths = Directory.EnumerateFiles(ResPath + "Levels\\", "*.lev", SearchOption.TopDirectoryOnly);

            foreach (var levelPath in levelsPaths) {
                lbLevels.Items.Add(Path.GetFileNameWithoutExtension(levelPath));
                LevelsList.Add(Path.GetFileNameWithoutExtension(levelPath), levelPath);
            }
        }
        private void InputHandler_UpdateFrame(InputHandler input, Graphics g) {
            if (input.IsJustKeyPressed(Keys.Tab)) {
                IsCreateMode = !IsCreateMode;
                lEditorMode.Text = (IsCreateMode) ? "Create Mode" : "Select Mode";
            }

            if (IsCreateMode) {
                if (input.IsJustMouseButtonPressed(MouseButtons.Left)) {
                    Block newBlock = new Block();
                    newBlock.Color = pBlockColor.BackColor;
                    newBlock.Location = input.MouseLocationCurrent.Int();
                    newBlock.Size = new Size((int)nudBlockWidth.Value, (int)nudBlockHeight.Value);
                    newBlock.Shadow = cbShadow.Checked;
                    newBlock.Border = cbBorder.Checked;
                    currentLevel.Blocks.Add(newBlock);
                    currentBlock = currentLevel.Blocks.Last();

                }
            }
            else {

                if (input.IsJustMouseButtonPressed(MouseButtons.Left)) {
                    // we moving in oposite direction, since last would be drawn first
                    for (int i = currentLevel.Blocks.Count - 1; i >= 0; i--) {
                        if (currentLevel.Blocks[i].GetBounds().Contains(input.MouseLocationCurrent.Int())) {
                            currentBlock = null;

                            cbBorder.Checked = currentLevel.Blocks[i].Border;
                            cbShadow.Checked = currentLevel.Blocks[i].Shadow;
                            nudBlockWidth.Value = currentLevel.Blocks[i].Size.Width;
                            nudBlockHeight.Value = currentLevel.Blocks[i].Size.Height;

                            currentBlock = currentLevel.Blocks[i];
                            currentLevel.Blocks.Remove(currentBlock);
                            currentLevel.Blocks.Add(currentBlock);
                            isBlockHeld = true;
                            pBlockColor.BackColor = currentBlock.Color;
                            // we found are first block that have mouse cursor in there is no point to continue
                            break;
                        }
                        else
                            currentBlock = null;
                    }
                }
                if (input.IsJustMouseButtonReleased(MouseButtons.Left)) {
                    isBlockHeld = false;
                    if (currentBlock != null)

                        currentBlock.Location = GetClosestGridPoint(currentBlock.Location);
                }
                if (input.IsMouseDown(MouseButtons.Left)) {
                    if (currentBlock != null && isBlockHeld) {
                        currentBlock.Location = new Point(currentBlock.Location.X + (int)input.MouseOffset.X,
                            currentBlock.Location.Y + (int)input.MouseOffset.Y);
                    }
                }
                if (input.IsJustKeyPressed(Keys.Delete)) {
                    if (currentBlock != null) {
                        currentLevel.Blocks.Remove(currentBlock);
                        currentBlock = null;
                    }

                }
            }
        }

        private void InputHandler_DrawFrame(InputHandler input, Graphics g) {
            //e.Graphics.Clear(currentLevel.BackgroundColor);
            if (cbShowGrid.Checked) {
                DrawLevelGrid(g);
            }
            foreach (var block in currentLevel.Blocks) {
                block.Draw(g);
            }
            if (currentBlock != null) {
                g.DrawRectangle(new Pen(Color.OrangeRed), currentBlock.GetBounds());
            }
            // is we in creative mode show that more visible
            if (IsCreateMode) {
                g.DrawRectangle(new Pen(Color.OrangeRed, 2), Rectangle.Inflate(pLevelCanvas.ClientRectangle, -2, -2));
            }
        }


        public void DrawLevelGrid(Graphics g) {
            //Pen gridPen = new Pen(Color.FromArgb(pLevelCanvas.BackColor.ToArgb() ^ 0xffffff), 1);
            Pen gridPen = new Pen(Color.Black, 1);

            Size numCells = new Size(pLevelCanvas.Width / CellSize, pLevelCanvas.Height / CellSize);
            for (int y = 1; y <= numCells.Height - 1; y++) {
                g.DrawLine(gridPen, new Point(0, y * CellSize), new Point(pLevelCanvas.Width, y * CellSize));
            }
            for (int x = 1; x <= numCells.Width - 1; x++) {
                g.DrawLine(gridPen, new Point(x * CellSize, 0), new Point(x * CellSize, pLevelCanvas.Height));
            }
            var rect = pLevelCanvas.ClientRectangle;
            Pen borderPen = new Pen(Color.Black, 2f);
            g.DrawRectangle(borderPen, new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2));
        }


        public Point GetClosestGridPoint(Point location) {
            //get absolute count of cells by x an y
            int cells_countx = (int)(Math.Abs(location.X) / CellSize);
            int cells_county = (int)(Math.Abs(location.Y) / CellSize);

            //current point offset from the cell center
            float loc_offsetx = Math.Abs(location.X) - (cells_countx * CellSize);
            float loc_offsety = Math.Abs(location.Y) - (cells_county * CellSize);
            PointF newloc = new PointF();


            if (loc_offsetx > (CellSize / 2))
                newloc.X = (cells_countx + 1) * CellSize;
            else
                newloc.X = cells_countx * CellSize;
            if (location.X < 0)
                newloc.X = -newloc.X;

            if (loc_offsety > (CellSize / 2))
                newloc.Y = (cells_county + 1) * CellSize;
            else
                newloc.Y = cells_county * CellSize;
            if (location.Y < 0)
                newloc.Y = -newloc.Y;

            return newloc.Int();

        }

        private void pLevelBackColor_Click(object sender, EventArgs e) {
            using (ColorDialog cd = new ColorDialog()) {
                if (cd.ShowDialog() == DialogResult.OK) {
                    currentLevel.BackgroundColor = cd.Color;
                    pLevelBackColor.BackColor = cd.Color;
                    pLevelCanvas.BackColor = cd.Color;

                }
            }
        }

        private void pBlockColor_Click(object sender, EventArgs e) {
            using (ColorDialog cd = new ColorDialog()) {
                if (cd.ShowDialog() == DialogResult.OK) {
                    if (currentBlock != null)
                        currentBlock.Color = cd.Color;
                    pBlockColor.BackColor = cd.Color;
                }
            }
        }

        private void nudGridSize_ValueChanged(object sender, EventArgs e) {
            CellSize = (int)nudGridSize.Value;
        }

        private void nudBlockWidth_ValueChanged(object sender, EventArgs e) {
            if (currentBlock != null) {
                currentBlock.Size = new Size((int)nudBlockWidth.Value, (int)nudBlockHeight.Value);
            }
        }

        private void nudBlockHeight_ValueChanged(object sender, EventArgs e) {
            if (currentBlock != null) {
                currentBlock.Size = new Size((int)nudBlockWidth.Value, (int)nudBlockHeight.Value);
            }
        }

        private void cbBorder_CheckedChanged(object sender, EventArgs e) {
            if (currentBlock != null) {
                currentBlock.Border = cbBorder.Checked;
            }
        }

        private void cbShadow_CheckedChanged(object sender, EventArgs e) {
            if (currentBlock != null) {
                currentBlock.Shadow = cbShadow.Checked;
            }
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void bSaveLevel_Click(object sender, EventArgs e) {
            SaveLevel();
            UpdateLevelsList();
        }

        public bool SaveLevel() {
            string levelName = tbLevelName.Text;
            if (levelName != "") {
                string fn = ResPath + "Levels\\" + levelName + ".lev";
                if (File.Exists(fn)) {
                    if (MessageBox.Show("Level already exist, overwrite?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        File.WriteAllText(fn, JsonConvert.SerializeObject(currentLevel, Formatting.Indented));
                        return true;
                    }
                    else
                        return false;
                }
                else {
                    File.WriteAllText(fn, JsonConvert.SerializeObject(currentLevel, Formatting.Indented));
                    return true;
                }
            }
            else {
                MessageBox.Show("Level Name is Empty!");
                return false;
            }
        }

        private void lbLevels_DoubleClick(object sender, EventArgs e) {
            if (lbLevels.SelectedIndex >= 0) {
                if (MessageBox.Show("Do you want to save current level?", "Save Level?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    SaveLevel();
                }
                string levPath = Path.GetFullPath(LevelsList[lbLevels.SelectedItem.ToString()]);
                if (File.Exists(levPath)) {
                    currentLevel = JsonConvert.DeserializeObject<Level>(File.ReadAllText(levPath));
                    pLevelBackColor.BackColor = currentLevel.BackgroundColor;
                    pLevelCanvas.BackColor = currentLevel.BackgroundColor;
                    tbLevelName.Text = lbLevels.SelectedItem.ToString();
                    currentBlock = null;
                }

            }
            UpdateLevelsList();
        }

        private void bLevelUp_Click(object sender, EventArgs e) {



            if (lbLevelsOrder.SelectedIndex > 0) {
                var store = lbLevelsOrder.Items[lbLevelsOrder.SelectedIndex - 1];
                lbLevelsOrder.Items[lbLevelsOrder.SelectedIndex - 1] = lbLevelsOrder.Items[lbLevelsOrder.SelectedIndex];
                lbLevelsOrder.Items[lbLevelsOrder.SelectedIndex] = store;
                lbLevelsOrder.SelectedIndex--;
            }



        }

        private void bLevelDown_Click(object sender, EventArgs e) {

            if (lbLevelsOrder.SelectedIndex < lbLevelsOrder.Items.Count - 1) {
                var store = lbLevelsOrder.Items[lbLevelsOrder.SelectedIndex + 1];
                lbLevelsOrder.Items[lbLevelsOrder.SelectedIndex + 1] = lbLevelsOrder.Items[lbLevelsOrder.SelectedIndex];
                lbLevelsOrder.Items[lbLevelsOrder.SelectedIndex] = store;
                lbLevelsOrder.SelectedIndex++;

            }
        }
    }




}