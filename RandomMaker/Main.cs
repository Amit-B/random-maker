using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace RandomMaker
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        int searchpos = -1;
        private void Error(string msg)
        {
            System.Media.SystemSounds.Exclamation.Play();
            toolStripStatusLabel1.Text = "Error: " + msg;
            toolStripDropDownButton1.Visible = true;
        }
        private void Info(string msg)
        {
            System.Media.SystemSounds.Asterisk.Play();
            toolStripStatusLabel1.Text = msg;
            toolStripDropDownButton1.Visible = false;
        }
        private void UpdateCount()
        {
            label1.Text = "Count: " + listBox1.Items.Count;
        }
        private void clearErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Exclamation.Play();
            toolStripStatusLabel1.Text = "";
            toolStripDropDownButton1.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0) Error("The value is empty.");
            else
            {
                bool shallask = false;
                for (int i = 0; i < listBox1.Items.Count && !shallask; i++) if (listBox1.Items[i].ToString() == textBox1.Text) shallask = true;
                if (shallask) if (MessageBox.Show("There is already item with this name, are you sure you want to continue?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) shallask = false;
                if (!shallask)
                {
                    listBox1.Items.Add(textBox1.Text);
                    Info("Added item \"" + textBox1.Text + "\".");
                    textBox1.ResetText();
                    UpdateCount();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AddSome a = new AddSome();
            if (a.ShowDialog() == System.Windows.Forms.DialogResult.Abort || a.toAdd.Length == 0) Info("Nothing added.");
            else
            {
                for (int i = 0; i < a.toAdd.Split('|').Length; i++) listBox1.Items.Add(a.toAdd.Split('|')[i]);
                Info("Added " + a.toAdd.Split('|').Length + " items.");
                UpdateCount();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) Error("Nothing selected.");
            else
            {
                Info("Removed item \"" + listBox1.Items[listBox1.SelectedIndex].ToString() + "\".");
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                UpdateCount();
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) Error("Nothing selected.");
            else
            {
                Info("Changed item \"" + listBox1.Items[listBox1.SelectedIndex].ToString() + "\" to \"" + textBox1.Text + "\".");
                listBox1.Items[listBox1.SelectedIndex] = textBox1.Text;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0) Error("The list is empty.");
            else
            {
                while (listBox1.Items.Count > 0) listBox1.Items.RemoveAt(0);
                Info("The list has been reseted.");
                UpdateCount();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0) Error("The value is empty.");
            else
            {
                bool flag = false;
                for (int i = searchpos + 1; i < listBox1.Items.Count && !flag; i++) if (listBox1.Items[i].ToString().Contains(textBox1.Text))
                    {
                        listBox1.SelectedIndex = i;
                        searchpos = i;
                        button5.Text = "Search Next\r\n(חפש את הבא)";
                        flag = true;
                    }
                if (!flag)
                {
                    searchpos = -1;
                    button5.Text = "Search Value\r\n(חפש ערך)";
                    listBox1.SelectedIndex = -1;
                    Info("Nothing found.");
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0) Error("The list is empty.");
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = "*.txt";
                sfd.Filter = "Text Files|*.txt|All Files|*.*";
                sfd.Title = "Save as...";
                if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Abort)
                {
                    try
                    {
                        string tw = string.Empty;
                        for (int i = 0; i < listBox1.Items.Count; i++) tw += listBox1.Items[i].ToString() + (i == listBox1.Items.Count - 1 ? "" : "\n");
                        System.IO.File.WriteAllText(sfd.FileName, tw);
                        Info("List saved as \"" + sfd.FileName + "\".");
                    }
                    catch
                    {
                        Error("Failed to save list.");
                    }
                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0) if (MessageBox.Show("Loading a list would replace your current list.\r\nAre you sure you want to continue?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.txt";
            ofd.Filter = "Text Files|*.txt|All Files|*.*";
            ofd.Title = "Load list...";
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Abort)
            {
                try
                {
                    while (listBox1.Items.Count > 0) listBox1.Items.RemoveAt(0);
                    string r = System.IO.File.ReadAllText(ofd.FileName);
                    for (int i = 0; i < r.Split('\n').Length; i++) listBox1.Items.Add(r.Split('\n')[i]);
                    Info("List \"" + ofd.FileName + "\" loaded.");
                    UpdateCount();
                }
                catch
                {
                    Error("Failed to load list.");
                }
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.txt";
            ofd.Filter = "Text Files|*.txt|All Files|*.*";
            ofd.Title = "Merge with...";
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Abort)
            {
                try
                {
                    string r = System.IO.File.ReadAllText(ofd.FileName);
                    for (int i = 0; i < r.Split('\n').Length; i++) listBox1.Items.Add(r.Split('\n')[i]);
                    Info("List \"" + ofd.FileName + "\" loaded.");
                    UpdateCount();
                }
                catch
                {
                    Error("Failed to merge lists.");
                }
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);
        }
        private void button14_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0) Error("The list is empty.");
            else
            {
                textBox3.ResetText();
                string[] newlist = new string[listBox1.Items.Count];
                int placeon = -1;
                Random rnd = new Random();
                for (int i = 0; i < newlist.Length; i++) newlist[i] = string.Empty;
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    placeon = rnd.Next(0, listBox1.Items.Count);
                    while (newlist[placeon].Length != 0) placeon = rnd.Next(0, listBox1.Items.Count);
                    newlist[placeon] = listBox1.Items[i].ToString();
                }
                for(int i = 0; i < newlist.Length; i++) textBox3.Text += newlist[i] + (i == newlist.Length - 1 ? "" : "\r\n");
                Info("Random list created.");
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            textBox3.Copy();
            Info("Output copied.");
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0) Error("The list is empty.");
            else
            {
                textBox3.ResetText();
                int placeon = -1, placeon2 = -1, toDel = -1;
                Random rnd = new Random();
		        if(listBox1.Items.Count % 2 != 0) toDel = listBox1.Items.Add("?");
                string[] newlist = new string[listBox1.Items.Count];
                for (int i = 0; i < newlist.Length; i++) newlist[i] = string.Empty;
		        for(int i = 0; i < listBox1.Items.Count / 2; i++)
		        {
			        placeon = rnd.Next(0,listBox1.Items.Count);
			        while(newlist[placeon].Length > 0) placeon = rnd.Next(0,listBox1.Items.Count);
			        placeon2 = rnd.Next(0,listBox1.Items.Count);
			        while(newlist[placeon2].Length > 0 || placeon == placeon2) placeon2 = rnd.Next(0,listBox1.Items.Count);
                    newlist[placeon] = listBox1.Items[placeon].ToString();
                    newlist[placeon2] = listBox1.Items[placeon2].ToString();
                    textBox3.Text += newlist[placeon] + textBox2.Text + newlist[placeon2] + (i == (newlist.Length / 2 - 1) ? "" : "\r\n");
		        }
                if (toDel != -1) listBox1.Items.RemoveAt(toDel);
                Info("Random list created.");
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label6.Text = "gmR" + textBox2.Text + "Hyper";
        }
    }
}