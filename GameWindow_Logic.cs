using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Game {

    public partial class GameWindow: Form {

        //System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\mywavfile.wav");
        //player.Play();


        string ResPath = "..\\..\\..\\Data\\";
        public enum GameState { INIT, NEW_GAME, MENU, GAME_LOOP, EXIT, NEXT_LEVEL };
        GameState gameState = GameState.INIT;
        public List<string> LevelsOrder = null;
        Dictionary<string, string> LevelsList;
        int currentLevelIndex = 0;
        Level currentLevel = null;
        GameMenu gameMenu = null;
        Color clearColor = Color.DimGray;

        int totalScore = 0;

        Ball ball = null;
        Paddle paddle = null;

        private void InputHandler_UpdateFrame(InputHandler input, Graphics g) {
            switch (gameState) {
                case GameState.INIT:
                    GameInit();
                    break;
                case GameState.MENU:
                    gameMenu.Update(input, g);
                    break;
                case GameState.NEW_GAME:
                    LoadLevel();
                    gameState = GameState.GAME_LOOP;
                    break;
                case GameState.GAME_LOOP:
                    GameLoopUpdate(input, g);
                    break;
                case GameState.NEXT_LEVEL:
                    if (currentLevelIndex + 1 < LevelsOrder.Count) {
                        currentLevelIndex++;
                        LoadLevel();
                        gameState = GameState.GAME_LOOP;
                    }
                    else
                        gameState = GameState.MENU;

                    break;
                case GameState.EXIT:
                    break;


            }
        }

        public void LoadLevel() {
            string levelPath = LevelsList[LevelsOrder[currentLevelIndex]];
            currentLevel = JsonConvert.DeserializeObject<Level>(File.ReadAllText(levelPath));
            clearColor = currentLevel.BackgroundColor;
            currentLevel.Reset();
        }

        public bool CircleIntersect(Rectangle rect, PointF rectCenter, PointF cLoc, float r, out int colType) {
            PointF dist = new PointF(Math.Abs(cLoc.X - rectCenter.X), Math.Abs(cLoc.Y - rectCenter.Y));  // circle is x and y away from rectangle center on verticle axis
            colType = -1;
            if (dist.X > (rect.Width / 2 + ball.R)) { return false; } // too far on x axis
            if (dist.Y > (rect.Height / 2 + ball.R)) { return false; } // too far on y axis

            if (dist.X <= (rect.Width / 2)) { colType = 0; return true; }
            if (dist.Y <= (rect.Height / 2)) { colType = 1; return true; }


            var cDist_sq = Math.Pow((dist.X - rect.Width / 2), 2) + Math.Pow((dist.Y - rect.Height / 2), 2);

            bool corner = (cDist_sq <= (ball.R * ball.R));
            if (corner)
                colType = 2;
            return corner;

        }

        public void GameLoopUpdate(InputHandler input, Graphics g) {
            if (input.IsJustKeyPressed(Keys.Escape)) {
                gameState = GameState.MENU;
            }

            if (input.IsKeyDown(Keys.Right)) {
                paddle.X += paddle.Speed;
            }
            if (input.IsKeyDown(Keys.Left)) {
                paddle.X -= paddle.Speed;
            }
            if (paddle.X < 0) {
                paddle.X = 0;
            }
            if (paddle.X + paddle.W > this.ClientSize.Width) {
                paddle.X = ClientSize.Width - paddle.W;
            }


            // precalculate first and then check if that was ok
            var bnx = ball.X + ball.DX * ball.Speed;
            var bny = ball.Y + ball.DY * ball.Speed;

            // window borders
            if (bnx - ball.R < 0 || (bnx + ball.R) >= this.ClientSize.Width) {
                ball.DX = -ball.DX;
            }
            if (bny - ball.R < 0 || (bny + ball.R) >= this.ClientSize.Height) {
                ball.DY = -ball.DY;
            }
            int colType = -1;

            for (int i = 0; i < currentLevel.Blocks.Count; i++) {
                var block = currentLevel.Blocks[i];
                if (block.IsVisible) {
                    Point bc = new Point(block.Location.X + block.Size.Width / 2, block.Location.Y + block.Size.Height / 2);
             
                   
                    if (CircleIntersect(block.GetBounds(), block.GetBounds().Center(), new PointF(bnx, bny), ball.R, out colType)) {
                        switch (colType) {                            
                            case 0:
                                ball.DY = -ball.DY;
                                break;
                            case 1:
                                ball.DX = -ball.DX;
                                break;

                            case 2:
                                ball.DX = -ball.DX;
                                ball.DY = -ball.DY;
                                break;
                            default:
                                break;
                        }

                        currentLevel.DestroyBlock(i);
                    }
                }
            }

           
            if (CircleIntersect(paddle.Bounds, paddle.Bounds.Center(), new PointF(bnx, bny), ball.R, out colType)) {
                switch (colType) {
                    case 0:
                        ball.DY = -ball.DY;
                        break;
                    case 1:
                        ball.DX = -ball.DX;
                        break;

                    case 2:
                        ball.DX = -ball.DX;
                        ball.DY = -ball.DY;
                        break;
                    default:
                        break;
                }
            }


            ball.X += ball.DX * ball.Speed;
            ball.Y += ball.DY * ball.Speed;

            if (currentLevel.BlocksLeft == 0) {
                gameState = GameState.NEXT_LEVEL;

            }

        }
        public void GameLoopDraw(InputHandler input, Graphics g) {
            foreach (var block in currentLevel.Blocks) {
                block.Draw(g);

            }
            paddle.Draw(g);
            ball.Draw(g);
            
            g.DrawString("SCORE : "+ totalScore.ToString(), SystemFonts.DefaultFont, Brushes.WhiteSmoke, 10, 10);
        }

        public void GameInit() {

            this.ClientSize = Level.DefaultDimensions;


            if (File.Exists(ResPath + "LevelsOrder.json")) {
                LevelsOrder = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(ResPath + "LevelsOrder.json"));

                LevelsList = new Dictionary<string, string>();
                var levelsPaths = Directory.EnumerateFiles(ResPath + "Levels\\", "*.lev", SearchOption.TopDirectoryOnly);

                foreach (var levelPath in levelsPaths) {

                    LevelsList.Add(Path.GetFileNameWithoutExtension(levelPath), levelPath);
                }
                gameMenu = new GameMenu(new string[] { "NEW GAME", "NEXT LEVEL", "LEVEL EDITOR", "EXIT" });
                gameMenu.OptionSelected += GameMenu_OptionSelected;
                clearColor = gameMenu.BackColor;

                ball = new Ball(ResPath + "Default\\ball.png",
                    Level.DefaultDimensions.Width / 2,
                    Level.DefaultDimensions.Height - 50,
                    1,
                    1, 4.0f,
                    8);
                paddle = new Paddle(ResPath + "Default\\paddle.png",
                  (Level.DefaultDimensions.Width / 2) - 35,
                  Level.DefaultDimensions.Height - 30,
                  4.0f,
                  100, 22);

                gameState = GameState.MENU;
            }
            else {
                InputHandler.StopUpdateLoop();
                if (MessageBox.Show(this, "There is no levels found! Do you want to open Level Editor and create some?",
                "No Levels", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    LevelEditorWindow editor = new LevelEditorWindow();
                    editor.Show();
                    editor.FormClosed += delegate (object? sender, FormClosedEventArgs e) { this.Show(); InputHandler.StartUpdateLoop(); };
                    this.Hide();
                }
                else {
                    this.Close();
                }
            }
        }

        private void GameMenu_OptionSelected(object? sender, EventArgs e) {
            switch (gameMenu.Options[gameMenu.CurrentOption].Text) {
                case "NEW GAME":
                    gameState = GameState.NEW_GAME;
                    break;
                case "LEVEL EDITOR":
                    InputHandler.StopUpdateLoop();
                    LevelEditorWindow editor = new LevelEditorWindow();
                    editor.Show();
                    editor.FormClosed += delegate (object? sender, FormClosedEventArgs e) { this.Show(); InputHandler.StartUpdateLoop(); };
                    this.Hide();
                    break;
                case "NEXT LEVEL":
                    gameState = GameState.NEXT_LEVEL;
                    break;
                case "EXIT":
                    this.Close();
                    break;
            }
        }


        private void InputHandler_DrawFrame(InputHandler input, Graphics g) {

            g.Clear(clearColor);
            switch (gameState) {

                case GameState.MENU:
                    gameMenu.Draw(g);
                    break;

                case GameState.GAME_LOOP:
                    GameLoopDraw(input, g);
                    break;

            }
        }


    }
}
