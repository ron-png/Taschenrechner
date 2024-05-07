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

        bool clearInput; // falls die Textbox gecleart werden muss
        bool kommacounter; // um zu gucken, ob schon ein Komma eingegeben wurde. Habe vorher "counter"´genutzt, jedoch jetzt bool
        bool berechnet; // wenn gleich gedrückt wurde

        public Rechner()
        {
            InitializeComponent();

            // damit am Anfang die Null angezeigt wird
            nullanzeige();

        }

        // kleine Funktoin, um die Null anzuzeigen
        private void nullanzeige()
        {
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
            else if (clearInput)
            {
                zahlenFeld.Text = string.Empty;
                clearInput = false;
            }

            zahlenFeld.Text += ((Button)sender).Text;
           
        }

        private void zahlenFeld_Click(object sender, EventArgs e)
        {

        }

        private void buttonMal_Click(object sender, EventArgs e)
        {
            if (zahlenFeld.Text.EndsWith(","))
            { 
                zahlenFeld.Text = zahlenFeld.Text.Substring(0, zahlenFeld.Text.Length - 1); // Falls die Eingabe mit einem Komma endet
            }
            if (berechnet)
            {
                operators.Clear();
                numbers.Clear();
                textBox1.Clear();
                berechnet = false;
            }

            numbers.Add(Convert.ToDouble(zahlenFeld.Text.Replace(',', '.'))); // konvertiert die eingegebene Zahl zu double und fügt diese zu Liste hinzu
            operators.Add(((Button)sender).Text); // fügt die den Operanten in eine Liste Hinzu
            textBox1.Text += zahlenFeld.Text + ((Button)sender).Text; // fuegt die Zahlen ins obere Feld ein
            clearInput = true; // markiert, um das Feld für neuen Input "frei" zu machen
            kommacounter = false; // Komma kann jetzt auch eingegeben werden
        }

        


        private void buttonKomma_Click(object sender, EventArgs e)
        {
            if (kommacounter)
            {
                return;
            }
            else if (clearInput)
            {
                zahlenFeld.Text = "0,"; // falls nach einem Operator direkt ein Komma eingegeben wird
            }
            else
            {
                zahlenFeld.Text += ",";
            }
            
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
            else
            {
                nullanzeige();
            }
        }

        private void c_Click(object sender, EventArgs e)
        {
            nullanzeige();
            operators.Clear();
            numbers.Clear();
        }

        private void ce_Click(object sender, EventArgs e)
        {
            nullanzeige();
        }

        

        // Zahlen negieren kek
        private void vorZeichen_Click(object sender, EventArgs e)
        {
            
        }


        private void buttonGleich_Click(object sender, EventArgs e)
        {
            if (berechnet)
            {
                return;
            }
            double result = Calculate(numbers, (Convert.ToDouble(zahlenFeld.Text.Replace(',', '.'))), operators);
            textBox1.Text += zahlenFeld.Text + "=";
            zahlenFeld.Text = Convert.ToString(result);
            clearInput = true;
            berechnet = true;
        }

        #region MATHE 
        // Berechne das zeug
        private double Calculate(List<double> numbers, double currentNumber, List<string> operators)
        {

            if (operators.Count == 0)
            {
                return currentNumber; // Falls keine Operationen vorhanden sind, gib die aktuelle Zahl zurück
            }

            double result = currentNumber; // Setze das Ergebnis auf die aktuelle Zahl

            for (int i = 0; i < operators.Count; i++)
            {
                double nextNumber = numbers[i];
                string currentOperator = operators[i];

                switch (currentOperator)
                {
                    case "+":
                        result += nextNumber;
                        break;
                    case "-":
                        result -= nextNumber;
                        break;
                    case "*":
                        result *= nextNumber;
                        break;
                    case "/":
                        if (nextNumber == 0)
                            throw new ArithmeticException("Teilen durch Null ist nicht erlaubt");
                        result /= nextNumber;
                        break;
                    default:
                        throw new ArithmeticException("Ungültiger Operator: " + currentOperator);
                }
            }

            return result;
        }

        #endregion















        #region ungenutzes Zeug
        private void buttonPlus_Click(object sender, EventArgs e)
        {

        }

        private void buttonGeteilt_Click(object sender, EventArgs e)
        {

        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {

        }

        private void zahlenFeld_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion
    }
}