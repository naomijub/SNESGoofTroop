using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoofTroopRemake.StateManager;
using GoofTroopRemake.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using GoofTroopRemake.Actor;
using GoofTroopRemake.JSON;

namespace GoofTroopRemake.Level 
{
    public class Level1State : State
    {
        Texture2D level1, gate;
        SoundEffect levelSnd;
        SoundEffectInstance levelSndInstance;
        private StateManager.StateManager state;
        private IList<Actor.Actor> actors;
        private JSON.Level rectangles;

        public Level1State(StateManager.StateManager state) {
            this.state = state;
            actors = new List<Actor.Actor>();
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(level1, Vector2.Zero, Color.White);
            sb.Draw(gate, new Vector2(336, 168), Color.White);
            foreach (Actor.Actor a in actors) {
                a.Draw(sb, gameTime);
            }
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
            gate = content.Load<Texture2D>("Gate");
            level1 = content.Load<Texture2D>("level1");
            levelSnd = content.Load<SoundEffect>("levelSnd");

            actors.Add(new Max(content.Load<Texture2D>("maxSpriteSheet")));
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
           
        }

        public void LoadRectangles(JSON.Level rectangles) {
            this.rectangles = rectangles;
        }
    }
}
