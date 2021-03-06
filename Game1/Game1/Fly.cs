﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1 
{
    class Fly : Enemies
    {
        //fields
        private int timer;


        //properties

        //constructor
        public Fly(Level level, Random rng, int health, int damage, Rectangle position, Texture2D texture) :
            base(level, rng, health, damage, position, texture)
        {   
            affected = false;
            moveSpeed = 3;
        }

        //methods
        /// <summary>
        /// the fly enemy will fly towards the player
        /// </summary>
        /// <param name="player"></param>
        public override void Move(Characters player, GameTime gameTime)
        {
            PrevPos = Position;

            //every frame the character should move a certian distance towards the player
            //to keep that distance consistent a ratio needs to be used since the character is moving at a diagnol
            int xDist = player.Position.X - Position.X;
            int yDist = player.Position.Y - Position.Y;
            Double tDist = DistanceTo(player.Position.X, player.Position.Y, Position.X, Position.Y);

            int xMov = 0;
            int yMov = 0;

            if(tDist != 0)
            {
                double xRatio = xDist / tDist;
                double yRatio = yDist / tDist;

                xMov = (int)(moveSpeed * xRatio);
                yMov = (int)(moveSpeed * yRatio);
            }

            if (xMov != 0)
            {
                yMov += (int)(3 * Math.Sin((timer * Math.PI) / 32));
            }

            Rectangle temp = Position;
            temp.X += xMov;
            temp.Y += yMov;
            Position = temp;
            if(prevPos2.X == Position.X && xMov != 0)
            {

                if (yDist < 0)
                {
                    temp.Y -= moveSpeed;

                }
                else
                {
                    temp.Y += moveSpeed;
                }
                Position = temp;
            }
            if(prevPos2.Y == Position.Y && (yMov != 0))
            {
                if (xDist < 0)
                {
                    temp.X -= moveSpeed;

                }
                else
                {
                    temp.X += moveSpeed;
                }
                Position = temp;
            }
            prevPos2 = Position;
            timer++;
        }
    }
}
