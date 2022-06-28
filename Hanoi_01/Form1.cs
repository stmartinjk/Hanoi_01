using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;


namespace Hanoi_01
{
    public partial class Form1 : Form
    {
        //create List moves and List_TowerDisk
        private List<string> moves = new List<string>();
        private List<Disks> _TowerDisks = new List<Disks>();
        AnimateView animate = new AnimateView();
        //Make Disk start 1, set Disksize(Hieght,Weight)
        int _DiskCount = 1;
        int diskHeight = 40;
        int baseHeight = 20;

        public Form1()
        {
            //Automatically Created
            InitializeComponent();
            //Operating Animation on Panel1 
            AnimateView.view = panel1;
            //Reset as like clean Panel1 after view Animate
            resetPanel();
        }
        //Create disk
        private void populateDisks()
        {
            int B = 1;
            foreach (Disks disk in _TowerDisks)
            {
                PictureBox panelBox = disk.box;
                panelBox.BackColor = colorSelector(disk);
                disk.width = 200 - (20 * B);
                panelBox.Width = disk.width;
                panelBox.Height = diskHeight;
                panelBox.Tag = disk.DiskNo;
                panelBox.BorderStyle=BorderStyle.FixedSingle;
                Point boxLocation = new Point(getDiskX(disk), (panel1.Height - baseHeight) - (diskHeight * B++));
                panelBox.Location = boxLocation;
                disk.box = panelBox;
                panel1.Controls.Add(disk.box);
            }
        }
        //Create Tower
        private int getDiskX(Disks disk)
        {
            int X = 0;
            int peg = 0;
            switch (disk.peg)
            {
                case 'A': peg = 1; break;
                case 'B': peg = 2; break;
                case 'C': peg = 3; break;
            }
            X = ((panel1.Width / 4) * peg) - (int)(disk.width / 2);
            return X;
        }
        //Reset and setup Disks
        private void resetPanel()
        {
            setupTower();
            panel1.Controls.Clear();
            _TowerDisks = Enumerable.Range(1, _DiskCount).Select(i => new Disks() { DiskNo = i, peg = 'A', box = new PictureBox() }).OrderByDescending(i => i.DiskNo).ToList();
            populateDisks();
            lblCounter.Text = string.Format("Best Possible moves{0}", GetMoveCount(_DiskCount));
        }
        //Create disks with count button
        private int getDiskY(Disks disk)
        {
            int Y = 0;
            int stackNo = _TowerDisks.Count(x => x.peg == disk.peg);
            Y = panel1.Height - baseHeight - (diskHeight * stackNo);
            return Y;
        }
        //Change Disk Color with Rainbow order
        private Color colorSelector(Disks disk)
        {
            switch (disk.DiskNo)
            {
                case 1: return Color.Red;
                case 2: return Color.Orange;
                case 3: return Color.Yellow;
                case 4: return Color.Green;
                case 5: return Color.Blue;
                default: return Color.White;
            }
        }
        //Create towers (3 Towers)
        private void solveTower(int numberOfDisks)
        {
            char oneT = 'A';
            char threeT = 'C';
            char twoT = 'B';
            solveTowers(numberOfDisks, oneT, threeT, twoT);
        }
        //SloveTowers (oneT->threeT)
        private void solveTowers(int n, char oneT, char threeT, char twoT)
        {
            if (n > 0)
            {
                solveTowers(n-1, oneT, twoT, threeT);
                Disks currentDisk = _TowerDisks.Find(x => x.DiskNo == n);
                currentDisk.peg = threeT;
                animate.moveUp(currentDisk.box, 50);
                if (oneT < threeT)
                    animate.moveRight(currentDisk.box, getDiskX(currentDisk));
                else
                    animate.moveLeft(currentDisk.box, getDiskX(currentDisk));
                animate.moveDown(currentDisk.box, getDiskY(currentDisk));

                string line = string.Format("Move disk {0} from {1} to{2}", n, oneT, threeT);
                Console.WriteLine(line);
                moves.Add(line);
                solveTowers(n - 1, twoT, threeT, oneT);
            }
        }
        public static int GetMoveCount(int numberOfDisks)
        {   //Mersenne number(2**n)-1 | time complexity O(2**n)
            double numberOfDisks_Double = numberOfDisks;
            return (int)Math.Pow(2.0, numberOfDisks_Double) - 1;
        }
        private void DiskCount_ValueChanged(object sender, EventArgs e)
        {
            _DiskCount = (int)DiskCount.Value;
            resetPanel();
        }
        private void setupTower()
        {
            panel1.Paint += delegate
            {
                setBase();
            };
        }
        private void setBase()
        {
            SolidBrush sb = new SolidBrush(Color.RosyBrown);
            Graphics g = panel1.CreateGraphics();
            int topSpacing = 120;
            int width = 40;
            g.FillRectangle(sb, 0, panel1.Height - baseHeight, panel1.Width, baseHeight);
            drawPeg(panel1, g, sb, 1, width, topSpacing);
            drawPeg(panel1, g, sb, 2, width, topSpacing);
            drawPeg(panel1, g, sb, 3, width, topSpacing);
        }
        private void drawPeg(Panel canvas,Graphics g, SolidBrush sb, int pegNo, int pegWidth, int topSpacing)
        {
            g.FillRectangle(sb, ((int)(canvas.Width / 4) * pegNo) - (pegWidth / 2), topSpacing, pegWidth, canvas.Height - topSpacing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            resetPanel();
            btnSolve.Enabled = false;
            moves.Clear();
            int NumberOfDisks = _DiskCount;
            solveTower(NumberOfDisks);
            listMoves.DataSource = null;
            listMoves.DataSource = moves;
            btnSolve.Enabled = true;
        }

        private void DiskCount_ValueChanged_1(object sender, EventArgs e)
        {
            _DiskCount = (int)DiskCount.Value;
            resetPanel();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thank you! Good bye!");
            Application.Exit();
        }
    }
}
