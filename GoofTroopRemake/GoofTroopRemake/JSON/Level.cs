using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoofTroopRemake.JSON
{
    public class Level
    {
        public string name { get; set; }
        public IList<RectangleObjects> rectangles { get; set; }

        public void SetRectangles() {
            for (int i = 0; i < rectangles.Count; i++) {
                rectangles[i].setRectangle();
            }
        }

        public void printRectangles() {
            foreach (RectangleObjects ro in rectangles) {
                Console.WriteLine("Rect: " + ro.collisionRegion.ToString());
            }
        }
    }
}
