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

        private readonly List<string> operators = new List<string>(); // Liste mit Operatoren

        private readonly List<double> numbers = new List<double>(); // Liste mit Zahlen

        bool clearInput;

        int counter = 0; // counter wird gebraucht um zu zählen, ob die letzte Angabe ein rechenoperator war
        bool kommacounter;

        public Rechner()
        {
            InitializeComponent();

            // damit am Anfang die Null angezeigt wird
            zahlenFeld.Text = "0";
            clearInput = true;

        }

        private void Rechner_Load(object sender, EventArgs e)
        {

        }

        private void zahl1_Click(object sender, EventArgs e)
        // 9 Verweise, alles verwiesen, somit programmieren wir alle Buttons in Einem
        {
            // zur Bereinigung des Inputs
            if (clearInput)
            {
                zahlenFeld.Text = string.Empty;
                clearInput = false;
            }
            // Zahlenfeld Eingabe 
            zahlenFeld.Text += ((Button)sender).Text;
        }
        private void zahl0_Click(object sender, EventArgs e)
        {
            if (zahlenFeld.Text == "0") //nichts, weil mehrere Nullen hintereinandeer keinen Sinn ergeben
            {
                return;
            }

            zahlenFeld.Text += ((Button)sender).Text;
           
        }

        private void zahlenFeld_Click(object sender, EventArgs e)
        {

        }

        private void buttonMal_Click(object sender, EventArgs e)
        {
            numbers.Add(double.Parse(zahlenFeld.Text));
            operators.Add(((Button)sender).Text);
            textBox1.Text = zahlenFeld.Text + ((Button)sender).Text; // fuegt die Zahlen ins obere Feld ein
            clearInput = true;
            kommacounter = false;
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
            
        }

        private void buttonKomma_Click(object sender, EventArgs e)
        {
            if (kommacounter)
            {
                return;
            }
            this.zahlenFeld.Text += ",";
            kommacounter = true;
            clearInput = false;
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
            zahlenFeld.Text = ""; // entfernt den ganzen Textinhalt
        }

        private void ce_Click(object sender, EventArgs e)
        {
            zahlenFeld.Text = "0";
            clearInput = true;
        }

        private void zahlenFeld_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        // Zahlen negieren kek
        private void vorZeichen_Click(object sender, EventArgs e)
        {
            
        }
    }
}