using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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

        // Listen die jeweils den Operator und die Zahl speichern
        private readonly List<string> operators = new List<string>();

        private readonly List<double> input = new List<double>();

        bool ClearInput;
        public int nullcounter, kommacounter;
        

        public Rechner()
        {
            InitializeComponent();
        }

        private void Rechner_Load(object sender, EventArgs e)
        {

        }

        private void zahl1_Click(object sender, EventArgs e)
        // 9 Verweise, Diese beinhalten die Zahlen 1 bis 9 
        {
            this.zahlenFeld.Text += ((Button)sender).Text;
            // resettet den counter, weil die letzte Eingabe kein Operant oder Komma mehr ist
            nullcounter = 0;
        }

        private void zahl0_Click(object sender, EventArgs e)
        {
            
        }

        private void zahlenFeld_Click(object sender, EventArgs e)
        {

        }

        private void buttonMal_Click(object sender, EventArgs e)
        {
            
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

        private void buttonGleich_Click(object sender, EventArgs e)
        {
           
            string formel = zahlenFeld.Text; // Speichert das String Feld in variable
                                             
            // wird eventuell nicht mehr gebraucht formel = "0" + formel; // fuegt eine Null vorne dran, damit eine Minuszahl am Anfang fuktioniert
            bool nummerTest, operantTest;    //https://code-maze.com/csharp-identify-if-a-string-is-a-number/ um zu testen, ob nur eine Zahl eingegeben wurde
            nummerTest = double.TryParse(formel, out _);
            operantTest = char.IsDigit(formel.Last());//https://code-maze.com/csharp-check-if-string-ends-with-a-number/ um zu testen, ob die Formel mit einem Operanten endet
            if (nummerTest || operantTest==false)
            {
                // macht nichts, weil die Berechnung nicht ausgefuehrt werden kann
            }
            else 
            { 
                // Berechnet den ganzen Spass
                   
            }
        }

        private void buttonKomma_Click(object sender, EventArgs e)
        {
            
        }

        private void backspace_Click(object sender, EventArgs e)
        {
            string formel = zahlenFeld.Text;
            if (formel.Length > 0)
            {
                zahlenFeld.Text = formel.Remove(formel.Length - 1, 1); //https://www.c-sharpcorner.com/blogs/remove-last-character-from-string-in-c-sharp1
            }            
        }

        private void c_Click(object sender, EventArgs e)
        {
            zahlenFeld.Text = ""; // entfernt den ganzen Inhalt
        }

        private void ce_Click(object sender, EventArgs e) //Soll die letzte Zahl entfernen!
        {
            var digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' }; // https://stackoverflow.com/questions/27289054/removing-numbers-at-the-end-of-a-string-c-sharp
            string formel = zahlenFeld.Text;
            zahlenFeld.Text = formel.TrimEnd(digits);
        }

        private void zahlenFeld_TextChanged(object sender, EventArgs e)
        {

        }
    }
}