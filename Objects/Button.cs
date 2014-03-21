﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using TiledSharp;

namespace Puddle
{
    class Button : Sprite
    {
        public bool activating;
        public bool activated;
        public string name;

        //TODO: add in function passing for individual button actions
        public Button(TmxObjectGroup.TmxObject obj) :
            base(obj.X, obj.Y, 32, 32)
        {
            this.imageFile = "button.png";
            this.name = obj.Name;
            faceLeft = false;
            sizeX = 24;
            sizeY = 30;
            if (obj.Properties["direction"].Equals("left"))
            {
                faceLeft = true;
                spriteX -= 9;
            }
            else
                spriteX += 9;
            
        }

        public override void Update(Physics physics)
        {
            Animate(physics);
        }

        public void Animate(Physics physics)
        {
            if (activating && frameIndex < (32 * 7))
                frameIndex += 32;
        }

        public void Action(Physics physics)
        {
            if (activated)
                return;
            activating = true;
            activated = true;
            if (this.name == "Button 1")
            {
                foreach (Block b in physics.blocks)
                {
                    if (b.name == "Block 1")
                    {
                        b.changeType("push");
                        b.gravity = true;//Do some action
                    }
                }
            }
            else if (this.name == "Button 2")
            {
                foreach (Block b in physics.blocks)
                {
                    if (b.name == "Block 2")
                    {
                        b.changeType("push");
                        b.gravity = true;//Do some action
                    }
                }
            }
            else if (this.name == "Button 3")
            {
                foreach (Block b in physics.blocks)
                {
                    if (b.name == "Block 10")
                    {
                        b.changeType("push");
                        b.gravity = true;
                        
                    }
                    else if (b.name == "Block 11")
                    {
                        b.changeType("push");
                        b.gravity = true;
                        b.y_vel = 2;
                    }
                }
            }

        }
    }
}
