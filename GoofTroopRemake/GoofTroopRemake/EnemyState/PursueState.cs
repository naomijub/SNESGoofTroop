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

//Needs fixing - stops when collide
namespace GoofTroopRemake.EnemyState
{
    public class PursueState : State
    {
        public Enemy enemy { get; set; }
        public StateManager.StateManager state { get; set; }
        private Actor.Actor.ActorState auxState;
        public Max max { get; set; }
        

        private int variation = 0;

        public PursueState(StateManager.StateManager state, Enemy enemy, Max max)
        {
            this.enemy = enemy;
            this.state = state;
            this.max = max;
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
            NextMoveUpdate(max, enemy);
            variateSprite(gameTime);
            Vector2 rectPosition = enemy.nextMove + new Vector2(10, 48);
            Vector2 enemyPos = enemy.position + new Vector2(10, 0);
            enemy.collideRectangle = new Rectangle(rectPosition.ToPoint(), new Point(40, 47));
            enemy.auxCollideRectangle = new Rectangle(enemyPos.ToPoint(), new Point(44, 85));
        }

        private void NextMoveUpdate(Max max, Enemy enemy)
        {
            Vector2 aux = maxEnemyVector(max, enemy);
            defineActorState(aux);
            enemy.nextMove += aux;
        }

        private double calculateDistance(Vector2 aux)
        {
            return Math.Sqrt(Math.Pow(aux.X, 2) + Math.Pow(aux.Y, 2));
        }

        private void defineActorState(Vector2 aux)
        {
            if (aux.Y >= aux.X)
            {
                if (aux.Y <= 0)
                {
                    enemy.actorState = Actor.Actor.ActorState.moveUp;
                }
                else {
                    enemy.actorState = Actor.Actor.ActorState.moveDown;
                }
            }
            else {
                if (aux.X >= 0)
                {
                    enemy.actorState = Actor.Actor.ActorState.moveRight;
                }
                else {
                    enemy.actorState = Actor.Actor.ActorState.moveLeft;
                }
            }
        }

        private Vector2 maxEnemyVector(Max max, Enemy enemy)
        {
            Vector2 maxOrigin = max.position + new Vector2(33, 41);
            Vector2 enemyOrigin = enemy.position + new Vector2(40, 47);
            Vector2 aux = new Vector2(maxOrigin.X - enemyOrigin.X, maxOrigin.Y - enemyOrigin.Y);
            int smallest = Modulus(aux);
            if (calculateDistance(aux) >= 300)
            {
                enemy.state.ChangeState(new PatrolState(enemy.state, enemy));
            }

            return aux / smallest;
        }

        private int Modulus(Vector2 aux)
        {
            if (aux.X <= aux.Y) {
                return (int)aux.X;
            }
            return (int)aux.Y;
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

            if (variation >= 4)
            {
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
