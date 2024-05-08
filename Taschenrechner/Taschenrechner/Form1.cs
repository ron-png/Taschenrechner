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
        bool fullclear; // wird bei String Fehlermeldung gebraucht, damit der String nicht als "Zahl" übernommen wird

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
                clearInput = false; // muss wieder auf falsé gestellt werden, weil die Zahlen sonst immer rausgeschmissen werden
            }
            // Zahlenfeld Eingabe 
            zahlenFeld.Text += ((Button)sender).Text;

            fullclear = false; //fullclear wird nicht gebraucht, weil die Zahl das Feld mit der Fehlermeldung ersetzt
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
            fullclear = false; //fullclear wird nicht gebraucht, weil die Zahl das Feld mit der Fehlermeldung ersetzt

        }


        bool denyKomma; // um zu gucken, ob schon ein Komma eingegeben wurde. Habe vorher "counter"´genutzt, jedoch jetzt bool
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

            zahlenFeld.Text = Convert.ToString(zahl); // die Zahl wird hier wieder ins Zahlenfeld ausgespuckt
        }


        // operanten
        #region advancedFeatures
        bool advancedOperators;


        private void wurzel_Click(object sender, EventArgs e)
        {
            if (berechnet)
            {
                textBox1.Clear();
                GleichGedrueckt();
            }
            string zahl = zahlenFeld.Text;
            try
            {
                if (((Button)sender).Text == "√")
                {
                    zahlenFeld.Text = Convert.ToString(Math.Sqrt(Convert.ToDouble(zahlenFeld.Text)));
                }
                else if (((Button)sender).Text == "sin")
                {
                    zahlenFeld.Text = Convert.ToString(Math.Sin(Convert.ToDouble(zahlenFeld.Text)));
                }
                else if (((Button)sender).Text == "cos")
                {
                    zahlenFeld.Text = Convert.ToString(Math.Cos(Convert.ToDouble(zahlenFeld.Text)));
                }
                else if (((Button)sender).Text == "tan")
                {
                    zahlenFeld.Text = Convert.ToString(Math.Tan(Convert.ToDouble(zahlenFeld.Text)));
                }
                textBox1.Text += ((Button)sender).Text + "("+ zahl + ")";
                advancedOperators = true;
            }
            catch
            {
                return;
            }
        }
        private void GleichGedrueckt() //Wenn Gleich gedrueckt wird, muss die alte Formel und Zahlen geloescht werden
        {
            operators.Clear(); // operatoren werden geleert
            numbers.Clear(); // zahlen werden geleert 
            textBox1.Clear(); // Obere Formel wird entfernt
            berechnet = false; // gleichzeichen wieder auf normal
        }
        #endregion
        private void buttonMal_Click(object sender, EventArgs e)
        {
            if (zahlenFeld.Text.EndsWith(","))
            {
                zahlenFeld.Text = zahlenFeld.Text.Substring(0, zahlenFeld.Text.Length - 1); // Falls die Eingabe mit einem Komma endet
            }
            if (berechnet) // Wenn das Gleichzeichen gedrueckt wurde, wird folgendes ausgefuert
            {
                GleichGedrueckt();
            }
            if (fullclear) // wenn es einen fehler gab, dann ist fullclear auf true
            {
                nullanzeige();
            }

            try
            {
                numbers.Add(Convert.ToDouble(zahlenFeld.Text/*.Replace(',', '.')*/)); // konvertiert die eingegebene Zahl zu double und fügt diese zu Liste hinzu
                operators.Add(((Button)sender).Text); // fügt die den Operanten in eine Liste Hinzu
                if (advancedOperators)
                {
                    textBox1.Text += ((Button)sender).Text;
                    advancedOperators = false;
                }
                else
                {
                    textBox1.Text += zahlenFeld.Text + ((Button)sender).Text; // fuegt die Zahlen ins obere Feld ein       
                }
            }
            catch
            {
                zahlenFeld.Text = "Fehler!";
                fullclear = true;
            }

            clearInput = true; // markiert, um das Feld für neuen Input "frei" zu machen // Vormerkung, damit die nächste Zahl eingegeben werden kann
            denyKomma = false; // Komma kann jetzt auch wieder eingegeben werden
        }

        private void buttonGleich_Click(object sender, EventArgs e)
        {
            // Prüfung, ob gerechnet werden kann
            #region vorbereitungen für die Berechnung
            if (berechnet) // wenn schon einmal das gleichzeichen gedrückt wurde, kann es nicht nochmal eine
                           // Berechnung ausführen. somit Wird irgendeine Dummheit vermieden
            {
                return;
            }
            

            try // Versucht, die letzte eingegebene Zahl in die Liste hinzuzufügen
            {
                numbers.Add(Convert.ToDouble(zahlenFeld.Text/*.Replace(',', '.')*/));
            }
            catch // falls es nicht klappt, wird einfach nichts berechnet
            {
                return;
            }

            #endregion

            //Berechnung

            zahlenFeld.Text = Calculate(numbers, operators); // Ruft die berechnung auf und gibt das Ergebnis in die Textbox aus

            // Das Behandeln von Fehlermeldungen und par Variabeln auf True setzen
            #region nachbereitungen
            try
            {
                Convert.ToDouble(zahlenFeld.Text); // Wenn die Rechenformel eine Fehlermeldung ausgibt, wird hiermit getestet
                                                   // ob eine Fehlermeldung ausgespuckt wurde
            }
            catch
            {
                fullclear = true;
            }

            clearInput = true; //Vormerkung, damit die nächste Zahl eingegeben werden kann
            berechnet = true; //damit das Gleichzeichen keine Faxen macht
            #endregion
        }

        #region MATHE 
        // Berechne das zeug
        private string Calculate(List<double> numbers, List<string> operators)
        {
            
            if (operators.Count == 0) // wenn es keine operatoren gibt, word die Erste Zahl der Liste zurückgegeben
            {
                return Convert.ToString(numbers[0]); // Falls keine Operationen vorhanden sind, gib die aktuelle Zahl zurück
            }

            double result = numbers[0]; // Setze das Ergebnis auf die erste Zahl der Liste und "Arbeite" ausgehend von der Zahl.
                                        // Für Punkt vor Strichrechnung muss man sich noch was besseres Überlegen


            int numberscounter = 1; // Numberscounter ist auf 1, was in der folgenden Funktion dann die Zweite Zahl in der Liste anspricht.
                                    // Die erste Zahl wird schon in result genutzt
            for (int i = 0; i < operators.Count; i++)  // geht die beiden Listen durch der Operators count ist als exitfaktor für i genutzt
            {
                double nextNumber = numbers[numberscounter]; // nextnumber ist also die nächste nummer in der Liste
                string currentOperator = operators[i]; // der aktuelle Operator in der Liste
                numberscounter++;

                                        
                switch (currentOperator) // Switch case für die operanten undso
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
                            return "Teilen durch Null ist nicht möglich"; // duh, Schmeißt die Fehlermeldung raus, daher ist die Ausgabe der
                                                                          // Mathefunktion auch ein string und kein double
                        result = result / nextNumber;
                        break;
                    default:
                        return "Ungültiger Operator: " + currentOperator; // falls unbekannter operator kek
                }
            }

            return Convert.ToString(result); //Ausgabe, wenn die Berechnung erfolgreich war!
        }
        private string higherprio()
        {
            return "1";
        }

        private string highestprio()
        {
            return "1";
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


        #endregion


    }
}