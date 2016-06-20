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

        //needs fixing in max rectangle area
        public bool foundMax() {
            Max max = (Max)actors.Last<Actor.Actor>();
            foreach (Actor.Actor ac in actors) {
                if(ac.GetType() == typeof(Enemy)){
                    Enemy enemy = (Enemy)ac;
                    if (enemy.auxCollideRectangle.Intersects(max.maxRectangle)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
