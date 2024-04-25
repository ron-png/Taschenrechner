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

        private void buttonGleich_Click(object sender, EventArgs e)
        {
           
            string formel = zahlenFeld.Text; // Speichert das String Feld in variable
                                             //double berechnet = Convert.ToDouble(new DataTable().Compute(formel, null)); //Konvertiert dieses zu double und berechnet mit .Compute
                                             // berechnet = Math.Round(berechnet, 10);
                                             //zahlenFeld.Text = Convert.ToString(berechnet); //

            double ergebnis = EvaluateFormula(formel);    
            zahlenFeld.Text = Convert.ToString(ergebnis);


            static double EvaluateFormula(string formula)
            {
                // Tokenisierung der Formel
                List<string> tokens = TokenizeFormula(formula);

                // Umwandlung der Infix-Notation in Postfix-Notation (umgekehrte polnische Notation)
                List<string> postfix = ConvertToPostfix(tokens);

                // Auswertung der Postfix-Notation
                Stack<double> stack = new Stack<double>();
                foreach (string token in postfix)
                {
                    if (IsNumeric(token))
                    {
                        stack.Push(double.Parse(token));
                    }
                    else if (IsOperator(token))
                    {
                        double b = stack.Pop();
                        double a = stack.Pop();
                        double result = ApplyOperator(a, b, token);
                        stack.Push(result);
                    }
                }

                if (stack.Count != 1)
                {
                    throw new InvalidOperationException("Ungültige Formel");
                }

                return stack.Pop();
            }

            static List<string> TokenizeFormula(string formula)
            {
                // Regulärer Ausdruck zum Tokenisieren der Formel
                string pattern = @"(\d+(\.\d+)?|\+|\-|\*|\/|\%|\(|\)|\^|\√)";
                MatchCollection matches = Regex.Matches(formula, pattern);

                List<string> tokens = new List<string>();
                foreach (Match match in matches)
                {
                    tokens.Add(match.Value);
                }

                return tokens;
            }

            static List<string> ConvertToPostfix(List<string> infix)
            {
                List<string> postfix = new List<string>();
                Stack<string> stack = new Stack<string>();

                foreach (string token in infix)
                {
                    if (IsNumeric(token))
                    {
                        postfix.Add(token);
                    }
                    else if (IsOperator(token))
                    {
                        while (stack.Count > 0 && Precedence(stack.Peek()) >= Precedence(token))
                        {
                            postfix.Add(stack.Pop());
                        }
                        stack.Push(token);
                    }
                    else if (token == "(")
                    {
                        stack.Push(token);
                    }
                    else if (token == ")")
                    {
                        while (stack.Count > 0 && stack.Peek() != "(")
                        {
                            postfix.Add(stack.Pop());
                        }
                        stack.Pop(); // Discard the '('
                    }
                }

                while (stack.Count > 0)
                {
                    postfix.Add(stack.Pop());
                }

                return postfix;
            }

            static bool IsNumeric(string token)
            {
                return double.TryParse(token, out _);
            }

            static bool IsOperator(string token)
            {
                return token == "+" || token == "-" || token == "*" || token == "/" || token == "%" || token == "^" || token == "√";
            }

            static int Precedence(string op)
            {
                switch (op)
                {
                    case "+":
                    case "-":
                        return 1;
                    case "*":
                    case "/":
                    case "%":
                        return 2;
                    case "^":
                    case "√":
                        return 3;
                    default:
                        return 0;
                }
            }

            static double ApplyOperator(double a, double b, string op)
            {
                switch (op)
                {
                    case "+":
                        return a + b;
                    case "-":
                        return a - b;
                    case "*":
                        return a * b;
                    case "/":
                        if (b == 0)
                        {
                            throw new DivideByZeroException();
                        }
                        return a / b;
                    case "%":
                        return a % b;
                    case "^":
                        return Math.Pow(a, b);
                    case "√":
                        return Math.Sqrt(b);
                    default:
                        throw new ArgumentException("Ungültiger Operator");
                }
            }

        }
    }
}