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
        public enum Atitude { patrol, pursue}
        public Enemy enemy { get; set; }
        public Max max { get; set; }
        public StateManager.StateManager state { get; set; }
        private Actor.Actor.ActorState auxState;
        public Atitude atitude { get; set; }
        public Atitude auxAtitude { get; set; }

        private int variation = 0;

        public PatrolState(StateManager.StateManager state, Enemy enemy, Max max) {
            this.enemy = enemy;
            this.state = state;
            this.max = max;
            atitude = auxAtitude = Atitude.patrol;
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
            if (atitude == Atitude.patrol)
            {
                enemy.nextMove = enemy.position;
                NextMoveUpdate();
                variateSprite(gameTime);
                Vector2 rectPosition = enemy.nextMove + new Vector2(10, 48);
                Vector2 enemyPos = enemy.position + new Vector2(10, 0);
                enemy.collideRectangle = new Rectangle(rectPosition.ToPoint(), new Point(40, 47));
                enemy.auxCollideRectangle = new Rectangle(enemyPos.ToPoint(), new Point(44, 85));
            }
            if (atitude == Atitude.pursue)
            {
                NextMoveUpdate(max, enemy);
                variateSprite(gameTime);
                Vector2 rectPositionX = enemy.position + new Vector2(enemy.nextMoveX, 0) + new Vector2(10, 48);
                Vector2 rectPositionY = enemy.position + new Vector2(0, enemy.nextMoveY) + new Vector2(10, 48);
                Vector2 enemyPos = enemy.position + new Vector2(10, 0);
                enemy.collideX = new Rectangle(rectPositionX.ToPoint(), new Point(40, 47));
                enemy.collideY = new Rectangle(rectPositionY.ToPoint(), new Point(40, 47));
                enemy.auxCollideRectangle = new Rectangle(enemyPos.ToPoint(), new Point(44, 85));
            }
        }

        private void NextMoveUpdate(Max max, Enemy enemy)
        {
            Vector2 aux = maxEnemyVector(max, enemy);
            Console.WriteLine(aux.ToString());
            enemy.nextMoveX = aux.X;
            enemy.nextMoveY = aux.Y;
        }

        private double calculateDistance(Vector2 aux)
        {
            return Math.Sqrt(Math.Pow(aux.X, 2) + Math.Pow(aux.Y, 2));
        }

        private Vector2 maxEnemyVector(Max max, Enemy enemy)
        {
            Vector2 maxOrigin = max.position + new Vector2(33, 41);
            Vector2 enemyOrigin = enemy.position + new Vector2(40, 47);
            Vector2 aux = new Vector2(maxOrigin.X - enemyOrigin.X, maxOrigin.Y - enemyOrigin.Y);
            //Console.WriteLine(aux.ToString());
            int smallest = Modulus(aux);
            if (calculateDistance(aux) >= 210 || calculateDistance(aux) <= 15)
            {
                atitude = Atitude.patrol;
            }

            return aux / smallest;
        }

        private int Modulus(Vector2 aux)
        {
            if (Math.Abs(aux.X) <= Math.Abs(aux.Y))
            {
                return (int)aux.X;
            }
            return (int)aux.Y;
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
            if (auxAtitude != atitude) {
                auxAtitude = atitude;
                variation = 0;
            }

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

        public void changeState(Atitude atitude) {
            this.atitude = atitude;
        }
    }
}
