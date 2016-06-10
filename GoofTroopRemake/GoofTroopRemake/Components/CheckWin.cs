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

namespace GoofTroopRemake.Components
{
    public class CheckWin
    {
        public IList<Rectangle> rectangles { get; set; }
        public IList<Actor.Actor> actors { get; set; }
        public bool[] completed { get; set; }

        public CheckWin(IList<Rectangle> rectangles, IList<Actor.Actor> actors) {
            this.rectangles = rectangles;
            this.actors = actors;
            completed = new bool[rectangles.Count];
            setCompleted();
        }

        public bool hasWon() {
            int count = 0;
            foreach (Actor.Actor ac in actors) {
                if (ac.GetType() == typeof(Block))
                {
                    Block aux = (Block)ac;
                    foreach (Rectangle rec in rectangles)
                    {
                        if (rec.Contains(aux.collisionRect))
                        {
                            completed[count] = true;
                            count++;
                        }
                    }
                }
            }

            return allCompleted();
        }

        public void setCompleted() {
            for (int i = 0; i < completed.Length; i++) {
                completed[i] = false;
            }
        }

        public bool allCompleted() {
            bool won = true;
            foreach (bool bl in completed) {
                if (bl == false) {
                    won = false;
                }
            }
            //Console.WriteLine(won);
            return won;
        }

        public bool gotKey(IList<Actor.Actor> actors, Rectangle key) {
            foreach (Actor.Actor ac in actors) {
                if (ac.GetType() == typeof(Max)){
                    Max aux = (Max)ac;
                    Console.WriteLine(aux.maxRectangle.ToString());
                    Console.WriteLine(key.ToString());
                    Console.WriteLine();
                    if (aux.maxRectangle.Intersects(key)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
