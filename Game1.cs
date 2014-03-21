﻿#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using TiledSharp;
#endregion

namespace Puddle
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Physics physics;
        Player player1;
        Enemy enemy1;
        Controls controls;
        TmxMap map;
        Texture2D background;

        public Game1()
            : base()
        {
            graphics  = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //new Scenegraph(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Initialize all your objects here
            map = new TmxMap("Content/Demo.tmx");

            graphics.PreferredBackBufferWidth = map.Width * map.TileWidth;
            graphics.PreferredBackBufferHeight = map.Height * map.TileHeight;

            background = Content.Load<Texture2D>("background.png");

            player1 = new Player(50, 200, 32, 32);
            physics = new Physics(player1);
            controls = new Controls();

            
            /*foreach (TmxObjectGroup.TmxObject obj in map.ObjectGroups["Blocks"].Objects)
            {
                Block block = new Block(obj);
                physics.blocks.Add(block);
            }
            foreach (TmxObjectGroup.TmxObject obj in map.ObjectGroups["Ground"].Objects)
            {
                Block block = new Block(obj);
                physics.blocks.Add(block);
            }
             * */
            foreach (TmxObjectGroup group in map.ObjectGroups)
            {
                foreach (TmxObjectGroup.TmxObject obj in group.Objects)
                {
                    Type t = Type.GetType(obj.Type);
                    object item = Activator.CreateInstance(t, obj);
                    if (item is Block)
                        physics.blocks.Add((Block)item);
                    else
                        physics.items.Add((Sprite)item);
                }
            }
            base.Initialize();            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Load your content here
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player1.LoadContent(this.Content);

            foreach (Block b in physics.blocks)
                b.LoadContent(this.Content);
            foreach (Sprite item in physics.items)
                item.LoadContent(this.Content);
            // TODO: use this.Content to load your game content here
            //new TileMap(this, Registry.Lookup<Scenegraph>(), @"Content\Level1.tmx");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //set our keyboardstate tracker update can change the gamestate on every cycle
            controls.Update();

            if (controls.onPress(Keys.Escape, Buttons.Back))
                Exit();

            // TODO: Add your update logic here

            player1.Update(controls, physics, this.Content);
            physics.Update(this.Content);

            foreach (Block b in physics.blocks)
                b.Update(physics);

            foreach (Enemy e in physics.enemies)
                e.Update(physics);

            foreach (Sprite s in physics.items)
                s.Update(physics);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);

            // BG
            /*Texture2D tt = new Texture2D(GraphicsDevice, 1, 1);
            tt.SetData(new Color[] { Color.ForestGreen });

            spriteBatch.Draw(
                tt,
                new Rectangle(0, 308, 900, 300),
                Color.White
            );*/
            //Texture2D background = Content.Load<Texture2D>("background.png");
            //spriteBatch.Draw(background, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            // Draw here
            player1.Draw(spriteBatch);
            foreach (Enemy e in physics.enemies)
                e.Draw(spriteBatch);
            foreach (Shot s in physics.shots)
                s.Draw(spriteBatch);
            foreach (Block b in physics.blocks)
                b.Draw(spriteBatch);
            foreach (Sprite item in physics.items)
                item.Draw(spriteBatch);
            foreach (Fireball f in physics.fireballs)
            {
                f.Draw(spriteBatch);
                Console.WriteLine("It's somewhere, kid.");
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

       /* protected override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }*/
    }
}
