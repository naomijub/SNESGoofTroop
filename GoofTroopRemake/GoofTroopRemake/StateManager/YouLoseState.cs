using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoofTroopRemake.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using GoofTroopRemake.StateManager;

namespace GoofTroopRemake.StateManager
{
    public class YouLoseState : State
    {
        SoundEffect loseSnd;
        SoundEffectInstance loseSndInstance;
        Texture2D lose;
        public StateManager state { get; set; }

        public YouLoseState(StateManager state) {
            this.state = state;
        }

        public void LoadContent(ContentManager content)
        {
            loseSnd = content.Load<SoundEffect>("deeplaugh");
            lose = content.Load<Texture2D>("lose");
        }

        public void Enter()
        {
            loseSndInstance = loseSnd.CreateInstance();
            loseSndInstance.Play();
        }

        public void Leave()
        {
            loseSndInstance.Stop();
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(lose, new Vector2(100, 250), Color.White);
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            if ((gameTime.TotalGameTime.Milliseconds % 1000) == 0) {
                state.ChangeState(new MainMenuState(state));
            }
        }
    }
}
