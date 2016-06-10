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

namespace GoofTroopRemake.Components
{
    public class Collision
    {
        //Franklin Gothic Heavy
        public IList<RectangleObjects> rectangles { get; set; }
        public IList<Actor.Actor> actors { get; set; }
        public StateManager.StateManager state { get; set; }
        public Actor.Actor movingBlock { get; set; }

        public Collision(IList<RectangleObjects> rectangles, IList<Actor.Actor> actors, StateManager.StateManager state) {
            this.actors = actors;
            this.rectangles = rectangles;
            this.state = state;
        }

        public void update(GameTime gameTime, InputHandler inputHandler, Rectangle resetRectangle) {
            for (int i = 0; i < actors.Count; i++)
            {

                if (actors[i].GetType() == typeof(Max))
                {
                    actors[i].Update(gameTime, inputHandler);
                    Max aux = (Max)actors[i];

                    collideMax(aux);
                    if (movingBlock == null)
                    {
                        movingBlock = checkKick(aux, gameTime, inputHandler);
                    }

                    if (aux.maxRectangleY.Intersects(resetRectangle))
                    {
                        state.ChangeState(new MainMenuState(state));
                    }
                }
                if (actors[i] == movingBlock) {
                    Block auxBlock = (Block)movingBlock;
                    auxBlock.Update(gameTime, inputHandler);
                    collideBlocks(gameTime, inputHandler);
                }
            }
        }

        public void collideBlocks(GameTime gameTime, InputHandler inputHandler) {
            Block aux = (Block)movingBlock;
            bool collide = false;
            foreach (RectangleObjects ro in rectangles) {
                if (aux.nextRectangle.Intersects(ro.collisionRegion)) {
                    aux.hasCollide();
                    collide = true;
                    movingBlock = null;
                    //Console.WriteLine("FUCKING collided");
                }
            }
            foreach (Actor.Actor ac in actors) {
                if (ac.GetType() != typeof(Max) && ac != movingBlock) {
                    Block auxBlock = (Block)ac;
                    if (aux.nextRectangle.Intersects(auxBlock.collisionRect)) {
                        aux.hasCollide();
                        collide = true;
                        movingBlock = null;
                    }
                }
            }
            if (!collide) {
                aux.Update(gameTime, inputHandler);
                aux.move();
            }
            
        }

        public Actor.Actor checkKick(Max aux, GameTime gameTime, InputHandler inputHandler) {
            Point kick = aux.Kick(inputHandler);
            Actor.Actor returnable = null;
            if (kick != Point.Zero)
            {
                foreach (Actor.Actor ac in actors)
                {
                    if (ac.GetType() == typeof(Block))
                    {
                        Block auxBlock = (Block)ac;
                        //Console.WriteLine(auxBlock.collisionRect.ToString());
                        //Console.WriteLine(auxBlock.collisionRect.Intersects(new Rectangle(kick, new Point(10, 10))));
                        if (auxBlock.collisionRect.Contains(kick.X, kick.Y))
                        {
                            //Console.WriteLine("pluff");
                           //Console.WriteLine((kick - aux.position.ToPoint()).ToString());
                           auxBlock.setDir(CalculateVector(kick - aux.position.ToPoint()));
                           auxBlock.Update(gameTime, inputHandler);
                            returnable = ac;

                        }
                        
                    }
                }
            }
            return returnable;
        }

        public Point CalculateVector(Point vect)
        {
            if (vect.Y == 71)
            {
                if (vect.X == 8)
                {
                    return new Point(-8, 0);
                }
                else {
                    return new Point(8, 0);
                }
            }
            else {
                if (vect.Y == 96)
                {
                    return new Point(0, 8);
                }
                else {
                    return new Point(0, -8);
                }
            }
        }

        public void collideMax(Max auxMax) {
            bool collideX = false;
            bool collideY = false;

            foreach (RectangleObjects ro in rectangles)
            {
                if (auxMax.maxRectangleX.Intersects(ro.collisionRegion))
                {
                    collideX = true;
                }
                if (auxMax.maxRectangleY.Intersects(ro.collisionRegion))
                {
                    collideY = true;
                }
            }
            foreach (Actor.Actor ac in actors) {
                if (ac.GetType() == typeof(Block)) {
                    //Console.WriteLine(ac.GetType());
                    Block auxBlock = (Block)ac;
                    if (auxMax.maxRectangleX.Intersects(auxBlock.collisionRect))
                    {
                        //Console.WriteLine("Collide x");
                        collideX = true;
                    }
                    if (auxMax.maxRectangleY.Intersects(auxBlock.collisionRect))
                    {
                        //Console.WriteLine("Collide y");
                        collideY = true;
                    }
                }
            }

            if (!collideX && !collideY) { auxMax.move(); }
            else if (!collideX && collideY) { auxMax.moveX(); }
            else if (collideX && !collideY) { auxMax.moveY(); }
        }
    }
}
