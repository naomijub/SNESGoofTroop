using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoofTroopRemake.Actor
{
    public class Max : Actor
    {
        Rectangle source;
        public Rectangle maxRectangle { get; set; }

        public Max(Texture2D texture) : base(texture) {
            source = new Rectangle(0, 0, 48, 96);
            position = new Vector2(375, 577);
            Console.WriteLine(" Max Created ");
        } 

        public override void attack()
        {
            
        }

        public override void die()
        {
            
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            Console.WriteLine("Draw: x/y " + position.X + "/" + position.Y);
            sb.Draw(texture, position, source, Color.White);
        }

        public override void move()
        {
            
        }

        public override void Update(GameTime gameTime, InputHandler inputHandler)
        {
            maxRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
    }
}
