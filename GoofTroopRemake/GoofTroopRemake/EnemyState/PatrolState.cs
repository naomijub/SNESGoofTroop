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

namespace GoofTroopRemake.EnemyState
{
    public class PatrolState : State
    {
        public Enemy enemy { get; set; }
        public StateManager.StateManager state { get; set; }
        private Actor.Actor.ActorState auxState;

        private int variation = 0;

        public PatrolState(StateManager.StateManager state, Enemy enemy) {
            this.enemy = enemy;
            this.state = state;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(enemy.texture, enemy.position, enemy.source, Color.White);
        }

        public void Enter()
        {
            auxState = enemy.actorState;
        }

        public void Leave()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            enemy.nextMove = enemy.position;
            NextMoveUpdate();
            variateSprite(gameTime);
            Vector2 rectPosition = enemy.nextMove + new Vector2(10, 48);
            Vector2 enemyPos = enemy.position + new Vector2(10, 0);
            enemy.collideRectangle = new Rectangle(rectPosition.ToPoint(), new Point(40, 47));
            enemy.auxCollideRectangle = new Rectangle(enemyPos.ToPoint(), new Point(44, 85));
        }

        private void NextMoveUpdate()
        {
            switch (enemy.actorState) {
                case Actor.Actor.ActorState.moveUp: enemy.nextMove += new Vector2(0, -2); break;
                case Actor.Actor.ActorState.moveDown: enemy.nextMove += new Vector2(0, 2); break;
                case Actor.Actor.ActorState.moveRight: enemy.nextMove += new Vector2(2, 0); break;
                case Actor.Actor.ActorState.moveLeft: enemy.nextMove += new Vector2(-2, 0); break;
            }
        }

        public void variateSprite(GameTime gameTime)
        {
            if (auxState != enemy.actorState)
            {
                variation = 0;
                auxState = enemy.actorState;
            }

            variateWalking();

            if ((gameTime.TotalGameTime.Milliseconds % 100) == 0)
            {
                variation++;
            }

            if (variation >= 4) {
                variation = 0;
            }
        }

        public void variateWalking()
        {

            int auxVar = 80 * (variation % 4);
            switch (auxState)
            {
                case Actor.Actor.ActorState.moveUp: enemy.source = new Rectangle((auxVar), 95, 80, 95); break;
                case Actor.Actor.ActorState.moveDown: enemy.source = new Rectangle((auxVar), 0, 80, 95); break;
                case Actor.Actor.ActorState.moveRight: enemy.source = new Rectangle((auxVar), 190, 80, 95); break;
                case Actor.Actor.ActorState.moveLeft: enemy.source = new Rectangle((auxVar), 285, 80, 95); break;
            }
        }
    }
}
