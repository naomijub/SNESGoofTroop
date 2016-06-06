using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoofTroopRemake.JSON
{
    public class RectangleObjects
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Rectangle collisionRegion { get; set; }

        public RectangleObjects() { }

        public RectangleObjects(int x, int y, int width, int height) {
            collisionRegion = new Rectangle(x, y, width, height);
        }

        public void setRectangle() {
            collisionRegion = new Rectangle(x, y, width, height);
        }
    }
}
