using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using GoofTroopRemake.JSON;
using GoofTroopRemake.Actor;
using GoofTroopRemake.StateManager;
using GoofTroopRemake.MaxStates;
using Microsoft.Xna.Framework.Input;
using GoofTroopRemake.EnemyState;

namespace GoofTroopRemake.Components
{
    public class Patroling
    {
        public IList<RectangleObjects> rectangles { get; set; }
        public IList<Actor.Actor> actors { get; set; }
        Rectangle resetRectangle;
        static Random rg;

        public Patroling(IList<RectangleObjects> rectangles, IList<Actor.Actor> actors, Rectangle resetRectangle) {
            this.rectangles = rectangles;
            this.actors = actors;
            this.resetRectangle = resetRectangle;
            rg = new Random();
        }

        public void Update(GameTime gameTime, InputHandler inputHandler) {
            Max max = (Max)actors.Last<Actor.Actor>();
            for (int i = 0; i < actors.Count - 1; i++)
            {
                if (actors[i].GetType() == typeof(Enemy))
                {
                    Enemy enemy = (Enemy)actors[i];
                    actors[i].Update(gameTime, inputHandler);
                    PatrolState pState = (PatrolState)enemy.state.state;
                    if (pState.atitude == PatrolState.Atitude.patrol)
                    {
                        checkCollideActors(actors[i]);
                        switchState(actors[i], max);
                    }
                    else {
                        checkCollidePursue(actors[i], rectangles, actors, resetRectangle);
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
                enemy.moveX( );
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
        }

        private void switchState(Actor.Actor actor, Max max)
        {
            double distance = calculateDistance(actor, max);
            Enemy enemy = (Enemy)actor;
            PatrolState patrol = (PatrolState)enemy.state.state;
            if (distance <= 200 && patrol.atitude != PatrolState.Atitude.pursue) {
                patrol.changeState(PatrolState.Atitude.pursue);
            }
        }

        private double calculateDistance(Actor.Actor actor, Max max)
        {
            Vector2 actOrigin = actor.position + new Vector2(40, 47);
            Vector2 maxOrigin = max.position + new Vector2(33, 41);
            return Math.Sqrt(Math.Pow((maxOrigin.X - actOrigin.X),2) + Math.Pow((maxOrigin.Y - actOrigin.Y), 2));
        }
        
        private void checkCollideActors(Actor.Actor actor)
        {
            Enemy enemy = (Enemy)actor;
            bool hasCollide = false;
            foreach (Actor.Actor ac in actors) {
                if (ac.GetType() == typeof(Enemy) && enemy != (Enemy)ac) {
                    Enemy aux = (Enemy)ac;
                    if (enemy.collideRectangle.Intersects(aux.collideRectangle))
                    {
                        hasCollide = true;
                    }
                }
                if (ac.GetType() == typeof(Rock)) {
                    Rock aux = (Rock)ac;
                    if (enemy.collideRectangle.Intersects(aux.collisionRect))
                    {
                        hasCollide = true;
                    }
                }
            }
            foreach (RectangleObjects ro in rectangles) {
                if (enemy.collideRectangle.Intersects(ro.collisionRegion)) {
                    hasCollide = true;
                }
            }
            if (enemy.collideRectangle.Intersects(resetRectangle)) {
                hasCollide = true;
            }

            if (!hasCollide)
            {
                enemy.move();
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
    }
}
