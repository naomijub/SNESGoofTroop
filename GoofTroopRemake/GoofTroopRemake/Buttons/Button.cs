using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoofTroopRemake.Buttons
{
    public class Button
    {
        private Texture2D buttonsSprite;
        private Vector2 pos, size, strPos;
        private string name;
        private SpriteFont font;
        private bool isSelected;
        private Rectangle buttonRectanlge;

        public Button(int x, int y, Texture2D buttonsSprite, string name, SpriteFont font) {
            pos = new Vector2(x, y);
            this.buttonsSprite = buttonsSprite;
            this.name = name;
            this.font = font;
            isSelected = false;
            setStringParameters();
            buttonRectanlge = new Rectangle((int)pos.X, (int)pos.Y, 215, 203);
        }

        public void setStringParameters() {
            size = font.MeasureString(name);
            float strPosX = ((215 - size.X) / 2) + pos.X;
            strPos = new Vector2(strPosX, (pos.Y + 80));
        }

        public bool update(InputHandler inputHandler) {
            Point mousePoint = new Point(Convert.ToInt32(inputHandler.MousePosition.X), Convert.ToInt32(inputHandler.MousePosition.Y));
            if (buttonRectanlge.Contains(mousePoint))
            {
                isSelected = true;
                return isKeyPressed(inputHandler);
            }
            else {
                isSelected = false;
                return false;
            }
        }

        public void draw(SpriteBatch sb) {
            if (isSelected)
            {
                drawSelected(sb);
            }
            else {
                drawUnselected(sb);
            }
        }

        private void drawSelected(SpriteBatch sb) {
            Rectangle source = new Rectangle(215, 0, 215, 203);
            sb.Draw(buttonsSprite, pos, source, Color.White);
            sb.DrawString(font, name, strPos, Color.LightGreen);

        }

        private void drawUnselected(SpriteBatch sb) {
            Rectangle source = new Rectangle(0, 0, 215, 203);
            sb.Draw(buttonsSprite, pos, source, Color.White);
            sb.DrawString(font, name, strPos, Color.DarkGreen);
        }

        public bool isKeyPressed(InputHandler inputHandler) {
            if (inputHandler.KeyPressed(Keys.Enter) || inputHandler.KeyPressed(Keys.Space)
                    || inputHandler.MouseLeftButtonPressed())
            {
                return true;
            }
            return false;
        }
    }
}
