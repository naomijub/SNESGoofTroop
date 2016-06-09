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
        private IList<RectangleObjects> rectangles;
        Rectangle resetRectangle;
        Collision collide;

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
            //levelSndInstance = levelSnd.CreateInstance();
            //levelSndInstance.Play();
            rectangles = state.levelManager.levels[0].rectangles;
            //gate
            rectangles.Add(new RectangleObjects(336, 192, 98, 48));
            //reset
            resetRectangle = new Rectangle(336, 662, 98, 10);

            collide = new Collision(rectangles, actors, state);
        }

        public void Leave()
        {
            //levelSndInstance.Stop();
        }

        public void LoadContent(ContentManager content)
        {
            gate = content.Load<Texture2D>("Gate");
            level1 = content.Load<Texture2D>("level1");
            levelSnd = content.Load<SoundEffect>("levelSnd");

           
            actors.Add(new Block(content.Load<Texture2D>("block"), new Vector2(288, 336)));
            actors.Add(new Block(content.Load<Texture2D>("block"), new Vector2(432, 336)));
            actors.Add(new Block(content.Load<Texture2D>("block"), new Vector2(624, 288)));
            actors.Add(new Block(content.Load<Texture2D>("block"), new Vector2(576, 480)));
            actors.Add(new Max(content.Load<Texture2D>("MaxWalkingSprite"), content.Load<Texture2D>("MaxKickSprite")));

        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            collide.update(gameTime, inputHandler, resetRectangle);
        }
    }
}
