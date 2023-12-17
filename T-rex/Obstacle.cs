using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_rex
{
    class Obstacle
    {
        private int locationX;
        private Size size;
        private Bitmap appearance;

        //constructor
        public Obstacle (int apppearanceNumber)
        {
            if(apppearanceNumber == 1)
            {
                appearance = Properties.Resources.obstacle_1;
                size = new Size(23, 46);
            }

            else
            {
                appearance = Properties.Resources.obstacle_2;
                size = new Size(32, 33);

            }
        }

        //proparties

        public int LocationX
        {
            get { return locationX; }
            set { locationX = value; }
        }

        public Size Size
        {
            get { return size;}
        }

        public Bitmap Appearance { get { return appearance; } }


    }
}
