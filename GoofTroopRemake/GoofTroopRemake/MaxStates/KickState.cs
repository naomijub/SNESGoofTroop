using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoofTroopRemake.Actor;
using GoofTroopRemake.StateManager;
using GoofTroopRemake.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GoofTroopRemake.MaxStates
{
    public class KickState : State
    {
        public Max max { get; set; }
        public StateManager.StateManager state { get; set; }

        public Texture2D maxKickTexture { get; set; }

        private int variation = 0;
        float currentTime = 0f;

        public KickState(StateManager.StateManager state, Max max)
        {
            this.state = state;
            this.max = max;
        }

        public void LoadContent(ContentManager content)
        {
            maxKickTexture = content.Load<Texture2D>("MaxKickSprite");
        }

        public void Enter()
        {
            
        }

        public void Leave()
        {
           
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(maxKickTexture, max.position, max.source, Color.White);
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            variateSprite(gameTime);
        }

        private void variateSprite(GameTime gameTime)
        {
            float duration = 0.12f;
            max.actorState = Actor.Actor.ActorState.idle;
            int auxVar = 66 * (variation % 3);
            
            switch (max.idle)
            {
                case Max.IdleState.up: max.source = new Rectangle((auxVar), 190, 66, 95); break;
                case Max.IdleState.down: max.source = new Rectangle((auxVar), 0, 66, 95); break;
                case Max.IdleState.right: max.source = new Rectangle((auxVar), 95, 66, 95); break;
                case Max.IdleState.left: max.source = new Rectangle((auxVar), 285, 66, 95); break;
            }

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= duration)
            {
                variation++;
                currentTime -= duration;
            }

            if (variation >= 3)
            {
                variation = 0;
                state.ChangeState(new WalkingState(state, max));
            }
            
        }
    }
}
