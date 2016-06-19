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

namespace GoofTroopRemake.Components
{
    public class RockHit
    {
        public IList<Actor.Actor> actors { get; set; }
        GrabThrow rock;

        public RockHit(IList<Actor.Actor> actors, GrabThrow rock) {
            this.actors = actors;
            this.rock = rock;
        }

        public void Update(GameTime gameTime, InputHandler inputHandler) {
            if (rock.rock != null)
            {
                if (rock.rock.throwed)
                {
                    for (int i = 0; i < actors.Count - 1; i++)
                    {
                        if (actors[i].GetType() == typeof(Enemy))
                        {
                            Enemy enemy = (Enemy)actors[i];
                            if (rock.rock.collisionRect.Intersects(enemy.auxCollideRectangle))
                            {
                                actors[i].die();
                                actors.Remove(actors[i]);
                                rock.rock.die();
                                actors.Remove(rock.rock);
                            }
                        }
                    }

                }
            }
        }

    }
}
