using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
    public class Level {

        public static Size DefaultDimensions { get; set; } = new Size(640, 480);

        public Size Size { get; set; }

        public Color BackgroundColor = Color.SlateGray;
        public List<Block> Blocks = new List<Block>();
        [Newtonsoft.Json.JsonIgnore]
        public int BlocksLeft = 0;
        [Newtonsoft.Json.JsonIgnore]
        public int Score = 0;
        public Level() {

        }
        public void Reset() {
            foreach (var block in Blocks) {
                block.IsVisible = true;
            }
            BlocksLeft = Blocks.Count;
        }

        public void DestroyBlock(int index) {
            if (index< Blocks.Count) {
                Blocks[index].IsVisible = false;
                BlocksLeft--;
            }
        }
   

    }
}
