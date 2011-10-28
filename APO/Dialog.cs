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
        public int combovalue;

        private class Item
        {
            public string Name;
            public int Value;
            public Item(string name, int value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        };

        public myCustomDialog(String title, String label)
        {
            InitializeComponent();
            Text = title;
            labelDesc.Text = label;
        }

        public myCustomDialog(String title, String label, Form[] forms)
        {
            InitializeComponent();
            Text = title;
            labelDesc.Text = label;
            textBox.Visible = false;
            comboBox1.Visible = true;
            int i = 0;
            foreach(Form form in forms)
            {
                comboBox1.Items.Add(new Item(form.Text, i++));
            }
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
            try
            {
                combovalue = ((Item)comboBox1.SelectedItem).Value;
            }catch(Exception errorek){}

            myAcceptButton.DialogResult = DialogResult.OK;
        }
    }
}
