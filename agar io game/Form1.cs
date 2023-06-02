using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace agar_io_game
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, goUp, goDown;
        int playerSpeed = 5;
        int playerSize = 20;
        int items = 0;

        private Random random = new Random();
        private List<PictureBox> pictureBoxes = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            GenerateRandomPictureBoxes(75); // Generate 10 random PictureBoxes on form load
        }
        private void GenerateRandomPictureBoxes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(10, 10); // Set the size of the PictureBox
                pictureBox.Location = GenerateRandomLocation(); // Generate random location

                // Set a random background color
                pictureBox.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                Controls.Add(pictureBox); // Add PictureBox to the form's Controls collection
                pictureBoxes.Add(pictureBox); // Add PictureBox to the list of PictureBoxes
            }
        }

        private Point GenerateRandomLocation()
        {
            int x = random.Next(ClientSize.Width ); // Subtracting 100 to keep PictureBox within form boundaries
            int y = random.Next(ClientSize.Height ); // Subtracting 100 to keep PictureBox within form boundaries
            return new Point(x, y);
        }

         private void moveTimerEvent(object sender, EventArgs e)
        {
            ItmCount.Text = "Items: " + items;
        
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left < 1395)
            {
                player.Left += playerSpeed;
            }
            if (goUp == true && player.Top > 0)
            {
                player.Top -= playerSpeed;
            }
            if (goDown == true && player.Top < 810)
            {
                player.Top += playerSpeed;
            }

            CheckCollision();
        }
        private void CheckCollision()
        {
            foreach (PictureBox pictureBox in pictureBoxes)
            {
                if (player.Bounds.IntersectsWith(pictureBox.Bounds))
                {
                    playerSize += 5; // Increase player's size
                    player.Size = new Size(playerSize, playerSize);
                    items++;

                    pictureBox.Hide(); // Hide the colliding PictureBox
                    pictureBoxes.Remove(pictureBox); // Remove the PictureBox from the list
                    Controls.Remove(pictureBox); // Remove the PictureBox from the form's Controls collection
                    break; // Exit the loop after handling one collision
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

    }
}
