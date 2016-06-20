using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GoofTroopRemake.EnemyState;

namespace GoofTroopRemake.Actor
{
    public class Enemy : Actor
    {
        public StateManager.StateManager state { get; set; }

        public Rectangle source { get; set; }
        public Rectangle collideX { get; set; }
        public Rectangle collideY { get; set; }
        public Rectangle collideRectangle { get; set; }
        public Rectangle auxCollideRectangle { get; set; }

        public Vector2 nextMove { get; set; }
        public float nextMoveX { get; set; }
        public float nextMoveY { get; set; }

        public bool alive { get; set; }

        public static Random rg;

        public Enemy(Texture2D texture, Vector2 position, ContentManager content, InputHandler inputHandler) : base(texture) {
            this.position = position;
            nextMove = position;
            nextMoveX = nextMoveY = 0;
            rg = new Random();
            alive = true;
            setActorState();
            setSource();
            state = new StateManager.StateManager(content, inputHandler);

            state.setPrimaryState(new PatrolState(state, this));
        }

        public void setActorState() {
            int direction = rg.Next(0, 131) % 4;
            switch (direction) {
                case 0: actorState = ActorState.moveUp; break;
                case 1: actorState = ActorState.moveDown; break;
                case 2: actorState = ActorState.moveRight; break;
                case 3: actorState = ActorState.moveLeft; break;
                default: actorState = ActorState.moveUp; break;
            }
        }

        public void setSource() {
            switch (actorState) {
                case ActorState.moveUp: source = new Rectangle(0, 0, 80, 95); break;
                case ActorState.moveDown: source = new Rectangle(0, 95, 80, 95); break;
                case ActorState.moveRight: source = new Rectangle(0, 190, 80, 95); break;
                case ActorState.moveLeft: source = new Rectangle(0, 285, 80, 95); break;
            }
        }

        public override void die()
        {
            alive = false;
            collideRectangle = Rectangle.Empty;
            auxCollideRectangle = Rectangle.Empty;
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            state.Draw(sb, gameTime);
        }

        public override void move()
        {
            position = nextMove;
        }

        public void moveX() {
            position += new Vector2(nextMoveX, 0);
        }

        public void moveY()
        {
            position += new Vector2(0, nextMoveY);
        }

        public override void Update(GameTime gameTime, InputHandler inputHandler)
        {
            state.Update(gameTime);
        }
    }
}
