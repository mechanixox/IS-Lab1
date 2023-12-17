using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_rex
{
    class Character
    {
        private int speed, jumpForce, ojumpForce, jumpSpeed, locationY, locationX, score;
        private Size size;
        private Bitmap appearance;
        private Bitmap deadAppearance;
        private bool jump, gameover;

        //constructors
        public Character() //default values
        {
            speed = 5;
            jumpForce = 18;
            ojumpForce = 24;
            jumpSpeed = 10;
            appearance = Properties.Resources.running;
            ImageAnimator.Animate(appearance, Anim); //this will start the frame changing process of the running.gif
            deadAppearance = Properties.Resources.dead;
            size = new Size(40, 43);
            jump = false;
            gameover = false;
        }

        public Character(int Speed, int originalJumpForce, int jSpeed)
        {
            speed = Speed;
            jumpForce = 18;
            ojumpForce = 88;
            jumpSpeed = 10;
            appearance = Properties.Resources.running;
            ImageAnimator.Animate(appearance, Anim);
            deadAppearance = Properties.Resources.dead;
            size = new Size(40, 43);
            jump = false;
            gameover = false;
        }

        //main character method, updated by each frame
        public void Update(List<Obstacle> obstacles)
        {
            ImageAnimator.UpdateFrames();

            // find the nearest obstacle
            int minDistance = int.MaxValue;
            int secondNearestDistance = int.MaxValue;

            foreach (var obstacle in obstacles)
            {
                // calculating the character distance to the obstacle
                int distance = obstacle.LocationX - (350 + size.Width);

                if (distance > 30 && distance < minDistance)
                {
                    secondNearestDistance = minDistance; // store the previous nearest obstacle's distance
                    minDistance = distance; // update minDistance with the new nearest obstacle's distance
                    nearestObstacleX = obstacle.LocationX;
                }
                else if (distance > 30 && distance < secondNearestDistance)
                {
                    // update secondNearestDistance with the new second nearest obstacle's distance
                    secondNearestDistance = distance; 
                }
            }

            if (locationY < 411 - size.Height)
            {
                if (!jump)
                {
                    locationY += 14;
                }
            }
            else if (!jump)
            {
                jumpForce = ojumpForce;
            }

            //jump when an obstacle is within a certain distance
            if (minDistance > 0 && minDistance < 40) 
            {
                //check if this is a consecutive obstacle
                if (minDistance < 30)
                {
                    consecutiveObstacles++;
                }
                else
                {
                    consecutiveObstacles = 0;
                }

                //perform a high jump for consecutive obstacles
                if (consecutiveObstacles >= 2)
                {
                    jumpForce = 30; 
                }
                else
                {
                    jumpForce = 15;
                }

                jump = true;
            }
            else if (jump && jumpForce > 0)
            {
                jumpForce--;
                locationY -= jumpSpeed;
            }

            if (jumpForce == 0)
            {
                jump = false;
            }

            if (locationY > 411 - size.Height)
            {
                locationY = 411 - size.Height;
            }

            if (CollissionDetection(obstacles))
            {
                appearance = deadAppearance;
                gameover = true;
            }
        }



        //properties

        private int nearestObstacleX;
        private int consecutiveObstacles = 0;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public Bitmap Appearance
        {
            get { return appearance; } 
        }

        public Size Size 
        {
            get { return size; } 
        }
        
        public bool Jump
        {
            set { jump = value; }
        }

        public int Speed
        {
            get { return speed; }
        }

        public int LocationY
        {
            get { return locationY; }
        }

        public int LocationX
        {
            get { return locationX; }
        }

        public bool GameOver
        {
            get { return  gameover; }
        }

        private void SetSpeed()
        {
            if (score % 10 == 0 && score < 10)
            {
                speed += 5;
            }
            else if(score % 10 == 1)
            {
                speed += 10;
            }
        }

        public bool OnGround() // check if the character is on ground
        {
            return locationY == 411 - size.Height;
        }


        public bool CollissionDetection(List<Obstacle> obstacles)
        {
            bool collission = false;
            int i = 0;
            while (i < obstacles.Count() && !collission) 
            {
                if(locationY + Size.Height > 420 - obstacles[i].Size.Height)
                {
                    if (335 + size.Width > obstacles[i].LocationX && obstacles[i].LocationX + obstacles[i].Size.Width > 350)
                    {
                        collission = true;
                    }
                }
                i++;
            }
            return collission;
        }
        private void Anim(object o, EventArgs e)
        {

        }
    }
}
