using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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

        Texture2D enemyTexture;
        List<Enemy> enemies = new List<Enemy>();

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
            enemyTexture = Content.Load<Texture2D>("Enemy");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            player.Update(delta, bulletTexture, bullets);

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(delta);
                if (bullets[i].position.X > GraphicsDevice.Viewport.Width)
                    bullets.RemoveAt(i);
            }

            SpawnEnemy(delta);
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(delta);
                if (enemies[i].position.X < -100)
                    enemies.RemoveAt(i);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Bullet bullet in bullets)
                bullet.Draw(spriteBatch);
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        float enemySpawnTimer = 0f;
        private void SpawnEnemy(float delta)
        {
            enemySpawnTimer += delta;
            if (enemySpawnTimer > 1)
            {
                int randy = new Random().Next(50, GraphicsDevice.Viewport.Height - 50);
                Vector2 spawnPos = new Vector2(GraphicsDevice.Viewport.Width + 50, randy);
                enemies.Add(new Enemy(enemyTexture, spawnPos));
                enemySpawnTimer = 0;
            }

        }
    }
}
