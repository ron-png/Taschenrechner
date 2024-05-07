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
        bool fullclear; // wird bei String Fehlermeldung gebraucht

        public Rechner()
        {
            InitializeComponent();

            // damit am Anfang die Null angezeigt wird
            nullanzeige();

        }

        // kleine Funktoin, um die Null anzuzeigen
        private void nullanzeige()
        {

            zahlenFeld.Text = "0";// unteres Zahlenfeld einfach eine Null wie beim Windows Taschenrechner
            clearInput = true; //clearinput, damit die null "ersetzt" wird
        }

        private void zahl1_Click(object sender, EventArgs e)
        // 9 Verweise, alles verwiesen, somit programmieren wir alle Buttons in Einem
        {
            // zur Bereinigung des Zahlenfelds
            if (clearInput)
            {
                zahlenFeld.Clear();
                clearInput = false;
            }
            // Zahlenfeld Eingabe 
            zahlenFeld.Text += ((Button)sender).Text;

            fullclear = false;
        }
        private void zahl0_Click(object sender, EventArgs e)
        {
            // wenn es ein null komma gibt
            if (zahlenFeld.Text == "0") //nichts, weil mehrere Nullen hintereinandeer keinen Sinn ergeben
            {
                return; // raus da
            }
            else if (clearInput) // gleiches Bereinigungsschema wie bei den anderen Zahlen
            {
                zahlenFeld.Clear();
                clearInput = false;
            }

            zahlenFeld.Text += ((Button)sender).Text; // Null eingeben
            fullclear = false;

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
            textBox1.Clear();
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
            double zahl;
            zahl = Convert.ToDouble(zahlenFeld.Text);

            if (zahl > 0)
            {
                zahl = -Math.Abs(zahl); //.abs absolute Wert der Zahl, also immer ohne minus 
            }
            else if (zahl < 0)
            {
                zahl =  Math.Abs(zahl);
            }
            else // zahl == 0
            {
                zahl = 0; // 0 bleibt unverändert
            }

            zahlenFeld.Text = Convert.ToString(zahl);
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
            if (fullclear)
            {
                nullanzeige();
            }

            numbers.Add(Convert.ToDouble(zahlenFeld.Text/*.Replace(',', '.')*/)); // konvertiert die eingegebene Zahl zu double und fügt diese zu Liste hinzu
            operators.Add(((Button)sender).Text); // fügt die den Operanten in eine Liste Hinzu
            textBox1.Text += zahlenFeld.Text + ((Button)sender).Text; // fuegt die Zahlen ins obere Feld ein
            clearInput = true; // markiert, um das Feld für neuen Input "frei" zu machen
            kommacounter = false; // Komma kann jetzt auch eingegeben werden
        }

        private void buttonGleich_Click(object sender, EventArgs e)
        {
            if (berechnet)
            {
                return;
            }
            numbers.Add(Convert.ToDouble(zahlenFeld.Text/*.Replace(',', '.')*/));
            textBox1.Text += zahlenFeld.Text + "=";
            zahlenFeld.Text = Calculate(numbers, operators);
            
            try
            {
                Convert.ToDouble(zahlenFeld.Text);
            }
            catch
            {
                fullclear = true;
            }

            clearInput = true;
            berechnet = true;
        }

        #region MATHE 
        // Berechne das zeug
        private string Calculate(List<double> numbers, List<string> operators)
        {
            int numberscounter = 1;
            if (operators.Count == 0)
            {
                return Convert.ToString(numbers[0]); // Falls keine Operationen vorhanden sind, gib die aktuelle Zahl zurück
            }

            double result = numbers[0]; // Setze das Ergebnis auf die aktuelle Zahl

            for (int i = 0; i < operators.Count; i++)
            {
                double nextNumber = numbers[numberscounter];
                string currentOperator = operators[i];
                numberscounter++;
                switch (currentOperator)
                {
                    case "+":
                        result = result + nextNumber;
                        break;
                    case "-":
                        result = result - nextNumber;
                        break;
                    case "*":
                        result = result * nextNumber;
                        break;
                    case "/":
                        if (nextNumber == 0)
                            return "Teilen durch Null ist nicht möglich"; // duh
                        result = result / nextNumber;
                        break;
                    default:
                        return "Ungültiger Operator: " + currentOperator; // falls unbekannter operator kek
                }
            }

            return Convert.ToString(result);
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
        private void Rechner_Load(object sender, EventArgs e)
        {

        }

        private void zahlenFeld_Click(object sender, EventArgs e)
        {

        }


        #endregion
    }
}