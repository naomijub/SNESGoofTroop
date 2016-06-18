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

namespace GoofTroopRemake.MaxStates
{
    public class ThrowState : State
    {
        public Max max { get; set; }
        public StateManager.StateManager state { get; set; }

        public Texture2D maxThrowTexture { get; set; }

        private int variation;

        public ThrowState(StateManager.StateManager state, Max max)
        {
            this.state = state;
            this.max = max;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(maxThrowTexture, max.position, max.source, Color.White);
        }

        public void Enter()
        {
            variation = 0;
        }

        public void Leave()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            maxThrowTexture = content.Load<Texture2D>("MaxGrabSprite");
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            variateSprite(gameTime);
        }

        private void variateSprite(GameTime gameTime)
        {
            int auxVar = 66 * (variation % 2);
            switch (max.idle)
            {
                case Max.IdleState.up: max.source = new Rectangle(504 + (auxVar), 170, 63, 85); break;
                case Max.IdleState.down: max.source = new Rectangle(504 + (auxVar), 0, 63, 85); break;
                case Max.IdleState.right: max.source = new Rectangle(504 + (auxVar), 85, 63, 85); break;
                case Max.IdleState.left: max.source = new Rectangle(504 + (auxVar), 255, 63, 85); break;
            }

            if ((gameTime.TotalGameTime.Milliseconds % 50) == 0) variation++;

            if (variation >= 2)
            {
                variation = 0;
                state.ChangeState(new WalkingState(state, max));
            }
        }
    }
}
