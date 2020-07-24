using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScrollerShooter
{
    class Player
    {
        Texture2D texture;
        Vector2 position;
        int speed = 300;

        public Player(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;
        }

        public void Update(float delta)
        {
            Vector2 inputVector = new Vector2(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                inputVector.X = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                inputVector.X = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                inputVector.Y = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                inputVector.Y = 1;

            if (inputVector != new Vector2(0, 0))
                inputVector.Normalize();

            position += inputVector * speed * delta;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
