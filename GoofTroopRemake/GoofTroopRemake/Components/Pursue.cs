using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using GoofTroopRemake.Actor;
using GoofTroopRemake.StateManager;
using GoofTroopRemake.MaxStates;
using Microsoft.Xna.Framework.Input;
using GoofTroopRemake.EnemyState;
using GoofTroopRemake.JSON;

namespace GoofTroopRemake.Components
{
    public class Pursue
    {
        public IList<Actor.Actor> actors { get; set; }
        public StateManager.StateManager state { get; set; }
        static Random rg;

        public Pursue(IList<Actor.Actor> actors, StateManager.StateManager state) {
            this.actors = actors;
            rg = new Random();
        }

        public void pursue(GameTime gameTime, InputHandler inputHandler, IList<RectangleObjects> rectangles,
            IList<Actor.Actor> actors, Rectangle resetRectangle) {
            Max max = (Max)actors.Last<Actor.Actor>();
            for (int i = 0; i < actors.Count - 1; i++)
            {
                if (actors[i].GetType() == typeof(Enemy))
                {
                    Enemy enemy = (Enemy)actors[i];
                    
                    if (enemy.state.GetType() == typeof(PursueState))
                    {
                        Console.WriteLine("Pursue");
                        actors[i].Update(gameTime, inputHandler);
                        checkCollidePursue(actors[i], rectangles, actors,resetRectangle);
                    }
                }
            }
            
        }

        private void checkCollidePursue(Actor.Actor actor, IList<RectangleObjects> rectangles,
            IList<Actor.Actor> actors, Rectangle resetRectangle)
        {
            Enemy enemy = (Enemy)actor;
            bool hasCollideX = false;
            bool hasCollideY = false;
            foreach (Actor.Actor ac in actors)
            {
                if (ac.GetType() == typeof(Enemy) && enemy != (Enemy)ac)
                {
                    Enemy aux = (Enemy)ac;
                    if (enemy.collideX.Intersects(aux.collideRectangle))
                    {
                        hasCollideX = true;
                    }
                    if (enemy.collideY.Intersects(aux.collideRectangle))
                    {
                        hasCollideY = true;
                    }
                }
                if (ac.GetType() == typeof(Rock))
                {
                    Rock aux = (Rock)ac;
                    if (enemy.collideX.Intersects(aux.collisionRect))
                    {
                        hasCollideX = true;
                    }
                    if (enemy.collideY.Intersects(aux.collisionRect))
                    {
                        hasCollideY = true;
                    }
                }
            }
            foreach (RectangleObjects ro in rectangles)
            {
                if (enemy.collideX.Intersects(ro.collisionRegion))
                {
                    hasCollideX = true;
                }
                if (enemy.collideY.Intersects(ro.collisionRegion))
                {
                    hasCollideY = true;
                }
            }
            if (enemy.collideX.Intersects(resetRectangle))
            {
                hasCollideX = true;
            }
            if (enemy.collideY.Intersects(resetRectangle))
            {
                hasCollideY = true;
            }

            if (!hasCollideX && hasCollideY)
            {
                enemy.moveX();
            }
            else if (hasCollideX && !hasCollideY)
            {
                enemy.moveY();
            }
            else if (!hasCollideX && !hasCollideY)
            {
                enemy.moveX();
                enemy.moveY();
            }
            else {
                changeDirection(enemy);
            }
        }

        private void changeDirection(Enemy enemy)
        {
            if (enemy.actorState == Actor.Actor.ActorState.moveUp || enemy.actorState == Actor.Actor.ActorState.moveDown)
            {
                int direction = rg.Next(0, 51) % 2;
                switch (direction)
                {
                    case 0: enemy.actorState = Actor.Actor.ActorState.moveRight; break;
                    case 1: enemy.actorState = Actor.Actor.ActorState.moveLeft; break;
                }
            }
            else {
                int direction = rg.Next(0, 51) % 2;
                switch (direction)
                {
                    case 0: enemy.actorState = Actor.Actor.ActorState.moveUp; break;
                    case 1: enemy.actorState = Actor.Actor.ActorState.moveDown; break;
                }
            }
        }

        //needs fixing in max rectangle area
        public bool foundMax() {
            Max max = (Max)actors.Last<Actor.Actor>();
            foreach (Actor.Actor ac in actors) {
                if(ac.GetType() == typeof(Enemy)){
                    Enemy enemy = (Enemy)ac;
                    if (enemy.collideRectangle.Intersects(max.maxRectangle)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
