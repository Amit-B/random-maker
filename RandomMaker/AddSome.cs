using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RandomMaker
{
    public partial class AddSome : Form
    {
        public AddSome()
        {
            InitializeComponent();
        }
        internal string toAdd = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            toAdd = "";
            for(int i = 0; i < textBox1.Lines.Length; i++)
                toAdd += textBox1.Lines[i] + (i == textBox1.Lines.Length - 1 ? "" : "|");
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            toAdd = "";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            toAdd = "";
            this.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains("|"))
            {
                MessageBox.Show("Do not using |", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = textBox1.Text.Replace("|", "");
            }
        }
    }
}