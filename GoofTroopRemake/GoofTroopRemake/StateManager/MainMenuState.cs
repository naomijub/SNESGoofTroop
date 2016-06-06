using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GoofTroopRemake.Components;
using GoofTroopRemake.Buttons;
using Microsoft.Xna.Framework.Content;
using GoofTroopRemake.Level;
using Microsoft.Xna.Framework.Audio;

namespace GoofTroopRemake.StateManager
{
    public class MainMenuState : State
    {
        private Texture2D buttons, levelSelector;
        SpriteFont font;
        SoundEffect selectorSnd;
        SoundEffectInstance selectorSndInstance;
        private Button lvl1Button, lvl2Button, lvl3Button;
        private StateManager state;

        public MainMenuState(StateManager state) {
            this.state = state;
        }

        public  void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(levelSelector, Vector2.Zero, Color.White);
            lvl1Button.draw(sb);
            lvl2Button.draw(sb);
            lvl3Button.draw(sb);
        }

        public  void Enter()
        {
            //selectorSndInstance = selectorSnd.CreateInstance();
           // selectorSndInstance.Play();
        }

        public  void Leave()
        {
            //selectorSndInstance.Stop();
        }

        public  void Update(GameTime gameTime, InputHandler inputHandler)
        {
            if (lvl1Button.update(inputHandler))
            {
                state.ChangeState(new Level1State(state));
            }
            if (lvl2Button.update(inputHandler))
            {
                state.ChangeState(new Level2State(state));
            }
            if (lvl3Button.update(inputHandler))
            {
                state.ChangeState(new Level3State(state));
            }
        }

        public  void LoadContent(ContentManager content)
        {
            buttons = content.Load<Texture2D>("button-states");
            levelSelector = content.Load<Texture2D>("level-select");
            font = content.Load<SpriteFont>("MenuFont");
            selectorSnd = content.Load<SoundEffect>("levelSelectorSnd");

            lvl1Button = new Button(113, 135, buttons, "Level 1", font);
            lvl2Button = new Button(441, 135, buttons, "Level 2", font);
            lvl3Button = new Button(277, 420, buttons, "Level 3", font);
        }

    }
}
