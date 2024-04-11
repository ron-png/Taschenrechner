using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taschenrechner
{
    public partial class Rechner : Form
    {
        // Alle 10 Buttons kann man linken
        // https://www.youtube.com/watch?v=SMtD308awME  <--- Video 
        // sehr hilfreich war https://github.com/windson/Calculator-Winforms/
        // auch: https://github.com/microsoft/calculator und https://github.com/scaretos/taschenrechner/

        
        int counter = 0; // counter wird gebraucht um zu zählen, ob die letzte Angabe ein rechenoperator war

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
            counter = 0;
        }

        private void zahlenFeld_Click(object sender, EventArgs e)
        {

        }

        private void buttonMal_Click(object sender, EventArgs e)
        {
            if (counter == 0)
            {
                this.zahlenFeld.Text += ((Button)sender).Text;
                counter++;
            }
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {

        }

        private void buttonGeteilt_Click(object sender, EventArgs e)
        {

        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {

        }
    }
}
