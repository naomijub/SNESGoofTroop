using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoofTroopRemake.Components;
using GoofTroopRemake.JSON;
using GoofTroopRemake.Actor;
using GoofTroopRemake.StateManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace GoofTroopRemake.Level
{
    public class Level3State : State
    {
        Texture2D level3, gate;
        SoundEffect levelSnd;
        SoundEffectInstance levelSndInstance;
        private StateManager.StateManager state;
        private IList<Actor.Actor> actors;
        private IList<RectangleObjects> rectangles;
        Rectangle resetRectangle;
        RectangleObjects gateRectangle;
        Collision collide;
        GrabThrow grabThrow;
        bool openGate;
        InputHandler inputHandler;

        public Level3State(StateManager.StateManager state, InputHandler inputHandler) {
            this.state = state;
            actors = new List<Actor.Actor>();
            openGate = false;
            this.inputHandler = inputHandler;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(level3, Vector2.Zero, Color.White);
            if (!openGate) sb.Draw(gate, new Vector2(336, 24), Color.White);

            foreach (Actor.Actor a in actors)
            {
                a.Draw(sb, gameTime);
            }
        }

        public void Enter()
        {
            //levelSndInstance = levelSnd.CreateInstance();
            //levelSndInstance.Play();
            rectangles = state.levelManager.levels[2].rectangles;
            //gate
            gateRectangle = new RectangleObjects(336, 24, 96, 72);
            rectangles.Add(gateRectangle);
            //reset
            resetRectangle = new Rectangle(336, 662, 98, 10);
            collide = new Collision(rectangles, actors, state);
            grabThrow = new GrabThrow(actors);
        }

        public void Leave()
        {
            //levelSndInstance.Stop();
        }

        public void LoadContent(ContentManager content)
        {
            level3 = content.Load<Texture2D>("level3");
            levelSnd = content.Load<SoundEffect>("levelSnd");
            gate = content.Load<Texture2D>("Gate");

            actors.Add(new Rock(content.Load<Texture2D>("rock"), new Vector2(672, 528)));
            actors.Add(new Rock(content.Load<Texture2D>("rock"), new Vector2(672, 576)));
            actors.Add(new Rock(content.Load<Texture2D>("rock"), new Vector2(72, 576)));
            actors.Add(new Rock(content.Load<Texture2D>("rock"), new Vector2(336, 240)));
            actors.Add(new Rock(content.Load<Texture2D>("rock"), new Vector2(648, 96)));
            actors.Add(new Max(content.Load<Texture2D>("MaxWalkingSprite"), content, inputHandler));
            
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            collide.update(gameTime, inputHandler, resetRectangle);
            grabThrow.update(gameTime, inputHandler);

        }
    }
}
