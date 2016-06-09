using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoofTroopRemake.Actor
{
    public abstract class Actor
    {
        public enum ActorState
        {
            idle,
            moveUp, moveDown, moveLeft, moveRight
        }

        public ActorState actorState { get; set; }
        public Vector2 position { get; set; }
        public Vector2 instantMovement { get; set; }
        public Texture2D texture { get; set; }


        public Actor(Texture2D texture)
        {
            this.texture = texture;
            actorState = ActorState.idle;
        }

        public abstract void attack();
        public abstract void die();
        public abstract void move();
        public abstract void Draw(SpriteBatch sb, GameTime gameTime);
        public abstract void Update(GameTime gameTime, InputHandler inputHandler);
    }
}
