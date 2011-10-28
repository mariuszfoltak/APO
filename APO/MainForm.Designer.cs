namespace APO
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otwórzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszJakoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplikujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zamknijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.obrazToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konwertujDoSzarości8BitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spłaszczanieHistogramuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metodaPierwszaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metodaDrugaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metodaTrzeciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operacjaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negacjaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progowanieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redukcjaPoziomówSzarościToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rozciaganieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.jasnoscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kontrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 459);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(764, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.obrazToolStripMenuItem,
            this.spłaszczanieHistogramuToolStripMenuItem,
            this.operacjaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(764, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.otwórzToolStripMenuItem,
            this.zapiszToolStripMenuItem,
            this.zapiszJakoToolStripMenuItem,
            this.duplikujToolStripMenuItem,
            this.toolStripSeparator1,
            this.zamknijToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // otwórzToolStripMenuItem
            // 
            this.otwórzToolStripMenuItem.Name = "otwórzToolStripMenuItem";
            this.otwórzToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.otwórzToolStripMenuItem.Text = "Otwórz...";
            this.otwórzToolStripMenuItem.Click += new System.EventHandler(this.otwórzToolStripMenuItem_Click);
            // 
            // zapiszToolStripMenuItem
            // 
            this.zapiszToolStripMenuItem.Name = "zapiszToolStripMenuItem";
            this.zapiszToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.zapiszToolStripMenuItem.Text = "Zapisz";
            // 
            // zapiszJakoToolStripMenuItem
            // 
            this.zapiszJakoToolStripMenuItem.Name = "zapiszJakoToolStripMenuItem";
            this.zapiszJakoToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.zapiszJakoToolStripMenuItem.Text = "Zapisz jako...";
            // 
            // duplikujToolStripMenuItem
            // 
            this.duplikujToolStripMenuItem.Name = "duplikujToolStripMenuItem";
            this.duplikujToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.duplikujToolStripMenuItem.Text = "Duplikuj";
            this.duplikujToolStripMenuItem.Click += new System.EventHandler(this.duplikujToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // zamknijToolStripMenuItem
            // 
            this.zamknijToolStripMenuItem.Name = "zamknijToolStripMenuItem";
            this.zamknijToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.zamknijToolStripMenuItem.Text = "Zamknij";
            // 
            // obrazToolStripMenuItem
            // 
            this.obrazToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konwertujDoSzarości8BitToolStripMenuItem});
            this.obrazToolStripMenuItem.Name = "obrazToolStripMenuItem";
            this.obrazToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.obrazToolStripMenuItem.Text = "Obraz";
            // 
            // konwertujDoSzarości8BitToolStripMenuItem
            // 
            this.konwertujDoSzarości8BitToolStripMenuItem.Name = "konwertujDoSzarości8BitToolStripMenuItem";
            this.konwertujDoSzarości8BitToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.konwertujDoSzarości8BitToolStripMenuItem.Text = "Konwertuj do szarości 8 bit";
            this.konwertujDoSzarości8BitToolStripMenuItem.Click += new System.EventHandler(this.konwertujDoSzarości8BitToolStripMenuItem_Click);
            // 
            // spłaszczanieHistogramuToolStripMenuItem
            // 
            this.spłaszczanieHistogramuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.metodaPierwszaToolStripMenuItem,
            this.metodaDrugaToolStripMenuItem,
            this.metodaTrzeciaToolStripMenuItem});
            this.spłaszczanieHistogramuToolStripMenuItem.Name = "spłaszczanieHistogramuToolStripMenuItem";
            this.spłaszczanieHistogramuToolStripMenuItem.Size = new System.Drawing.Size(148, 20);
            this.spłaszczanieHistogramuToolStripMenuItem.Text = "Spłaszczanie histogramu";
            // 
            // metodaPierwszaToolStripMenuItem
            // 
            this.metodaPierwszaToolStripMenuItem.Name = "metodaPierwszaToolStripMenuItem";
            this.metodaPierwszaToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.metodaPierwszaToolStripMenuItem.Text = "Metoda pierwsza";
            this.metodaPierwszaToolStripMenuItem.Click += new System.EventHandler(this.metodaPierwszaToolStripMenuItem_Click);
            // 
            // metodaDrugaToolStripMenuItem
            // 
            this.metodaDrugaToolStripMenuItem.Name = "metodaDrugaToolStripMenuItem";
            this.metodaDrugaToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.metodaDrugaToolStripMenuItem.Text = "Metoda druga";
            this.metodaDrugaToolStripMenuItem.Click += new System.EventHandler(this.metodaDrugaToolStripMenuItem_Click);
            // 
            // metodaTrzeciaToolStripMenuItem
            // 
            this.metodaTrzeciaToolStripMenuItem.Name = "metodaTrzeciaToolStripMenuItem";
            this.metodaTrzeciaToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.metodaTrzeciaToolStripMenuItem.Text = "Metoda trzecia";
            this.metodaTrzeciaToolStripMenuItem.Click += new System.EventHandler(this.metodaTrzeciaToolStripMenuItem_Click);
            // 
            // operacjaToolStripMenuItem
            // 
            this.operacjaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.negacjaToolStripMenuItem,
            this.progowanieToolStripMenuItem,
            this.redukcjaPoziomówSzarościToolStripMenuItem,
            this.rozciaganieToolStripMenuItem,
            this.jasnoscToolStripMenuItem,
            this.kontrastToolStripMenuItem});
            this.operacjaToolStripMenuItem.Name = "operacjaToolStripMenuItem";
            this.operacjaToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.operacjaToolStripMenuItem.Text = "Operacja";
            // 
            // negacjaToolStripMenuItem
            // 
            this.negacjaToolStripMenuItem.Name = "negacjaToolStripMenuItem";
            this.negacjaToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.negacjaToolStripMenuItem.Text = "Negacja";
            this.negacjaToolStripMenuItem.Click += new System.EventHandler(this.negacjaToolStripMenuItem_Click);
            // 
            // progowanieToolStripMenuItem
            // 
            this.progowanieToolStripMenuItem.Name = "progowanieToolStripMenuItem";
            this.progowanieToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.progowanieToolStripMenuItem.Text = "Progowanie";
            this.progowanieToolStripMenuItem.Click += new System.EventHandler(this.progowanieToolStripMenuItem_Click);
            // 
            // redukcjaPoziomówSzarościToolStripMenuItem
            // 
            this.redukcjaPoziomówSzarościToolStripMenuItem.Name = "redukcjaPoziomówSzarościToolStripMenuItem";
            this.redukcjaPoziomówSzarościToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.redukcjaPoziomówSzarościToolStripMenuItem.Text = "Redukcja poziomów szarości";
            this.redukcjaPoziomówSzarościToolStripMenuItem.Click += new System.EventHandler(this.redukcjaPoziomówSzarościToolStripMenuItem_Click);
            // 
            // rozciaganieToolStripMenuItem
            // 
            this.rozciaganieToolStripMenuItem.Name = "rozciaganieToolStripMenuItem";
            this.rozciaganieToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.rozciaganieToolStripMenuItem.Text = "Rozciaganie";
            this.rozciaganieToolStripMenuItem.Click += new System.EventHandler(this.rozciaganieToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // jasnoscToolStripMenuItem
            // 
            this.jasnoscToolStripMenuItem.Name = "jasnoscToolStripMenuItem";
            this.jasnoscToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.jasnoscToolStripMenuItem.Text = "Jasność";
            this.jasnoscToolStripMenuItem.Click += new System.EventHandler(this.kontrastToolStripMenuItem_Click);
            // 
            // kontrastToolStripMenuItem
            // 
            this.kontrastToolStripMenuItem.Name = "kontrastToolStripMenuItem";
            this.kontrastToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.kontrastToolStripMenuItem.Text = "Kontrast";
            this.kontrastToolStripMenuItem.Click += new System.EventHandler(this.kontrastToolStripMenuItem_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 481);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Algorytmy przetwarzania obrazów";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otwórzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem zamknijToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszJakoToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem obrazToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konwertujDoSzarości8BitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplikujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spłaszczanieHistogramuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metodaPierwszaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metodaDrugaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metodaTrzeciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operacjaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negacjaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem progowanieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redukcjaPoziomówSzarościToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rozciaganieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jasnoscToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kontrastToolStripMenuItem;
    }
}

