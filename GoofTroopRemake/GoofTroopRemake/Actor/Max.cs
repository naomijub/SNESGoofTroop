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
        enum IdleState {up, down, right, left }
        public enum Kicker { kick, walk}
        private IdleState idle;
        private ActorState auxState;
        public Kicker kickState { get; set; }

        Rectangle source;
        public Rectangle maxRectangleY { get; set; }
        public Rectangle maxRectangleX { get; set; }
        public Rectangle maxRectangle { get; set; }

        public int nextMoveY { get; set; }
        public int nextMoveX { get; set; }

        Texture2D maxKickTexture;
        private int variation = 0;

        public Max(Texture2D texture, Texture2D maxKickTexture) : base(texture) {
            this.maxKickTexture = maxKickTexture;
            source = new Rectangle(0, 164, 66, 82);
            position = new Vector2(370, 570);
            instantMovement = Vector2.Zero;
            idle = IdleState.up;
            auxState = ActorState.idle;
            kickState = Kicker.walk;
            Console.WriteLine(" Max Created ");
        } 

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            //Console.WriteLine("Draw: x/y " + position.X + "/" + position.Y);
            Color color = Color.White;
            if (kickState == Kicker.walk)
            {
                sb.Draw(texture, position, source, color);
            }
            else {
                sb.Draw(maxKickTexture, position, source, color);
            }
        }

        public override void move()
        {
            position = new Vector2(nextMoveX, nextMoveY);
        }

        public void moveX() {
            position = new Vector2(nextMoveX, position.Y);
        }

        public void moveY()
        {
            position = new Vector2(position.X, nextMoveY);
            
        }

        public override void Update(GameTime gameTime, InputHandler inputHandler)
        {
            nextMoveY = (int)position.Y;
            nextMoveX = (int)position.X;
            //maxRectangleY = new Rectangle((int)position.X + 11, (int)position.Y + 60, 42, 20);
            NextMoveCalculator(inputHandler);
            variateSprite(gameTime);
            maxRectangle = new Rectangle(position.ToPoint(), new Point(48, 72));
        }

        public void NextMoveCalculator(InputHandler inputHandler) {

            if (inputHandler.KeyDown(Keys.Up))
            {
                nextMoveY += -4;
                actorState = ActorState.moveUp;
                idle = IdleState.up;
            }
            if (inputHandler.KeyDown(Keys.Down))
            {
                nextMoveY += 4;
                actorState = ActorState.moveDown;
                idle = IdleState.down;
            }
            if (inputHandler.KeyDown(Keys.Right))
            {
                
                nextMoveX += 4;
                actorState = ActorState.moveRight;
                idle = IdleState.right;
            }
            if (inputHandler.KeyDown(Keys.Left))
            {
                nextMoveX += -4;
                actorState = ActorState.moveLeft;
                idle = IdleState.left;
            }
            if(!inputHandler.KeyDown(Keys.Up) && !inputHandler.KeyDown(Keys.Down) &&
                !inputHandler.KeyDown(Keys.Right) && !inputHandler.KeyDown(Keys.Left)) {
                actorState = ActorState.idle;
                determineSourceIdle();
            }

            maxRectangleY = new Rectangle((int)position.X + 11, nextMoveY + 60, 42, 20);
            maxRectangleX = new Rectangle(nextMoveX + 11, (int)position.Y + 60, 42, 20);
        }

        private void determineSourceIdle()
        {
            if (idle == IdleState.up)
            {
                source = new Rectangle(0, 164, 66, 82);
            }
            else if (idle == IdleState.down)
            {
                source = new Rectangle(0, 0, 66, 82);
            }
            else if (idle == IdleState.right)
            {
                source = new Rectangle(0, 82, 66, 82);
            }
            else if (idle == IdleState.left)
            {
                source = new Rectangle(0, 246, 66, 82);
            }
            else
            {
                source = new Rectangle(0, 164, 66, 82);
            }
        }

        public void variateSprite(GameTime gameTime) {
            if (auxState != actorState)
            {
                variation = 0;
                auxState = actorState;
            }

            if (kickState == Kicker.walk)
            {
                variateWalking();
            }
            else {
                if (variation >= 3) kickState = Kicker.walk;
                variateKick();
            }

            if ((gameTime.TotalGameTime.Milliseconds % 150) == 0) {
                variation++;
            }
        }

        public void variateWalking() {
            
            int auxVar = 66 * ((variation % 5) + 1);
            switch (auxState)
            {
                case ActorState.moveUp: source = new Rectangle(66 + (auxVar), 164, 66, 82); break;
                case ActorState.moveDown: source = new Rectangle(66 + (auxVar), 0, 66, 82); break;
                case ActorState.moveRight: source = new Rectangle(66 + (auxVar), 82, 66, 82); break;
                case ActorState.moveLeft: source = new Rectangle(66 + (auxVar), 246, 66, 82); break;
            }
        }

        public void variateKick()
        {

            int auxVar = 66 * (variation % 3);
            switch (idle)
            {
                case IdleState.up: source = new Rectangle(66 + (auxVar), 190, 66, 95); break;
                case IdleState.down: source = new Rectangle(66 + (auxVar), 0, 66, 95); break;
                case IdleState.right: source = new Rectangle(66 + (auxVar), 95, 66, 95); break;
                case IdleState.left: source = new Rectangle(66 + (auxVar), 285, 66, 95); break;
            }
        }

        public override void attack()
        {
            
        }

        public override void die()
        {
            
        }

        public Point Kick(InputHandler inputHandler) {
            if (inputHandler.KeyPressed(Keys.Z)) {
                //Console.WriteLine(origin.ToString());
                kickState = Kicker.kick;
                switch (idle) {
                    case IdleState.up: return new Point((int)position.X + 33, (int)position.Y + 46);
                    case IdleState.down: return new Point((int)position.X + 33, (int)position.Y + 96);
                    case IdleState.right: return new Point(58 + (int)position.X, (int)position.Y + 71);
                    case IdleState.left:  return new Point(8 + (int)position.X, (int)position.Y + 71);
                }
            }
            return Point.Zero;
        }
    }
}
