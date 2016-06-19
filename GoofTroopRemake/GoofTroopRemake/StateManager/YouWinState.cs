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

namespace GoofTroopRemake.StateManager
{
    public class YouWinState : State
    {
        SoundEffect winSnd;
        SoundEffectInstance winSndInstance;
        Texture2D win;
        

        public YouWinState() {
            
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(win, new Vector2(100,250), Color.White);
        }

        public void Enter()
        {
            winSndInstance = winSnd.CreateInstance();
            winSndInstance.Play();
        }

        public void Leave()
        {
            winSndInstance.Stop();
            
        }

        public void LoadContent(ContentManager content)
        {
            win = content.Load<Texture2D>("youWin");
            winSnd = content.Load<SoundEffect>("WinSnd");
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            
        }
    }
}
