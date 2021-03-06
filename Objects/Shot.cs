﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Puddle
{
    class Shot : Sprite
    {
        public string dir;
        public int speed;
        public int x_vel;

        public Shot(Player p, string dir = "none")
            : base(p.spriteX - 16, p.spriteY - 16, 24, 24)
        {
            this.imageFile = "bubble.png";
            
            this.dir = dir;
            collisionWidth = 8;
            collisionHeight = 8;
            speed = 6;
            x_vel = Convert.ToInt32(p.x_vel * .5 + speed * (p.faceLeft ? -1 : 1));
        }

        public override void Update(Physics physics)
        {
            Move();

            CheckCollisions(physics);
        }

        public void CheckCollisions(Physics physics)
        {
            // Check collisions with enemies
            foreach (Enemy e in physics.enemies)
            {
                if (Intersects(e))
                {
                    destroyed = true;
                    e.health--;
                }
            }

            // check collisions with blocks
            foreach (Block b in physics.blocks)
            {
                if (Intersects(b))
                    destroyed = true;
            }
        }

        public void Move()
        {
            if (dir == "down")
                spriteY += speed / 2;
            else if (dir == "up")
            {
                spriteY -= speed;
            }
            else
                spriteX += x_vel;

        }

        public new void Draw(SpriteBatch sb)
        {
            sb.Draw(
                image,
                new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight),
                new Rectangle(0, 0, 32, 32),
                Color.White,
                0,
                new Vector2(16, 16),
                SpriteEffects.None,
                0
            );


        }
    }
}
