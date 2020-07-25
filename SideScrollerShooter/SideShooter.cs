using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SideScrollerShooter
{
    public class SideShooter : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;

        Texture2D bulletTexture;
        List<Bullet> bullets = new List<Bullet>();

        public SideShooter()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D playerTexture = Content.Load<Texture2D>("Player");
            player = new Player(playerTexture, new Vector2(100, 100));

            bulletTexture = Content.Load<Texture2D>("Bullet");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            player.Update(delta, bulletTexture, bullets);
            for (int bulletIndex = 0; bulletIndex < bullets.Count; bulletIndex++)
            {
                bullets[bulletIndex].Update(delta);
                if (bullets[bulletIndex].position.X > GraphicsDevice.Viewport.Width)
                    bullets.RemoveAt(bulletIndex);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Bullet bullet in bullets)
                bullet.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
