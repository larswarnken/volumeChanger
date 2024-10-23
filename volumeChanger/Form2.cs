using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace volumeChanger
{
    public partial class Form2 : Form
    {
        private RichTextBox lastFocusedTextBox;

        public Form2()
        {
            InitializeComponent();

            pictureBoxListIcon1.Image = Image.FromFile("icons/brave.png");
            pictureBoxListIcon2.Image = Image.FromFile("icons/spotify.png");
            pictureBoxListIcon3.Image = Image.FromFile("icons/discord.png");
            pictureBoxListIcon4.Image = Image.FromFile("icons/gaming.png");

            richTextBox1.Enter += textBox_Enter;
            richTextBox2.Enter += textBox_Enter;
            richTextBox3.Enter += textBox_Enter;
            richTextBox4.Enter += textBox_Enter;
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            lastFocusedTextBox = sender as RichTextBox ?? throw new ArgumentNullException(nameof(sender));
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Perform button click actions here

            // Set focus back to the text box
            lastFocusedTextBox?.Focus();
        }
    }
}
