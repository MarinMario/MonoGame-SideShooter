using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScrollerShooter
{
    class Collider
    {
        public Vector2 position;
        public Vector2 size;

        public static bool Overlaps(Collider collider1, Collider collider2)
        {
            bool colliderX = collider1.position.X + collider1.size.X > collider2.position.X;
            bool colliderY = collider1.position.Y + collider1.size.Y > collider2.position.Y;
            bool colliderW = collider1.position.X < collider2.position.X + collider2.size.Y;
            bool colliderH = collider1.position.Y < collider2.position.Y + collider2.size.Y;
            if (colliderX && colliderY && colliderW && colliderH)
                return true;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            Point positionPoint = new Point((int)position.X, (int)position.Y);
            Point sizePoint = new Point((int)size.X, (int)size.Y);
            spriteBatch.Draw(texture, new Rectangle(positionPoint, sizePoint), new Color(200, 200, 200, 100));
        }
    }
}
