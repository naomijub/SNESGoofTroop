using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GoofTroopRemake.Components
{
    public interface State
    {
        void LoadContent(ContentManager content);
        void Enter();
        void Leave();
        void Draw(SpriteBatch sb, GameTime gameTime);
        void Update(GameTime gameTime, InputHandler inputHandler);
    }
}
