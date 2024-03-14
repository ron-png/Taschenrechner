using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taschenrechner
{
    public partial class Rechner : Form
    {
        // Alle 10 Buttons kann man linken (wichtig!!!) 
        // https://www.youtube.com/watch?v=SMtD308awME  <--- Video 
        // sehr hilfreich war https://github.com/windson/Calculator-Winforms/
        public Rechner()
        {
            InitializeComponent();
        }

        private void Rechner_Load(object sender, EventArgs e)
        {

        }

        private void zahl1_Click(object sender, EventArgs e)
        // 10 Verweise, alles verwiesen, somit programmieren wir alle Buttons in Einem
        {
            this.zahlenFeld.Text += ((Button)sender).Text;
        }

        private void zahlenFeld_Click(object sender, EventArgs e)
        {

        }
    }
}
