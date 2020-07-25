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
        public Vector2 position;
        Vector2 velocity = Vector2.Zero;
        int speed = 300;
        int acceleration = 600;
        int friction = 1000;

        public Player(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;
        }

        public void Update(float delta)
        {
            Vector2 inputVector = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                inputVector.X = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                inputVector.X = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                inputVector.Y = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                inputVector.Y = 1;

            if (inputVector != Vector2.Zero)
            {
                inputVector /= inputVector.Length();
                velocity += MoveVector(velocity, inputVector * speed, acceleration * delta);
            }
            else
                velocity += MoveVector(velocity, Vector2.Zero, friction * delta);

            position += velocity * delta;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        private Vector2 MoveVector(Vector2 vector, Vector2 target, float amount)
        {
            Vector2 direction = (target - vector) / (target - vector).Length();
            if (vector != target)
                return direction * amount;
            return Vector2.Zero;
        }
    }
}
