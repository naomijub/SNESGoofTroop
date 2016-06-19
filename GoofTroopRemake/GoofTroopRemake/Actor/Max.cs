using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GoofTroopRemake.StateManager;
using GoofTroopRemake.MaxStates;
using GoofTroopRemake.Components;
using Microsoft.Xna.Framework.Content;

namespace GoofTroopRemake.Actor
{
    public class Max : Actor
    {
        public enum IdleState { up, down, right, left }
        public enum Kicker { kick, walk }
        public IdleState idle { get; set; }

        public StateManager.StateManager state { get; set; }

        public Rectangle source { get; set; }
        public Rectangle maxRectangleY { get; set; }
        public Rectangle maxRectangleX { get; set; }
        public Rectangle maxRectangle { get; set; }

        public int nextMoveY { get; set; }
        public int nextMoveX { get; set; }

        public Max(Texture2D texture, ContentManager content, InputHandler inputHandler) : base(texture)
        {
            position = new Vector2(370, 570);
            idle = Max.IdleState.up;
            source = new Rectangle(0, 164, 66, 82);
            state = new StateManager.StateManager(content, inputHandler);

            state.setPrimaryState(new WalkingState(state, this));
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            //Console.WriteLine("Draw: x/y " + position.X + "/" + position.Y);
            state.Draw(sb, gameTime);
        }

        public override void move()
        {
            position = new Vector2(nextMoveX, nextMoveY);
        }

        public void moveX()
        {
            position = new Vector2(nextMoveX, position.Y);
        }

        public void moveY()
        {
            position = new Vector2(position.X, nextMoveY);

        }

        public override void Update(GameTime gameTime, InputHandler inputHandler)
        {
            state.Update(gameTime);
        }

        public override void die()
        {

        }

        public Point Kick(InputHandler inputHandler)
        {
            if (inputHandler.KeyDown(Keys.Z))
            {
                switch (idle)
                {
                    case IdleState.up: return new Point((int)position.X + 33, (int)position.Y + 46);
                    case IdleState.down: return new Point((int)position.X + 33, (int)position.Y + 96);
                    case IdleState.right: return new Point(58 + (int)position.X, (int)position.Y + 71);
                    case IdleState.left: return new Point(8 + (int)position.X, (int)position.Y + 71);
                }
            }
            return Point.Zero;
        }

        public Point Grab(InputHandler inputHandler)
        {
            if (inputHandler.KeyDown(Keys.X))
            {
                switch (idle)
                {
                    case IdleState.up: return new Point((int)position.X + 33, (int)position.Y + 46);
                    case IdleState.down: return new Point((int)position.X + 33, (int)position.Y + 96);
                    case IdleState.right: return new Point(58 + (int)position.X, (int)position.Y + 71);
                    case IdleState.left: return new Point(8 + (int)position.X, (int)position.Y + 71);
                }
            }
            return Point.Zero;
        
        }
    }
}
