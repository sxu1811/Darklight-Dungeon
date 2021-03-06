﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Centipede : Enemies
    {

        //fields
        private bool moveUp;
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;
        private double walkTimer;
        private double aniTimer;
        private double fps;
        private double frameDuration;
        private double walkDuration;
        private int frame;
        private bool prevUp;
        private bool prevDown;
        private bool prevLeft;
        private bool prevRight;
        private Texture2D rotTexture;

        //properties
        public bool MoveUp
        {
            get { return moveUp; }
        }
        public bool MoveDown
        {
            get { return moveDown; }
        }
        public bool MoveLeft
        {
            get { return moveLeft; }
        }
        public bool MoveRight
        {
            get { return moveRight; }
        }
        public int Frame
        {
            get { return frame; }
        }
        public Texture2D RotTexture
        {
            get { return rotTexture; }
        }


        //constructor
        public Centipede(Level level, Player player, Random rng, int health, int damage, Rectangle position, Texture2D texture, Texture2D rotTexture) : base(level, rng, health, damage, position, texture)
        {
            affected = false;
            moveSpeed = 2;
            walkTimer = 0;
            aniTimer = 0;
            fps = 30.0;
            frameDuration = 3.0/fps;
            walkDuration = 3;
            SelectDirection(player);
            prevLeft = true;
            this.rotTexture = rotTexture;
        }

        //methods
        public override void Move(Characters player, GameTime gameTime)
        {
            //this character moves towards the player in segments
            //It moves towards the player in either the x or y direction
            //Every time the time
            Rectangle temp = Position;
            prevPos = Position;

            walkTimer += gameTime.ElapsedGameTime.TotalSeconds;
            aniTimer += gameTime.ElapsedGameTime.TotalSeconds;


            if (moveUp)
            { 
                temp.Y -= moveSpeed;
            }
            if (moveDown)
            {
                temp.Y += moveSpeed;
            }
            if (moveLeft)
            {
                temp.X -= moveSpeed;
            }
            if (moveRight)
            {
                temp.X += moveSpeed;
            }
            if(walkTimer >= walkDuration)
            {
                SelectDirection(player);
                temp = Position;
                walkTimer = 0;
            }
            if(aniTimer >= frameDuration)
            {
                frame++;
                if(frame > 3)
                {
                    frame = 0;
                }
                aniTimer = 0;
            }

            Position = temp;
            if (prevPos2.X == Position.X && (moveLeft || moveRight))
            {
                if (MoveUp)
                {
                    temp.Y -= moveSpeed;

                }
                else
                {
                    temp.Y += moveSpeed;
                }
                Position = temp;
            }
            if (prevPos2.Y == Position.Y && (moveUp || moveDown))
            {

                if (moveLeft)
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
        }

        /// <summary>
        /// method that selects what direction the enemy should move in based on the distance from the player
        /// if the x distance is greater than the y distance than it will move it will set the corresponding boolean to true
        /// if the y distance is gerater than the x distance then it will set the corresponding boolean to true
        /// </summary>
        /// <param name="player"></param>
        public void SelectDirection(Characters player)
        {
            //booleans to keep track of the previous direction chosen to help with the rotations of the hitbox
            prevDown = MoveDown;
            prevUp = moveUp;
            prevLeft = moveLeft;
            prevRight = moveRight;

            //set all movement related booleans to true
            moveDown = false;
            moveUp = false;
            moveLeft = false;
            moveRight = false;

            //get absolute value of distance in x and y direction
            int xDist = Math.Abs(Position.X - player.Position.X);
            int yDist = Math.Abs(Position.Y - player.Position.Y);

            //set the correct boolean to true
            //in addition the sprite is a rectangle so the hitbox needs to be changed correspondingly
            Rectangle temp = Position;
            if(xDist > yDist)
            {
                
                if (!prevRight && !prevLeft)//if the rectangle is vertical
                {

                    temp.Width = 200;
                    temp.Height = 60;
                }

                if((Position.X - player.Position.X) < 0)
                {
                    moveRight = true;
                    if (prevDown)
                    {
                        temp.X = Position.X - 200;
                        temp.Y = Position.Y + Position.Height;
                    }
                }
                else
                {
                    moveLeft = true;
                    if (prevDown)
                    {
                        temp.X = Position.X + Position.Width;
                        temp.Y = Position.Y + Position.Height;
                    }
                }
            }
            else
            {               
                if (!prevUp && !prevDown)
                {
                    //temp.X = Position.X + 70;
                    //temp.Y = Position.Y - 70;
                    temp.Width = 53;
                    temp.Height = 200;
                }

                if ((Position.Y - player.Position.Y) < 0)
                {
                    moveDown = true;
                }
                else
                {
                    moveUp = true;
                }
            }
            Position = temp;

            
        }
    }
}
