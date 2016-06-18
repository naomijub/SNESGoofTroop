using GoofTroopRemake.Actor;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoofTroopRemake.MaxStates;
using Microsoft.Xna.Framework.Input;

namespace GoofTroopRemake.Components
{
    public class GrabThrow
    {
        public IList<Actor.Actor> actors { get; set; }
        public Rock rock;

        public GrabThrow(IList<Actor.Actor> actors) {
            this.actors = actors;
            rock = null;
        }

        public void update(GameTime gameTime, InputHandler inputHandler) {
            Max max = null;
            for (int i = 0; i < actors.Count; i++) {
                if (actors[i].GetType() == typeof(Max)) {
                   max = (Max)actors[i];
                    Rock auxRock = (Rock)checkGrab((Max)actors[i], gameTime, inputHandler);
                    if (auxRock != null)
                    {
                        rock = auxRock;
                        rock.moveUp(actors[i].position);
                        rock.updateRockMax(actors[i].position, max.idle);
                    }
                    if (rock != null)
                    {
                        if (!rock.steady && !rock.throwed)
                        {
                            rock.updateRockMax(actors[i].position, max.idle);
                        }
                    }
                }
            }
            if (rock != null) {
                if (inputHandler.KeyDown(Keys.C)) {
                    max.state.ChangeState(new ThrowState(max.state, max));
                    rock.throwed = true;
                }
            }
        }

        public Actor.Actor checkGrab(Max aux, GameTime gameTime, InputHandler inputHandler)
        {
            Point grab = aux.Grab(inputHandler);
            Actor.Actor returnable = null;

            if (grab != Point.Zero)
            {
                
                foreach (Actor.Actor ac in actors)
                {
                    if (ac.GetType() == typeof(Rock))
                    {
                        Console.WriteLine("grabbed");
                        Rock auxRock = (Rock)ac;
                        if (auxRock.collisionRect.Contains(grab)) {
                            aux.state.ChangeState(new GrabState(aux, aux.state));
                            returnable = ac;
                        }
                    }
                }
            }
            return returnable;
        }
    }
}
