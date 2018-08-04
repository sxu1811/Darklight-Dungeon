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
    class Enemies : Characters
    {
        //fields
        private Random rng;
        private int initialX;
        private int initialY;
        private int direction;
        private bool hit;
        protected bool affected;
        
        //properties
        public bool Hit
        {
            get { return hit; }
            set { hit = value; }
        }
        public bool Affected
        {
            get { return affected; }
            set { affected = value; }
        }


        //constructor
        public Enemies(Random rng, int health, int damage, Rectangle position, Texture2D texture) : base(health, damage, position, texture)
        {
            hit = false;
            affected = false;
            this.rng = rng;
            initialX = Position.X;
            initialY = Position.Y;
            moveSpeed = 3;
            direction = rng.Next(0, 4);
        }

        public override void Move(Characters player)
        {
            prevPos = Position;
            int xDist = player.Position.X - Position.X;
            int yDist = player.Position.Y - Position.Y;
            Double tDist = DistanceTo(player.Position.X, player.Position.Y, Position.X, Position.Y);

            double ratio = moveSpeed / tDist;

            int xMov = (int)(ratio * xDist);
            int yMov = (int)(ratio * yDist);

            
            Rectangle temp = Position;
            temp.X += xMov;
            temp.Y += yMov;
            Position = temp;

        }

        public override void TakeDamage(Player damaged, Characters damager)
        {
            if (damager.Position.Intersects(damaged.Position))
            {
                if(damaged.Armor is ThornArmor)
                {
                    damaged.Armor.ArmorAction(this);
                }
                else
                {
                    damaged.Armor.ArmorAction();
                }
                if(damaged.Armor is ShieldArmor temp)
                {
                    temp = (ShieldArmor)damaged.Armor;
                    if (temp.Popped)
                    {
                        damaged.Health -= damage - damaged.Armor.Defense;
                    }
                }
                else
                {
                    damaged.Health -= damage - damaged.Armor.Defense;

                }
            }
        }
    }
}

