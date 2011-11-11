using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace APO {
    public partial class Mask3x3Form : Form {
        private int[,] mask;
        int divisor;

        public int[,] Mask {
            get { return mask; }
        }

        public int Divisor {
            get { return divisor; }
        }

        public Mask3x3Form() {
            mask = new int[3, 3];
            InitializeComponent();
            Recalculate();
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            try {
                Recalculate();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Recalculate() {
            mask[0, 0] = ParseOrZero(mask1Box.Text);
            mask[0, 1] = ParseOrZero(mask2Box.Text);
            mask[0, 2] = ParseOrZero(mask3Box.Text);
            mask[1, 0] = ParseOrZero(mask4Box.Text);
            mask[1, 1] = ParseOrZero(mask5Box.Text);
            mask[1, 2] = ParseOrZero(mask6Box.Text);
            mask[2, 0] = ParseOrZero(mask7Box.Text);
            mask[2, 1] = ParseOrZero(mask8Box.Text);
            mask[2, 2] = ParseOrZero(mask9Box.Text);

            if (!checkBox1.Checked)
                divisor = Math.Max(ParseOrZero(divisorBox.Text), 1);
            else {
                divisor = 0;
                foreach (int value in mask) {
                    divisor += value;
                }
                divisor = Math.Max(divisor, 1);
                divisorBox.Text = divisor.ToString();
            }
        }

        private int ParseOrZero(string s) {
            try {
                return Int32.Parse(s);
            }
            catch {
                return 0;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (checkBox1.Checked == false)
                divisorBox.Enabled = true;
            else
                divisorBox.Enabled = false;
            Recalculate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            Recalculate();
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {
            Recalculate();
        }

        private void textBox3_TextChanged(object sender, EventArgs e) {
            Recalculate();
        }

        private void textBox4_TextChanged(object sender, EventArgs e) {
            Recalculate();
        }

        private void textBox5_TextChanged(object sender, EventArgs e) {
            Recalculate();
        }

        private void textBox6_TextChanged(object sender, EventArgs e) {
            Recalculate();
        }

        private void textBox7_TextChanged(object sender, EventArgs e) {
            Recalculate();
        }

        private void textBox8_TextChanged(object sender, EventArgs e) {
            Recalculate();
        }

        private void textBox9_TextChanged(object sender, EventArgs e) {
            Recalculate();
        }

        private void textBox10_TextChanged(object sender, EventArgs e) {
            Recalculate();
        }
    }
}
