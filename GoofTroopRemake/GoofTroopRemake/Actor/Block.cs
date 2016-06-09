using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoofTroopRemake.Actor
{
    public class Block : Actor
    {
        public Rectangle collisionRect { get; set; }
        public Rectangle nextRectangle { get; set; }
        public Vector2 nextMove { get; set; }
        Point area; 
        

        public Block(Texture2D texture, Vector2 position) : base(texture) {
            this.position = position;
            area = new Point(48, 48);
            collisionRect = new Rectangle(position.ToPoint(), area);
            instantMovement = Vector2.Zero;
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(texture, position, Color.White);
        }

        public override void move()
        {
            position = nextMove;
            Console.WriteLine(position.ToString());
        }

        public override void Update(GameTime gameTime, InputHandler inputHandler)
        {
            collisionRect = new Rectangle(position.ToPoint(), area);
            nextMove = position + instantMovement;
            Console.WriteLine(nextMove.ToString());
            nextRectangle = new Rectangle(nextMove.ToPoint(), area);
            //Console.WriteLine(nextRectangle.ToString());
        }

        public void setDir(Point kickVect) {
            instantMovement = kickVect.ToVector2();
            Console.WriteLine(kickVect.ToString() + " Inst move " + instantMovement.ToString());
        }

        public void hasCollide() {
            instantMovement = Vector2.Zero;
           // Console.WriteLine("Has collided");
        }


        public override void attack()
        {
            throw new NotImplementedException();
        }

        public override void die()
        {
            throw new NotImplementedException();
        }
    }
}
