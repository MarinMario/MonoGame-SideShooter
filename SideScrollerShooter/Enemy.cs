using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScrollerShooter
{
    class Enemy
    {
        Texture2D texture;
        public Vector2 position;
        public bool shouldDespawn = false;

        public Collider collider = new Collider();

        int speed = 300;

        public Enemy(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;
        }

        public void Update(float delta)
        {
            position.X -= speed * delta;
            collider.position = position;
            collider.size = new Vector2(96);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
