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

namespace GoofTroopRemake.Level
{
    public class Level3State : State
    {
        Texture2D level3;
        SoundEffect levelSnd;
        SoundEffectInstance levelSndInstance;
        private StateManager.StateManager state;

        public Level3State(StateManager.StateManager state) {
            this.state = state;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(level3, Vector2.Zero, Color.White);
        }

        public void Enter()
        {
            levelSndInstance = levelSnd.CreateInstance();
            levelSndInstance.Play();
        }

        public void Leave()
        {
            levelSndInstance.Stop();
        }

        public void LoadContent(ContentManager content)
        {
            level3 = content.Load<Texture2D>("level3");
            levelSnd = content.Load<SoundEffect>("levelSnd");
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            
        }
    }
}
