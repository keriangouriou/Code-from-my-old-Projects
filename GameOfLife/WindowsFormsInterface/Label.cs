using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JeuDeLaVieInterfaceGraphique.Controls
{
    public class Label_ : Label
    {
        public Label_()
        {
            BorderStyle = BorderStyle.FixedSingle;
            Size = new Size(100, 23);
            TextAlign = ContentAlignment.MiddleCenter;
        }

        public void RefreshLabel(int g)
        {
            Text = ($"Génération : {g}");
        }

    }
}
