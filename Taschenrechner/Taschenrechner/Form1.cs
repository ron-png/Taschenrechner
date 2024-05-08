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
        bool berechnet; // wenn gleich gedrückt wurde
        bool fullclear; // wird bei String Fehlermeldung gebraucht, damit der String nicht als
                        // "Zahl" übernommen wird

        public Rechner()
        {
            InitializeComponent();

            // damit am Anfang die Null angezeigt wird
            nullanzeige();

        }

        // kleine Funktoin, um die Null anzuzeigen
        private void nullanzeige()
        {
            zahlenFeld.Text = "0";// unteres Zahlenfeld einfach eine Null wie beim Windows
                                  // Taschenrechner
            clearInput = true; //clearinput, damit die null "ersetzt" wird
        }

        private void zahl1_Click(object sender, EventArgs e)
        // 9 Verweise, alles verwiesen, somit programmieren wir alle Buttons in Einem
        {
            // zur Bereinigung des Zahlenfelds
            if (powerclicked)
            {
                clearInput = false;
                advancedOperators = false;
            }
            else if (clearInput || advancedOperators)
            {
                zahlenFeld.Clear();
                clearInput = false; // muss wieder auf falsé gestellt werden, weil die Zahlen
                                    // sonst immer rausgeschmissen werden
                advancedOperators = false;
            }
            // Zahlenfeld Eingabe 
            zahlenFeld.Text += ((Button)sender).Text;

            fullclear = false; //fullclear wird nicht gebraucht, weil die Zahl das Feld mit der
                               //Fehlermeldung ersetzt
        }
        private void zahl0_Click(object sender, EventArgs e)
        {
            // wenn es ein null komma gibt
            if (zahlenFeld.Text == "0") //nichts, weil mehrere Nullen hintereinandeer keinen
                                        //Sinn ergeben
            {
                return; // raus da
            }
            else if (powerclicked)
            {
                clearInput = false;
                advancedOperators = false;
            }
            else if (clearInput || advancedOperators) // gleiches Bereinigungsschema wie bei
                                                      // den anderen Zahlen
            {
                zahlenFeld.Clear();
                clearInput = false;
                advancedOperators = false;
            }

            zahlenFeld.Text += ((Button)sender).Text; // Null eingeben
            fullclear = false; //fullclear wird nicht gebraucht, weil die Zahl das Feld mit der
                               //Fehlermeldung ersetzt
        }


        bool denyKomma; // um zu gucken, ob schon ein Komma eingegeben wurde.
                        // Habe vorher "counter"´genutzt, jedoch jetzt bool
        private void buttonKomma_Click(object sender, EventArgs e)
        {
            if (denyKomma) // wenn denyKomma true, dann kann kein weiteres Komma eingegeben werden
            {
                return;
            }
            else if (clearInput)
            {
                zahlenFeld.Text = "0,"; // falls nach einem Operator direkt ein Komma eingegeben wird, wird automatisch die Null davor geschrieben
                clearInput = false;
            }
            else
            {
                zahlenFeld.Text += ","; // sonst einach normal das Komma eingeben
            }
            
            denyKomma = true; // denyKomma wird auf True geschaltet, weil wir sonst mehrere Kommas hintereinander eingeben können
        }

        private void backspace_Click(object sender, EventArgs e)
        {
            string formel = zahlenFeld.Text;
            // die Zahl wird pro Click úm eins reduziert
            if (formel.Length > 0) // das geschieht, wenn die Länge des Strings das auch hergibt
            {
                zahlenFeld.Text = formel.Remove(formel.Length - 1, 1); //https://www.c-sharpcorner.com/blogs/remove-last-character-from-string-in-c-sharp1
            }
            else //sonst wird einfach eine Null angezeigt
            {
                nullanzeige();
            }
        }

        // alles löschen
        private void c_Click(object sender, EventArgs e)
        {

            nullanzeige(); // zeigt die Null an
            textBox1.Clear(); // bereinigt die obere Textbox
            operators.Clear(); // bereinigt die Liste mit den Operatoren
            numbers.Clear(); // bereinigt die Liste mit den Zahlen
        }

        // löscht nur die untere Reihe
        private void ce_Click(object sender, EventArgs e)
        {
            nullanzeige(); // zeigt die Null an
        }

        // Zahlen negieren kek
        private void vorZeichen_Click(object sender, EventArgs e)
        {
            double zahl; //einfach die Zahl zum negieren
            zahl = Convert.ToDouble(zahlenFeld.Text); //hier wird die Zahl geladen

            if (zahl > 0) // wenn die zahl Größer als null, dann minuszahl erstellen
            {
                zahl = -Math.Abs(zahl); //.abs absolute Wert der Zahl, also immer ohne minus 
            }
            else if (zahl < 0)
            {
                zahl =  Math.Abs(zahl); // wenn die zahl kleiner als null, dann plus erstellen
            }                           //.abs absolute Wert der Zahl, also immer ohne minus
            else // zahl == 0           
            {
                zahl = 0; // 0 bleibt unverändert
            }

            zahlenFeld.Text = Convert.ToString(zahl); // die Zahl wird hier wieder ins
                                                      // Zahlenfeld ausgespuckt
        }


        // operanten
        #region advancedFeatures
        bool advancedOperators;
        bool lastclickadvanced;
        string lastinput;
        
        private void wurzel_Click(object sender, EventArgs e)
        {
            string zahl = zahlenFeld.Text;
            if (berechnet)
            {
                textBox1.Clear();
                GleichGedrueckt();
            }
            else if (lastclickadvanced)
            {
                if (((Button)sender).Text == "√")
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length-lastinput.Length);
                    lastclickadvanced = false;
                }
                else if (((Button)sender).Text == "sin")
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - lastinput.Length);
                    lastclickadvanced = false;

                }
                else if (((Button)sender).Text == "cos")
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - lastinput.Length);
                    lastclickadvanced = false;

                }
                else if (((Button)sender).Text == "tan")
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - lastinput.Length);
                    lastclickadvanced = false;

                }
                else if (((Button)sender).Text == "^2")
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - lastinput.Length);
                    lastclickadvanced = false;

                }
            } 
            try
            {
                if (((Button)sender).Text == "√")
                {
                    zahlenFeld.Text = Convert.ToString(Math.Sqrt(Convert.ToDouble(zahlenFeld.Text)));
                    textBox1.Text += ((Button)sender).Text + "(" + zahl + ")";
                    lastinput = ((Button)sender).Text + "(" + zahl + ")";
                }
                else if (((Button)sender).Text == "sin")
                {
                    zahlenFeld.Text = Convert.ToString(Math.Sin(Convert.ToDouble(zahlenFeld.Text)));
                    textBox1.Text += ((Button)sender).Text + "(" + zahl + ")";
                    lastinput = ((Button)sender).Text + "(" + zahl + ")";
                }
                else if (((Button)sender).Text == "cos")
                {
                    zahlenFeld.Text = Convert.ToString(Math.Cos(Convert.ToDouble(zahlenFeld.Text)));
                    textBox1.Text += ((Button)sender).Text + "(" + zahl + ")";
                    lastinput = ((Button)sender).Text + "(" + zahl + ")";
                }
                else if (((Button)sender).Text == "tan")
                {
                    zahlenFeld.Text = Convert.ToString(Math.Tan(Convert.ToDouble(zahlenFeld.Text)));
                    textBox1.Text += ((Button)sender).Text + "(" + zahl + ")";
                    lastinput = ((Button)sender).Text + "(" + zahl + ")";
                }
                else if (((Button)sender).Text == "^2")
                {
                    zahlenFeld.Text = Convert.ToString(Math.Pow(Convert.ToDouble(zahlenFeld.Text), 2));
                    textBox1.Text += "(" + zahl + ")" + ((Button)sender).Text;
                    lastinput = "(" + zahl + ")" + ((Button)sender).Text;
                }
                advancedOperators = true;
                lastclickadvanced = true;
            }
            catch
            {
                return;
            }
        }

        bool powerclicked;
        private readonly List<double> powernumbers = new List<double>(); // Liste mit Zahlen

        private void power_Click(object sender, EventArgs e)
        {
            if (zahlenFeld.Text.EndsWith(","))
            {
                zahlenFeld.Text = zahlenFeld.Text.Substring(0,
                    zahlenFeld.Text.Length - 1); // Falls die Eingabe mit
                                                 // einem Komma endet
            }
            if (berechnet)
            {
                //textBox1.Clear();
                GleichGedrueckt();
                //berechnet = false;
            }
            if (fullclear) // wenn es einen fehler gab, dann ist fullclear auf true
            {
                nullanzeige();
            }
            string zahl = zahlenFeld.Text;
            try
            {
                powernumbers.Add(Convert.ToDouble(zahlenFeld.Text));
                zahlenFeld.Text += ((Button)sender).Text;
                powerclicked = true;
                denyKomma = false;
            }
            catch
            {
                return;
            }
        }
        private void GleichGedrueckt() //Wenn Gleich gedrueckt wird, muss die alte Formel
                                       //und Zahlen geloescht werden
        {
            operators.Clear(); // operatoren werden geleert
            numbers.Clear(); // zahlen werden geleert 
            textBox1.Clear(); // Obere Formel wird entfernt
            berechnet = false; // gleichzeichen wieder auf normal
        }
        #endregion
        private void endOfCalculation()
        {
            advancedOperators = false;
            clearInput = true; //Vormerkung, damit die nächste Zahl eingegeben werden kann
            berechnet = true; //damit das Gleichzeichen keine Faxen macht
        }

        private void buttonMal_Click(object sender, EventArgs e)
        {
            if (zahlenFeld.Text.EndsWith(","))
            {
                zahlenFeld.Text = zahlenFeld.Text.Substring(0, 
                    zahlenFeld.Text.Length - 1); // Falls die Eingabe mit
                                                 // einem Komma endet
            }
            if (berechnet) // Wenn das Gleichzeichen gedrueckt wurde, wird folgendes ausgefuert
            {
                GleichGedrueckt();
            }
            if (fullclear) // wenn es einen fehler gab, dann ist fullclear auf true
            {
                nullanzeige();
            }
            #region hochrechnen
            if (powerclicked)
            {
                try
                {
                    powernumbers.Add(Convert.ToDouble(zahlenFeld.Text.Substring
                        (zahlenFeld.Text.IndexOf('^') + 1)));
                }
                catch //wenn keine zweite Zahl eingegeben wurde 
                {
                    powernumbers.Add(2);
                }
                zahlenFeld.Text = Convert.ToString(Math.Pow(powernumbers[0], powernumbers[1]));
            }
            #endregion

            try
            {
                numbers.Add(Convert.ToDouble(zahlenFeld.Text/*.Replace(',', '.')*/)); // konvertiert
                                                                                      // die eingegebene
                                                                                      // Zahl zu double
                                                                                      // und fügt diese
                                                                                      // zu Liste hinzu
                operators.Add(((Button)sender).Text); // fügt die den Operanten in eine Liste Hinzu
                if (advancedOperators)
                {
                    textBox1.Text += ((Button)sender).Text;
                    
                }
                else if (powerclicked)
                {
                    textBox1.Text += Convert.ToString(powernumbers[0]) + "^" + 
                        Convert.ToString(powernumbers[1]) + ((Button)sender).Text;
                    powernumbers.Clear();
                    powerclicked = false;
                }
                else
                {
                    textBox1.Text += zahlenFeld.Text + ((Button)sender).Text; // fuegt die Zahlen
                                                                              // ins obere Feld ein                                                     
                }
            }
            catch
            {
                zahlenFeld.Text = "Fehler!";
                fullclear = true;
            }


            advancedOperators = false;
            clearInput = true; //Vormerkung, damit die nächste Zahl eingegeben werden kann
        }

        private void buttonGleich_Click(object sender, EventArgs e)
        {
            // Prüfung, ob gerechnet werden kann
            #region vorbereitungen für die Berechnung
            if (berechnet) // wenn schon einmal das gleichzeichen gedrückt wurde, kann es nicht
                           // nochmal eine
                           // Berechnung ausführen. somit Wird irgendeine Dummheit vermieden
            {
                return;
            }

            if (powerclicked)
            {
                try
                {
                    powernumbers.Add(Convert.ToDouble(zahlenFeld.Text.
                        Substring(zahlenFeld.Text.IndexOf('^') + 1)));
                }
                catch //wenn keine zweite Zahl eingegeben wurde 
                {
                    powernumbers.Add(2);
                }
                zahlenFeld.Text = Convert.ToString(Math.Pow(powernumbers[0], powernumbers[1]));
            }

            try // Versucht, die letzte eingegebene Zahl in die Liste hinzuzufügen
            {
                numbers.Add(Convert.ToDouble(zahlenFeld.Text/*.Replace(',', '.')*/));
                if (advancedOperators)
                {
                    textBox1.Text += "=";
                }
                else if (powerclicked)
                {
                    textBox1.Text += Convert.ToString(powernumbers[0]) + "^" + 
                        Convert.ToString(powernumbers[1]) + ((Button)sender).Text;
                    powernumbers.Clear();
                    powerclicked = false;
                }
                else
                {
                    textBox1.Text += zahlenFeld.Text + "=";
                }
            }
            catch // falls es nicht klappt, wird einfach nichts berechnet
            {
                return;
            }

            #endregion

            //Berechnung

            zahlenFeld.Text = Calculate2(numbers, operators); // Ruft die berechnung auf und
                                                             // gibt das Ergebnis in die Textbox aus

            // Das Behandeln von Fehlermeldungen und par Variabeln auf True setzen
            #region nachbereitungen
            try
            {
                Convert.ToDouble(zahlenFeld.Text); // Wenn die Rechenformel eine Fehlermeldung
                                                   // ausgibt, wird hiermit getestet
                                                   // ob eine Fehlermeldung ausgespuckt wurde
            }
            catch
            {
                fullclear = true;
            }


            endOfCalculation();
            #endregion
        }

        #region MATHE 
        // Berechne das zeug
        

        private string Calculate2(List<double> numbers, List<string> operators)
        {
            if (operators.Count == 0) // wenn es keine operatoren gibt, word die Erste Zahl
                                      // der Liste zurückgegeben
            {
                return Convert.ToString(numbers[0]); // Falls keine Operationen vorhanden sind,
                                                     // gib die aktuelle Zahl zurück
            }

            PerformMultiplicationsAndDivisions(numbers, operators);

            double result = numbers[0];

            for (int i = 0; i < operators.Count; i++)
            {
                double nextNumber = numbers[i + 1];
                string currentOperator = operators[i];

                switch (currentOperator)
                {
                    case "+":
                        result += nextNumber;
                        break;
                    case "-":
                        result -= nextNumber;
                        break;
                    default:
                        return "Ungültiger Operator: " + currentOperator;
                }
            }

            return result.ToString();
        }

        private void PerformMultiplicationsAndDivisions(List<double> numbers, List<string> operators)
        {
            for (int i = 0; i < operators.Count;)
            {
                if (operators[i] == "*" || operators[i] == "/")
                {
                    double operand1 = numbers[i];
                    double operand2 = numbers[i + 1];
                    string currentOperator = operators[i];

                    double intermediateResult = 0.0;
                    switch (currentOperator)
                    {
                        case "*":
                            intermediateResult = operand1 * operand2;
                            break;
                        case "/":
                            if (operand2 == 0)
                                textBox1.Text = "Teilen durch 0 ist nicht erlaubt";
                            intermediateResult = operand1 / operand2;
                            break;
                    }

                    // Ersetze operand1 und operand2 durch das Zwischenergebnis
                    numbers[i] = intermediateResult;
                    numbers.RemoveAt(i + 1);
                    operators.RemoveAt(i);
                }
                else
                {
                    // Wenn kein * oder / gefunden wurde, gehe zum nächsten Operator
                    i++;
                }
            }
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

        private void sinus_Click(object sender, EventArgs e)
        {
            /*try
            {
                double zahl;
                textBox1.Text += "sin" + "(" + zahlenFeld.Text + ")";
                zahl = Math.Sin(Convert.ToDouble(zahlenFeld.Text));
                zahlenFeld.Text = Convert.ToString(zahl);
                advancedOperators = true;
            }
            catch
            {
                return;
            }*/
        }

        private void cosinus_Click(object sender, EventArgs e)
        {
            /*try
            {
                double zahl;
                textBox1.Text += "cos" + "(" + zahlenFeld.Text + ")";
                zahl = Math.Cos(Convert.ToDouble(zahlenFeld.Text));
                zahlenFeld.Text = Convert.ToString(zahl);
                advancedOperators = true;
            }
            catch
            {
                return;
            }*/
        }

        private void tangens_Click(object sender, EventArgs e)
        {
            /*try
            {
                double zahl;
                textBox1.Text += "tan" + "(" + zahlenFeld.Text + ")";
                zahl = Math.Tan(Convert.ToDouble(zahlenFeld.Text));
                zahlenFeld.Text = Convert.ToString(zahl);
                advancedOperators = true;
            }
            catch
            {
                return;
            }*/
        }

        private void power2_Click(object sender, EventArgs e)
        {

        }
        #endregion


    }
}