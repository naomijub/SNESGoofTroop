using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoofTroopRemake.Actor
{
    public class Rock : Actor
    {
        Point area;
        Point rectanglePos;
        public Rectangle collisionRect { get; set; }
        public Rectangle nextRectangle { get; set; }
        bool draw = true, horizontal;
        public bool steady { get; set; }
        public bool throwed { get; set; }
        Vector2 nextMove, maxOrigin;
        double seconds = 0;


        public Rock(Texture2D texture, Vector2 position) : base(texture) {
            this.position = position;
            area = new Point(42, 33);
            rectanglePos = new Point((int)position.X + 3, (int)position.Y + 5);
            collisionRect = new Rectangle(rectanglePos, area);
            steady = true;
            throwed = false;
        }

        public override void die()
        {
            draw = false;
            area = Point.Zero;
            rectanglePos = Point.Zero;
            collisionRect = Rectangle.Empty;
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            if(draw)
            sb.Draw(texture, position, Color.White);
        }

        public void move(GameTime gameTime)
        {
            if (!horizontal)
            {
                position += nextMove;
                collisionRect = new Rectangle(rectanglePos, new Point(42, 33));
                if (distance() > 250) die();
            }
            else {
                
                nextMove = new Vector2(nextMove.X, (float)Math.Pow(seconds, 2));
                position += nextMove;
                collisionRect = new Rectangle(rectanglePos, new Point(42, 33));
                if (distance() > 250) die();
                seconds += 0.018;
            }
        }

        public override void Update(GameTime gameTime, InputHandler inputHandler)
        {
            rectanglePos = new Point((int)position.X + 3, (int)position.Y + 5);
            if (steady)
            {
                collisionRect = new Rectangle(rectanglePos, area);
            }
            else {
                if (throwed)
                {
                    move(gameTime);
                    Vector2 nextPosition = (position + nextMove) + new Vector2(-9, 35);
                    nextRectangle = new Rectangle(nextPosition.ToPoint(), new Point(20, 20));
                }
                else {
                    nextRectangle = Rectangle.Empty;
                }
            }
            
        }

        public void updateRockMax(Vector2 maxPos, Max.IdleState dir) {
            position = new Vector2(9 + maxPos.X, maxPos.Y - 35);
            switch (dir) {
                case Max.IdleState.up: nextMove = new Vector2(0, -4); horizontal = false;  break;
                case Max.IdleState.down: nextMove = new Vector2(0, 4); horizontal = false; break;
                case Max.IdleState.right: nextMove = new Vector2(4, 0); horizontal = true; break;
                case Max.IdleState.left: nextMove = new Vector2(-4, 0); horizontal = true; break;
            }
            collisionRect = new Rectangle(rectanglePos, new Point(0, 0));
            maxOrigin = maxPos;
        }

        public void moveUp(Vector2 maxPos) {
            position = new Vector2(9 + maxPos.X, maxPos.Y - 35);
            collisionRect = Rectangle.Empty;
            steady = false;
            throwed = false;
        }

        public double distance() {
            return Math.Sqrt(Math.Pow(maxOrigin.X - position.X ,2) + Math.Pow(maxOrigin.Y - position.Y, 2));
        }

        public override void move()
        {

        }
    }
}
