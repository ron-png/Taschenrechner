namespace Taschenrechner
{
    partial class Rechner
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.zahl1 = new System.Windows.Forms.Button();
            this.zahl2 = new System.Windows.Forms.Button();
            this.zahl3 = new System.Windows.Forms.Button();
            this.zahl4 = new System.Windows.Forms.Button();
            this.zahl5 = new System.Windows.Forms.Button();
            this.zahl6 = new System.Windows.Forms.Button();
            this.zahl7 = new System.Windows.Forms.Button();
            this.zahl8 = new System.Windows.Forms.Button();
            this.zahl9 = new System.Windows.Forms.Button();
            this.zahl0 = new System.Windows.Forms.Button();
            this.buttonKomma = new System.Windows.Forms.Button();
            this.vorZeichen = new System.Windows.Forms.Button();
            this.buttonGleich = new System.Windows.Forms.Button();
            this.buttonPlus = new System.Windows.Forms.Button();
            this.buttonMinus = new System.Windows.Forms.Button();
            this.buttonMal = new System.Windows.Forms.Button();
            this.buttonGeteilt = new System.Windows.Forms.Button();
            this.zahlenFeld = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // zahl1
            // 
            this.zahl1.Location = new System.Drawing.Point(219, 270);
            this.zahl1.Name = "zahl1";
            this.zahl1.Size = new System.Drawing.Size(75, 23);
            this.zahl1.TabIndex = 0;
            this.zahl1.Text = "1";
            this.zahl1.UseVisualStyleBackColor = true;
            this.zahl1.Click += new System.EventHandler(this.zahl1_Click);
            // 
            // zahl2
            // 
            this.zahl2.Location = new System.Drawing.Point(301, 269);
            this.zahl2.Name = "zahl2";
            this.zahl2.Size = new System.Drawing.Size(75, 23);
            this.zahl2.TabIndex = 1;
            this.zahl2.Text = "2";
            this.zahl2.UseVisualStyleBackColor = true;
            this.zahl2.Click += new System.EventHandler(this.zahl1_Click);
            // 
            // zahl3
            // 
            this.zahl3.Location = new System.Drawing.Point(383, 269);
            this.zahl3.Name = "zahl3";
            this.zahl3.Size = new System.Drawing.Size(75, 23);
            this.zahl3.TabIndex = 2;
            this.zahl3.Text = "3";
            this.zahl3.UseVisualStyleBackColor = true;
            this.zahl3.Click += new System.EventHandler(this.zahl1_Click);
            // 
            // zahl4
            // 
            this.zahl4.Location = new System.Drawing.Point(219, 300);
            this.zahl4.Name = "zahl4";
            this.zahl4.Size = new System.Drawing.Size(75, 23);
            this.zahl4.TabIndex = 3;
            this.zahl4.Text = "4";
            this.zahl4.UseVisualStyleBackColor = true;
            this.zahl4.Click += new System.EventHandler(this.zahl1_Click);
            // 
            // zahl5
            // 
            this.zahl5.Location = new System.Drawing.Point(301, 299);
            this.zahl5.Name = "zahl5";
            this.zahl5.Size = new System.Drawing.Size(75, 23);
            this.zahl5.TabIndex = 4;
            this.zahl5.Text = "5";
            this.zahl5.UseVisualStyleBackColor = true;
            this.zahl5.Click += new System.EventHandler(this.zahl1_Click);
            // 
            // zahl6
            // 
            this.zahl6.Location = new System.Drawing.Point(383, 299);
            this.zahl6.Name = "zahl6";
            this.zahl6.Size = new System.Drawing.Size(75, 23);
            this.zahl6.TabIndex = 5;
            this.zahl6.Text = "6";
            this.zahl6.UseVisualStyleBackColor = true;
            this.zahl6.Click += new System.EventHandler(this.zahl1_Click);
            // 
            // zahl7
            // 
            this.zahl7.Location = new System.Drawing.Point(219, 330);
            this.zahl7.Name = "zahl7";
            this.zahl7.Size = new System.Drawing.Size(75, 23);
            this.zahl7.TabIndex = 6;
            this.zahl7.Text = "7";
            this.zahl7.UseVisualStyleBackColor = true;
            this.zahl7.Click += new System.EventHandler(this.zahl1_Click);
            // 
            // zahl8
            // 
            this.zahl8.Location = new System.Drawing.Point(301, 329);
            this.zahl8.Name = "zahl8";
            this.zahl8.Size = new System.Drawing.Size(75, 23);
            this.zahl8.TabIndex = 7;
            this.zahl8.Text = "8";
            this.zahl8.UseVisualStyleBackColor = true;
            this.zahl8.Click += new System.EventHandler(this.zahl1_Click);
            // 
            // zahl9
            // 
            this.zahl9.Location = new System.Drawing.Point(383, 330);
            this.zahl9.Name = "zahl9";
            this.zahl9.Size = new System.Drawing.Size(75, 23);
            this.zahl9.TabIndex = 8;
            this.zahl9.Text = "9";
            this.zahl9.UseVisualStyleBackColor = true;
            this.zahl9.Click += new System.EventHandler(this.zahl1_Click);
            // 
            // zahl0
            // 
            this.zahl0.Location = new System.Drawing.Point(301, 359);
            this.zahl0.Name = "zahl0";
            this.zahl0.Size = new System.Drawing.Size(75, 23);
            this.zahl0.TabIndex = 9;
            this.zahl0.Text = "0";
            this.zahl0.UseVisualStyleBackColor = true;
            this.zahl0.Click += new System.EventHandler(this.zahl1_Click);
            // 
            // buttonKomma
            // 
            this.buttonKomma.Location = new System.Drawing.Point(382, 359);
            this.buttonKomma.Name = "buttonKomma";
            this.buttonKomma.Size = new System.Drawing.Size(75, 23);
            this.buttonKomma.TabIndex = 10;
            this.buttonKomma.Text = ",";
            this.buttonKomma.UseVisualStyleBackColor = true;
            // 
            // vorZeichen
            // 
            this.vorZeichen.Location = new System.Drawing.Point(220, 359);
            this.vorZeichen.Name = "vorZeichen";
            this.vorZeichen.Size = new System.Drawing.Size(75, 23);
            this.vorZeichen.TabIndex = 11;
            this.vorZeichen.Text = "+/-";
            this.vorZeichen.UseVisualStyleBackColor = true;
            // 
            // buttonGleich
            // 
            this.buttonGleich.Location = new System.Drawing.Point(464, 359);
            this.buttonGleich.Name = "buttonGleich";
            this.buttonGleich.Size = new System.Drawing.Size(75, 23);
            this.buttonGleich.TabIndex = 12;
            this.buttonGleich.Text = "=";
            this.buttonGleich.UseVisualStyleBackColor = true;
            // 
            // buttonPlus
            // 
            this.buttonPlus.Location = new System.Drawing.Point(464, 330);
            this.buttonPlus.Name = "buttonPlus";
            this.buttonPlus.Size = new System.Drawing.Size(75, 23);
            this.buttonPlus.TabIndex = 13;
            this.buttonPlus.Text = "+";
            this.buttonPlus.UseVisualStyleBackColor = true;
            this.buttonPlus.Click += new System.EventHandler(this.buttonMal_Click);
            // 
            // buttonMinus
            // 
            this.buttonMinus.Location = new System.Drawing.Point(464, 300);
            this.buttonMinus.Name = "buttonMinus";
            this.buttonMinus.Size = new System.Drawing.Size(75, 23);
            this.buttonMinus.TabIndex = 14;
            this.buttonMinus.Text = "-";
            this.buttonMinus.UseVisualStyleBackColor = true;
            this.buttonMinus.Click += new System.EventHandler(this.buttonMal_Click);
            // 
            // buttonMal
            // 
            this.buttonMal.Location = new System.Drawing.Point(464, 269);
            this.buttonMal.Name = "buttonMal";
            this.buttonMal.Size = new System.Drawing.Size(75, 23);
            this.buttonMal.TabIndex = 15;
            this.buttonMal.Text = "*";
            this.buttonMal.UseVisualStyleBackColor = true;
            this.buttonMal.Click += new System.EventHandler(this.buttonMal_Click);
            // 
            // buttonGeteilt
            // 
            this.buttonGeteilt.Location = new System.Drawing.Point(464, 240);
            this.buttonGeteilt.Name = "buttonGeteilt";
            this.buttonGeteilt.Size = new System.Drawing.Size(75, 23);
            this.buttonGeteilt.TabIndex = 16;
            this.buttonGeteilt.Text = "/";
            this.buttonGeteilt.UseVisualStyleBackColor = true;
            this.buttonGeteilt.Click += new System.EventHandler(this.buttonMal_Click);
            // 
            // zahlenFeld
            // 
            this.zahlenFeld.Location = new System.Drawing.Point(219, 48);
            this.zahlenFeld.Name = "zahlenFeld";
            this.zahlenFeld.Size = new System.Drawing.Size(320, 20);
            this.zahlenFeld.TabIndex = 21;
            // 
            // Rechner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.zahlenFeld);
            this.Controls.Add(this.buttonGeteilt);
            this.Controls.Add(this.buttonMal);
            this.Controls.Add(this.buttonMinus);
            this.Controls.Add(this.buttonPlus);
            this.Controls.Add(this.buttonGleich);
            this.Controls.Add(this.vorZeichen);
            this.Controls.Add(this.buttonKomma);
            this.Controls.Add(this.zahl0);
            this.Controls.Add(this.zahl9);
            this.Controls.Add(this.zahl8);
            this.Controls.Add(this.zahl7);
            this.Controls.Add(this.zahl6);
            this.Controls.Add(this.zahl5);
            this.Controls.Add(this.zahl4);
            this.Controls.Add(this.zahl3);
            this.Controls.Add(this.zahl2);
            this.Controls.Add(this.zahl1);
            this.Name = "Rechner";
            this.Text = "Rechner";
            this.Load += new System.EventHandler(this.Rechner_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button zahl1;
        private System.Windows.Forms.Button zahl2;
        private System.Windows.Forms.Button zahl3;
        private System.Windows.Forms.Button zahl4;
        private System.Windows.Forms.Button zahl5;
        private System.Windows.Forms.Button zahl6;
        private System.Windows.Forms.Button zahl7;
        private System.Windows.Forms.Button zahl8;
        private System.Windows.Forms.Button zahl9;
        private System.Windows.Forms.Button zahl0;
        private System.Windows.Forms.Button buttonKomma;
        private System.Windows.Forms.Button vorZeichen;
        private System.Windows.Forms.Button buttonGleich;
        private System.Windows.Forms.Button buttonPlus;
        private System.Windows.Forms.Button buttonMinus;
        private System.Windows.Forms.Button buttonMal;
        private System.Windows.Forms.Button buttonGeteilt;
        private System.Windows.Forms.TextBox zahlenFeld;
    }
}

