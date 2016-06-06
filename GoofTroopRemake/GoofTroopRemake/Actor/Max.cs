using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoofTroopRemake.Actor
{
    public class Max : Actor
    {
        Rectangle source;
        public Rectangle maxRectangle { get; set; }
        public Vector2 nextMove { get; set; }

        public Max(Texture2D texture) : base(texture) {
            source = new Rectangle(0, 164, 66, 82);
            position = new Vector2(370, 570);
            instantMovement = Vector2.Zero;
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
            position = nextMove;
        }

        public override void Update(GameTime gameTime, InputHandler inputHandler)
        {
            nextMove = position;
            maxRectangle = new Rectangle((int)position.X + 11, (int)position.Y + 60, 42, 20);
            NextMoveCalculator(inputHandler);
        }

        public void NextMoveCalculator(InputHandler inputHandler) {
            Vector2 auxMovement;
            if (inputHandler.KeyDown(Keys.Up))
            {
                auxMovement = new Vector2(0, -4);
                instantMovement = auxMovement;
                nextMove += instantMovement;
            }
            if (inputHandler.KeyDown(Keys.Down))
            {
                auxMovement = new Vector2(0, 4);
                instantMovement = auxMovement;
                nextMove += instantMovement;
            }
            if (inputHandler.KeyDown(Keys.Right))
            {
                auxMovement = new Vector2(4, 0);
                instantMovement = auxMovement;
                nextMove += instantMovement;
            }
            if (inputHandler.KeyDown(Keys.Left))
            {
                auxMovement = new Vector2(-4, 0);
                instantMovement = auxMovement;
                nextMove += instantMovement;
            }
            if(!inputHandler.KeyDown(Keys.Up) && !inputHandler.KeyDown(Keys.Down) &&
                !inputHandler.KeyDown(Keys.Right) && !inputHandler.KeyDown(Keys.Left)) {
                instantMovement = Vector2.Zero;
                position += instantMovement;
            }

            maxRectangle = new Rectangle((int)nextMove.X + 11, (int)nextMove.Y + 60, 42, 20);
        }
    }
}
