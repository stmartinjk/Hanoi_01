﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Hanoi_01
{
    class AnimateView
    {
        public static Panel view;
        public void moveUp(PictureBox Disk, int newY)
        {
            while (Disk.Location.Y > newY)
            {
                Disk.Location = new System.Drawing.Point(Disk.Location.X, Disk.Location.Y - 10);
                view.Refresh();
                Thread.Sleep(5);
            }
        }
        public void moveDown(PictureBox Disk, int newY)
        {
            while (Disk.Location.Y < newY)
            {
                Disk.Location = new System.Drawing.Point(Disk.Location.X, Disk.Location.Y + 10);
                view.Refresh();
                Thread.Sleep(5);
            }
        }
        public void moveRight(PictureBox Disk, int newX)
        {
            while (Disk.Location.X < newX)
            {
                Disk.Location = new System.Drawing.Point(Disk.Location.X+9, Disk.Location.Y);
                view.Refresh();
                Thread.Sleep(5);
            }
        }
        public void moveLeft(PictureBox Disk, int newX)
        {
            while (Disk.Location.X > newX)
            {
                Disk.Location = new System.Drawing.Point(Disk.Location.X-9, Disk.Location.Y);
                view.Refresh();
                Thread.Sleep(5);
            }
        }
    }
}
