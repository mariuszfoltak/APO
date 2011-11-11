using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using APO.Operacje;

namespace APO
{
    public partial class MainForm : Form
    {
        private Int32 formCounter = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void useFilter(IFilter filter)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            filter.setImage(activeChild.bitmap);

            filter.Convert();

            activeChild.refresh();
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            Picture picture = new Picture();
            picture.MdiParent = this;
            picture.Text = new StringBuilder("Obraz ").Append(++formCounter).ToString();
            picture.loadImage(openFileDialog1.FileName);
            picture.Show();
        }

        private void konwertujDoSzarości8BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            useFilter(new GrayScale());
        }

        private void duplikujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild != null)
            {
                Picture newChild = new Picture(activeChild);
                newChild.Text = new StringBuilder("Obraz ").Append(++formCounter).ToString();
                newChild.MdiParent = this;
                newChild.Show();
            }
        }

        private void metodaPierwszaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild != null)
            {
                activeChild.Metoda1();
            }
        }

        private void metodaDrugaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild != null)
            {
                activeChild.Metoda2();
            }
        }

        private void metodaTrzeciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild != null)
            {
                activeChild.Metoda3();
            }
        }

        private void negacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild != null)
            {
                activeChild.negacja();
            }
        }

        private void progowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            myCustomDialog dialog = new myCustomDialog("Progowanie", "Podaj wartość do progowania");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.progowanie(Convert.ToInt32(dialog.value));
        }

        private void redukcjaPoziomówSzarościToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            myCustomDialog dialog = new myCustomDialog("Redukcja poziomów szarości", "Podaj ilość poziomów");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.redukcjaPoziomowSzarosci(Convert.ToInt32(dialog.value));
        }

        private void rozciaganieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            myCustomDialog dialog = new myCustomDialog("Rozciąganie", 
                "Podaj początkowy poziom", "Podaj końcowy poziom");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.rozciaganie(Convert.ToInt32(dialog.value), Convert.ToInt32(dialog.value2));
        }

        private void kontrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            myCustomDialog dialog = new myCustomDialog("Jasność", "Podaj o ile procent zwiększyć jasność");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.jasnosc(Convert.ToInt32(dialog.value));
        }

        private void kontrastToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            myCustomDialog dialog = new myCustomDialog("Kontrast", "Podaj o ile procent zwiększyć kontrast");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.kontrast(Convert.ToInt32(dialog.value));
        }

        private void gammaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            myCustomDialog dialog = new myCustomDialog("Gamma", "Podaj o ile procent zwiększyć gamme");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.gamma(Convert.ToDouble(dialog.value));
        }

        private void dodawanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            myCustomDialog dialog = new myCustomDialog("Dodawanie", 
                "Wybierz obraz który chcesz dodać", this.MdiChildren);

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.add(((Picture)this.MdiChildren[dialog.combovalue]).bitmap);
        }

        private void odejmowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            myCustomDialog dialog = new myCustomDialog("Odejmowanie",
                "Wybierz obraz który chcesz odjąć", this.MdiChildren);

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.sub(((Picture)this.MdiChildren[dialog.combovalue]).bitmap);
        }

        private void aNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            myCustomDialog dialog = new myCustomDialog("Operacja logiczna AND",
                "Wybierz obraz", this.MdiChildren);

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.and(((Picture)this.MdiChildren[dialog.combovalue]).bitmap);
        }

        private void oRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            myCustomDialog dialog = new myCustomDialog("Operacja logiczna OR",
                "Wybierz obraz", this.MdiChildren);

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.or(((Picture)this.MdiChildren[dialog.combovalue]).bitmap);
        }

        private void xORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture activeChild = (Picture)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            myCustomDialog dialog = new myCustomDialog("Operacja logiczna XOR",
                "Wybierz obraz", this.MdiChildren);

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.xor(((Picture)this.MdiChildren[dialog.combovalue]).bitmap);
        }
    }
}
