using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoofTroopRemake.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GoofTroopRemake.Actor;
using Microsoft.Xna.Framework.Input;

namespace GoofTroopRemake.MaxStates
{
    public class GrabState : State
    {
        public Max max { get; set; }
        public StateManager.StateManager state { get; set; }

        public Texture2D maxGrabTexture;

        private Actor.Actor.ActorState auxState;
        private int variation = 0;

        public GrabState(Max max, StateManager.StateManager state) {
            this.max = max;
            this.state = state;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(maxGrabTexture, max.position, max.source, Color.White);
        }

        public void Enter()
        {
            
        }

        public void Leave()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            maxGrabTexture = content.Load<Texture2D>("MaxGrabSprite");
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            max.nextMoveY = (int)max.position.Y;
            max.nextMoveX = (int)max.position.X;
            NextMoveCalculator(inputHandler);
            variateSprite(gameTime);
            max.maxRectangle = new Rectangle(max.position.ToPoint(), new Point(48, 72));
        }

        private void NextMoveCalculator(InputHandler inputHandler)
        {
            if (inputHandler.KeyDown(Keys.Up))
            {
                max.nextMoveY += -4;
                max.actorState = Actor.Actor.ActorState.moveUp;
                max.idle = Max.IdleState.up;
            }
            if (inputHandler.KeyDown(Keys.Down))
            {
                max.nextMoveY += 4;
                max.actorState = Actor.Actor.ActorState.moveDown;
                max.idle = Max.IdleState.down;
            }
            if (inputHandler.KeyDown(Keys.Right))
            {

                max.nextMoveX += 4;
                max.actorState = Actor.Actor.ActorState.moveRight;
                max.idle = Max.IdleState.right;
            }
            if (inputHandler.KeyDown(Keys.Left))
            {
                max.nextMoveX += -4;
                max.actorState = Actor.Actor.ActorState.moveLeft;
                max.idle = Max.IdleState.left;
            }
            if (!inputHandler.KeyDown(Keys.Up) && !inputHandler.KeyDown(Keys.Down) &&
                !inputHandler.KeyDown(Keys.Right) && !inputHandler.KeyDown(Keys.Left))
            {
                max.actorState = Actor.Actor.ActorState.idle;
                determineSourceIdle();
            }

            max.maxRectangleY = new Rectangle((int)max.position.X + 11,
                max.nextMoveY + 60, 42, 20);
            max.maxRectangleX = new Rectangle(max.nextMoveX + 11,
                (int)max.position.Y + 60, 42, 20);
        }

        private void determineSourceIdle()
        {
            if (max.idle == Max.IdleState.up)
            {
                max.source = new Rectangle(0, 170, 63, 85);
            }
            else if (max.idle == Max.IdleState.down)
            {
                max.source = new Rectangle(0, 0, 63, 85);
            }
            else if (max.idle == Max.IdleState.right)
            {
                max.source = new Rectangle(0, 85, 63, 85);
            }
            else if (max.idle == Max.IdleState.left)
            {
                max.source = new Rectangle(0, 255, 63, 85);
            }
            
        }

        private void variateSprite(GameTime gameTime)
        {
            if (auxState != max.actorState)
            {
                variation = 0;
                auxState = max.actorState;
            }
            
            int auxVar = 63 * (variation % 6);
            switch (auxState)
            {
                case Actor.Actor.ActorState.moveUp: max.source = new Rectangle(63 + (auxVar), 170, 63, 85); break;
                case Actor.Actor.ActorState.moveDown: max.source = new Rectangle(63 + (auxVar), 0, 63, 85); break;
                case Actor.Actor.ActorState.moveRight: max.source = new Rectangle(63 + (auxVar), 85, 63, 85); break;
                case Actor.Actor.ActorState.moveLeft: max.source = new Rectangle(63 + (auxVar), 255, 63, 85); break;
            }

            if ((gameTime.TotalGameTime.Milliseconds % 100) == 0) variation++;

            if (variation >= 6)
            {
                variation = 0;
            }
        }
    }
}
