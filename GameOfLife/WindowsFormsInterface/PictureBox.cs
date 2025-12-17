using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JeuDeLaVieInterfaceGraphique.Controls
{
    public class Picture_Box : PictureBox
    {
        public Picture_Box(int n)
        {
            Size = new Size(n, n);
            BackColor = Color.DarkGray;
        }
    }
}
