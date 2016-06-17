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
using GoofTroopRemake.JSON;
using GoofTroopRemake.Actor;
using GoofTroopRemake.StateManager;

namespace GoofTroopRemake.Level
{
    public class Level2State : State
    {
        Texture2D level2, gate;
        SoundEffect levelSnd;
        SoundEffectInstance levelSndInstance;
        private StateManager.StateManager state;
        private IList<Actor.Actor> actors;
        private IList<RectangleObjects> rectangles;
        Rectangle resetRectangle, keyRectangle;
        RectangleObjects gateRectangle;
        Collision collide;
        CheckWin checkWin;
        bool openGate;

        public Level2State(StateManager.StateManager state) {
            this.state = state;
            actors = new List<Actor.Actor>();
            openGate = false;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(level2, Vector2.Zero, Color.White);
            if (!openGate) sb.Draw(gate, new Vector2(336, 168), Color.White);

            foreach (Actor.Actor a in actors)
            {
                a.Draw(sb, gameTime);
            }
        }

        public void Enter()
        {
            levelSndInstance = levelSnd.CreateInstance();
            levelSndInstance.Play();
            rectangles = state.levelManager.levels[1].rectangles;
            //gate
            gateRectangle = new RectangleObjects(336, 192, 72, 48);
            rectangles.Add(gateRectangle);
            //reset
            resetRectangle = new Rectangle(336, 662, 98, 10);
            keyRectangle = new Rectangle(360, 96, 48, 48);

            collide = new Collision(rectangles, actors, state);
            IList<Rectangle> winPosition = new List<Rectangle>();
            winPosition.Add(new Rectangle(143, 143, 50, 50));
            winPosition.Add(new Rectangle(191, 143, 50, 50));
            winPosition.Add(new Rectangle(527, 143, 50, 50));
            winPosition.Add(new Rectangle(575, 143, 50, 50));
            winPosition.Add(new Rectangle(335, 383, 50, 50));
            winPosition.Add(new Rectangle(383, 383, 50, 50));
            checkWin = new CheckWin(winPosition, actors);
        }

        public void Leave()
        {
            levelSndInstance.Stop();
        }

        public void LoadContent(ContentManager content)
        {
            gate = content.Load<Texture2D>("Gate");
            level2 = content.Load<Texture2D>("level2NoGate");
            levelSnd = content.Load<SoundEffect>("levelSnd");

            actors.Add(new Block(content.Load<Texture2D>("block"), new Vector2(192, 432)));
            actors.Add(new Block(content.Load<Texture2D>("block"), new Vector2(192, 480)));
            actors.Add(new Block(content.Load<Texture2D>("block"), new Vector2(288, 336)));
            actors.Add(new Block(content.Load<Texture2D>("block"), new Vector2(384, 336)));
            actors.Add(new Block(content.Load<Texture2D>("block"), new Vector2(480, 384)));
            actors.Add(new Block(content.Load<Texture2D>("block"), new Vector2(488, 288)));
            actors.Add(new Max(content.Load<Texture2D>("MaxWalkingSprite"), content.Load<Texture2D>("MaxKickSprite")));
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            collide.update(gameTime, inputHandler, resetRectangle);
            openGate = checkWin.hasWon();

            if (openGate)
            {
                rectangles.Remove(gateRectangle);
                if (checkWin.gotKey(actors, keyRectangle))
                {
                    Console.WriteLine("WIN");
                    state.ChangeState(new MainMenuState(state));
                }

            }
        }
    }
}
