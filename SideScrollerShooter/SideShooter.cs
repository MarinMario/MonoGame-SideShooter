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

        Texture2D colliderTexture;
        SpriteFont font;

        int score = 0;
        Vector2 initialPlayerPos;

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
            initialPlayerPos = new Vector2(100, GraphicsDevice.Viewport.Height / 2);
            player = new Player(playerTexture, initialPlayerPos);

            bulletTexture = Content.Load<Texture2D>("Bullet");
            enemyTexture = Content.Load<Texture2D>("Enemy");
            colliderTexture = Content.Load<Texture2D>("Collider");
            font = Content.Load<SpriteFont>("Arial");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            player.Update(delta, bulletTexture, bullets);

            foreach(Bullet bullet in bullets)
            {
                bullet.Update(delta);
                if (bullet.position.X > GraphicsDevice.Viewport.Width)
                    bullet.shouldDespawn = true;
            }

            foreach(Enemy enemy in enemies)
            {
                enemy.Update(delta);
                if (enemy.position.X < -100)
                    enemy.shouldDespawn = true;
            }

            CheckCollision();
            SpawnEnemy(delta);
            Despawn();

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

            spriteBatch.DrawString(font, $"Score: {score}", new Vector2(10, 10), Color.White);

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

        private void CheckCollision()
        {
            foreach (Enemy enemy in enemies)
            {
                if (Collider.Overlaps(player.collider, enemy.collider))
                {
                    enemy.shouldDespawn = true;
                    score = 0;
                    player.position = initialPlayerPos;
                }

                foreach (Bullet bullet in bullets)
                    if (Collider.Overlaps(bullet.collider, enemy.collider))
                    {
                        bullet.shouldDespawn = true;
                        enemy.shouldDespawn = true;
                        score += 1;
                    }
            }
        }

        private void Despawn()
        {
            for (int i = 0; i < enemies.Count; i++)
                if (enemies[i].shouldDespawn)
                    enemies.RemoveAt(i);
            for (int i = 0; i < bullets.Count; i++)
                if (bullets[i].shouldDespawn)
                    bullets.RemoveAt(i);
        }
    }
}
