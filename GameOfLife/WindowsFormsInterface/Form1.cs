using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JeuDeLaVieInterfaceGraphique.Controls;

namespace JeuDeLaVieInterfaceGraphique
{
    public partial class Form1 : Form
    {
        private Timer MyTimer;
        private Game game; 
        private Picture_Box pictureBox;
        private Label_ label;
        private int n;
        private int generation;
        private int pixSize;
        public Form1()
        {
            generation = 0;
            n = 300;
            pictureBox = new Picture_Box(n);
            pictureBox.Location = new Point((Size.Width/2), (Size.Height - n/2) / 2);
            label = new Label_();
            label.Location = new Point(pictureBox.Location.X + (n/2 - label.Width/2), pictureBox.Location.Y + n);
            InitializeComponent();
            Controls.Add(pictureBox);
            Controls.Add(label);
            MyTimer = new Timer();
            MyTimer.Interval = (600);
            MyTimer.Tick += new EventHandler(UpdateGrid);
            MyTimer.Start();
            pictureBox.Paint += new PaintEventHandler(pictureBox_Paint);
            pixSize = 5;
            game = new Game(n, 100);
        }
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Définir une brush blanche
            SolidBrush coloredBrush = new SolidBrush(Color.White);
            var g = e.Graphics;
            // Boucler sur toutes les cellules de la grille
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (game.grid.TabCells[x, y].isAlive) { g.FillRectangle(coloredBrush,x*pixSize, y*pixSize, pixSize, pixSize); }
                }
            }
            e.Dispose();
            // Si la cellule est vivante :
            // Dessiner un rectangle plein de 5x5 pixels à la position correspondante
        }

        private void UpdateGrid(object sender, EventArgs e)
        {
            game.grid.UpdateGrid();
            generation++;
            label.RefreshLabel(generation);
            this.Refresh();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Size = new Size(800,800);
            Text = "Conway's game of life";
        }
    }
}
