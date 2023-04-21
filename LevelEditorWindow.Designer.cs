namespace Game {
    partial class LevelEditorWindow {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            pLevelCanvas = new Panel();
            panel2 = new Panel();
            nudGridSize = new NumericUpDown();
            label3 = new Label();
            lEditorMode = new Label();
            label1 = new Label();
            label2 = new Label();
            pLevelBackColor = new Panel();
            pBlockColor = new Panel();
            colorDialog1 = new ColorDialog();
            groupBox2 = new GroupBox();
            cbBorder = new CheckBox();
            cbShadow = new CheckBox();
            nudBlockHeight = new NumericUpDown();
            label5 = new Label();
            nudBlockWidth = new NumericUpDown();
            label4 = new Label();
            groupBox1 = new GroupBox();
            bSaveLevel = new Button();
            label6 = new Label();
            tbLevelName = new TextBox();
            cbShowGrid = new CheckBox();
            tabControl1 = new TabControl();
            tpGeneral = new TabPage();
            tpLevel = new TabPage();
            groupBox3 = new GroupBox();
            lbLevels = new ListBox();
            tbLevelsOrder = new TabPage();
            bLevelDown = new Button();
            bLevelUp = new Button();
            lbLevelsOrder = new ListBox();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudGridSize).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudBlockHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBlockWidth).BeginInit();
            groupBox1.SuspendLayout();
            tabControl1.SuspendLayout();
            tpGeneral.SuspendLayout();
            tpLevel.SuspendLayout();
            groupBox3.SuspendLayout();
            tbLevelsOrder.SuspendLayout();
            SuspendLayout();
            // 
            // pLevelCanvas
            // 
            pLevelCanvas.BackColor = SystemColors.Control;
            pLevelCanvas.Location = new Point(13, 15);
            pLevelCanvas.Margin = new Padding(3, 4, 3, 4);
            pLevelCanvas.Name = "pLevelCanvas";
            pLevelCanvas.Size = new Size(731, 699);
            pLevelCanvas.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.AutoScroll = true;
            panel2.BackColor = SystemColors.WindowFrame;
            panel2.Controls.Add(pLevelCanvas);
            panel2.Location = new Point(12, 10);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(920, 753);
            panel2.TabIndex = 1;
            // 
            // nudGridSize
            // 
            nudGridSize.Location = new Point(86, 64);
            nudGridSize.Maximum = new decimal(new int[] { 48, 0, 0, 0 });
            nudGridSize.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            nudGridSize.Name = "nudGridSize";
            nudGridSize.Size = new Size(60, 27);
            nudGridSize.TabIndex = 4;
            nudGridSize.Value = new decimal(new int[] { 32, 0, 0, 0 });
            nudGridSize.ValueChanged += nudGridSize_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 66);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 3;
            label3.Text = "Grid Size";
            // 
            // lEditorMode
            // 
            lEditorMode.AutoSize = true;
            lEditorMode.BackColor = Color.DarkGray;
            lEditorMode.Font = new Font("Cambria", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lEditorMode.Location = new Point(21, 17);
            lEditorMode.Name = "lEditorMode";
            lEditorMode.Size = new Size(59, 23);
            lEditorMode.TabIndex = 2;
            lEditorMode.Text = "mode";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(18, 93);
            label1.Name = "label1";
            label1.Size = new Size(126, 20);
            label1.TabIndex = 1;
            label1.Text = "Level Background";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(8, 40);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 1;
            label2.Text = "Block Color";
            // 
            // pLevelBackColor
            // 
            pLevelBackColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pLevelBackColor.BackColor = SystemColors.ActiveCaption;
            pLevelBackColor.BorderStyle = BorderStyle.Fixed3D;
            pLevelBackColor.Location = new Point(173, 79);
            pLevelBackColor.Name = "pLevelBackColor";
            pLevelBackColor.Size = new Size(60, 34);
            pLevelBackColor.TabIndex = 0;
            pLevelBackColor.Click += pLevelBackColor_Click;
            // 
            // pBlockColor
            // 
            pBlockColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pBlockColor.BackColor = SystemColors.ActiveCaption;
            pBlockColor.BorderStyle = BorderStyle.Fixed3D;
            pBlockColor.Location = new Point(169, 26);
            pBlockColor.Name = "pBlockColor";
            pBlockColor.Size = new Size(60, 34);
            pBlockColor.TabIndex = 0;
            pBlockColor.Click += pBlockColor_Click;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.CadetBlue;
            groupBox2.Controls.Add(cbBorder);
            groupBox2.Controls.Add(cbShadow);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(nudBlockHeight);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(nudBlockWidth);
            groupBox2.Controls.Add(pBlockColor);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(6, 106);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(247, 263);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Block Settings";
            // 
            // cbBorder
            // 
            cbBorder.AutoSize = true;
            cbBorder.CheckAlign = ContentAlignment.MiddleRight;
            cbBorder.Location = new Point(8, 172);
            cbBorder.Name = "cbBorder";
            cbBorder.Size = new Size(116, 24);
            cbBorder.TabIndex = 8;
            cbBorder.Text = "Block Border";
            cbBorder.UseVisualStyleBackColor = true;
            cbBorder.CheckedChanged += cbBorder_CheckedChanged;
            // 
            // cbShadow
            // 
            cbShadow.AutoSize = true;
            cbShadow.CheckAlign = ContentAlignment.MiddleRight;
            cbShadow.Location = new Point(8, 142);
            cbShadow.Name = "cbShadow";
            cbShadow.Size = new Size(124, 24);
            cbShadow.TabIndex = 8;
            cbShadow.Text = "Block Shadow";
            cbShadow.UseVisualStyleBackColor = true;
            cbShadow.CheckedChanged += cbShadow_CheckedChanged;
            // 
            // nudBlockHeight
            // 
            nudBlockHeight.Location = new Point(169, 99);
            nudBlockHeight.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            nudBlockHeight.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
            nudBlockHeight.Name = "nudBlockHeight";
            nudBlockHeight.Size = new Size(60, 27);
            nudBlockHeight.TabIndex = 6;
            nudBlockHeight.Value = new decimal(new int[] { 32, 0, 0, 0 });
            nudBlockHeight.ValueChanged += nudBlockHeight_ValueChanged;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(8, 106);
            label5.Name = "label5";
            label5.Size = new Size(94, 20);
            label5.TabIndex = 7;
            label5.Text = "Block Height";
            // 
            // nudBlockWidth
            // 
            nudBlockWidth.Location = new Point(169, 66);
            nudBlockWidth.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            nudBlockWidth.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
            nudBlockWidth.Name = "nudBlockWidth";
            nudBlockWidth.Size = new Size(60, 27);
            nudBlockWidth.TabIndex = 5;
            nudBlockWidth.Value = new decimal(new int[] { 32, 0, 0, 0 });
            nudBlockWidth.ValueChanged += nudBlockWidth_ValueChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(8, 73);
            label4.Name = "label4";
            label4.Size = new Size(89, 20);
            label4.TabIndex = 7;
            label4.Text = "Block Width";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.SlateGray;
            groupBox1.Controls.Add(bSaveLevel);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(tbLevelName);
            groupBox1.Controls.Add(pLevelBackColor);
            groupBox1.Controls.Add(label1);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Location = new Point(5, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(249, 163);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Level Settings";
            // 
            // bSaveLevel
            // 
            bSaveLevel.Location = new Point(18, 119);
            bSaveLevel.Name = "bSaveLevel";
            bSaveLevel.Size = new Size(215, 29);
            bSaveLevel.TabIndex = 11;
            bSaveLevel.Text = "Save";
            bSaveLevel.UseVisualStyleBackColor = true;
            bSaveLevel.Click += bSaveLevel_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(16, 23);
            label6.Name = "label6";
            label6.Size = new Size(90, 20);
            label6.TabIndex = 9;
            label6.Text = "Level Name:";
            // 
            // tbLevelName
            // 
            tbLevelName.Location = new Point(32, 46);
            tbLevelName.Name = "tbLevelName";
            tbLevelName.Size = new Size(201, 27);
            tbLevelName.TabIndex = 10;
            // 
            // cbShowGrid
            // 
            cbShowGrid.AutoSize = true;
            cbShowGrid.CheckAlign = ContentAlignment.MiddleRight;
            cbShowGrid.Checked = true;
            cbShowGrid.CheckState = CheckState.Checked;
            cbShowGrid.Location = new Point(152, 67);
            cbShowGrid.Name = "cbShowGrid";
            cbShowGrid.Size = new Size(99, 24);
            cbShowGrid.TabIndex = 8;
            cbShowGrid.Text = "Show Grid";
            cbShowGrid.UseVisualStyleBackColor = true;
            cbShowGrid.CheckedChanged += cbShadow_CheckedChanged;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            tabControl1.Controls.Add(tpGeneral);
            tabControl1.Controls.Add(tpLevel);
            tabControl1.Controls.Add(tbLevelsOrder);
            tabControl1.Location = new Point(938, 10);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(268, 753);
            tabControl1.TabIndex = 2;
            // 
            // tpGeneral
            // 
            tpGeneral.BackColor = Color.Silver;
            tpGeneral.Controls.Add(groupBox2);
            tpGeneral.Controls.Add(cbShowGrid);
            tpGeneral.Controls.Add(nudGridSize);
            tpGeneral.Controls.Add(label3);
            tpGeneral.Controls.Add(lEditorMode);
            tpGeneral.Location = new Point(4, 29);
            tpGeneral.Name = "tpGeneral";
            tpGeneral.Padding = new Padding(3);
            tpGeneral.Size = new Size(260, 720);
            tpGeneral.TabIndex = 0;
            tpGeneral.Text = "General";
            // 
            // tpLevel
            // 
            tpLevel.BackColor = Color.Silver;
            tpLevel.Controls.Add(groupBox3);
            tpLevel.Controls.Add(groupBox1);
            tpLevel.Location = new Point(4, 29);
            tpLevel.Name = "tpLevel";
            tpLevel.Padding = new Padding(3);
            tpLevel.Size = new Size(260, 720);
            tpLevel.TabIndex = 1;
            tpLevel.Text = "Level";
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.BackColor = Color.SlateGray;
            groupBox3.Controls.Add(lbLevels);
            groupBox3.Location = new Point(6, 175);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(250, 539);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Levels";
            // 
            // lbLevels
            // 
            lbLevels.FormattingEnabled = true;
            lbLevels.ItemHeight = 20;
            lbLevels.Location = new Point(6, 26);
            lbLevels.Name = "lbLevels";
            lbLevels.Size = new Size(238, 464);
            lbLevels.TabIndex = 0;
            lbLevels.DoubleClick += lbLevels_DoubleClick;
            // 
            // tbLevelsOrder
            // 
            tbLevelsOrder.BackColor = Color.Silver;
            tbLevelsOrder.Controls.Add(bLevelDown);
            tbLevelsOrder.Controls.Add(bLevelUp);
            tbLevelsOrder.Controls.Add(lbLevelsOrder);
            tbLevelsOrder.Location = new Point(4, 29);
            tbLevelsOrder.Name = "tbLevelsOrder";
            tbLevelsOrder.Size = new Size(260, 720);
            tbLevelsOrder.TabIndex = 2;
            tbLevelsOrder.Text = "Levels Order";
            // 
            // bLevelDown
            // 
            bLevelDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bLevelDown.Location = new Point(138, 420);
            bLevelDown.Name = "bLevelDown";
            bLevelDown.Size = new Size(101, 29);
            bLevelDown.TabIndex = 2;
            bLevelDown.Text = "Move Down";
            bLevelDown.UseVisualStyleBackColor = true;
            bLevelDown.Click += bLevelDown_Click;
            // 
            // bLevelUp
            // 
            bLevelUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bLevelUp.Location = new Point(18, 420);
            bLevelUp.Name = "bLevelUp";
            bLevelUp.Size = new Size(82, 29);
            bLevelUp.TabIndex = 3;
            bLevelUp.Text = "Move Up";
            bLevelUp.UseVisualStyleBackColor = true;
            bLevelUp.Click += bLevelUp_Click;
            // 
            // lbLevelsOrder
            // 
            lbLevelsOrder.FormattingEnabled = true;
            lbLevelsOrder.ItemHeight = 20;
            lbLevelsOrder.Location = new Point(3, 30);
            lbLevelsOrder.Name = "lbLevelsOrder";
            lbLevelsOrder.Size = new Size(254, 384);
            lbLevelsOrder.TabIndex = 0;
            // 
            // LevelEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1232, 776);
            Controls.Add(tabControl1);
            Controls.Add(panel2);
            Margin = new Padding(3, 4, 3, 4);
            Name = "LevelEditorWindow";
            Text = "Level Editor";
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudGridSize).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudBlockHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBlockWidth).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tpGeneral.ResumeLayout(false);
            tpGeneral.PerformLayout();
            tpLevel.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            tbLevelsOrder.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pLevelCanvas;
        private Panel panel2;
        private Label label1;
        private Panel pBlockColor;
        private ColorDialog colorDialog1;
        private Label label2;
        private Panel pLevelBackColor;
        private Label lEditorMode;
        private NumericUpDown nudGridSize;
        private Label label3;
        private Label label5;
        private Label label4;
        private NumericUpDown nudBlockHeight;
        private NumericUpDown nudBlockWidth;
        private CheckBox cbShadow;
        private CheckBox cbBorder;
        private CheckBox cbShowGrid;
        private GroupBox groupBox1;
        private Label label6;
        private TextBox tbLevelName;
        private GroupBox groupBox2;
        private Button bSaveLevel;
        private TabControl tabControl1;
        private TabPage tpGeneral;
        private TabPage tpLevel;
        private GroupBox groupBox3;
        private ListBox lbLevels;
        private TabPage tbLevelsOrder;
        private ListBox lbLevelsOrder;
        private Button bLevelDown;
        private Button bLevelUp;
    }
}