using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace APO
{
    public partial class myCustomDialog : Form
    {
        public String value;
        public String value2;

        public myCustomDialog(String title, String label)
        {
            InitializeComponent();
            Text = title;
            labelDesc.Text = label;
        }

        public myCustomDialog(String title, String label, String label2)
        {
            InitializeComponent();
            Text = title;
            labelDesc.Text = label;
            labelDesc2.Text = label2;
            labelDesc2.Visible = true;
            textBox2.Visible = true;
        }

        private void myAcceptButton_Click(object sender, EventArgs e)
        {
            value = textBox.Text;
            value2 = textBox2.Text;

            myAcceptButton.DialogResult = DialogResult.OK;
        }
    }
}
