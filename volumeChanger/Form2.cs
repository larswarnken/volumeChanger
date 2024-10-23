using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace volumeChanger
{
    public partial class Form2 : Form
    {
        private RichTextBox? lastFocusedTextBox;

        private SettingManager? settingsManager;

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

            settingsManager = new SettingManager();

            LoadSettings();

            ActiveControl = null;  //this = form
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            // Set focus to the dummy button to remove focus from text boxes
            ActiveControl = null;  //this = form
        }


        private void textBox_Enter(object? sender, EventArgs e)
        {
            lastFocusedTextBox = sender as RichTextBox ?? throw new ArgumentNullException(nameof(sender));
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Perform button click actions here
            settingsManager?.SaveSettings(richTextBox1, richTextBox2, richTextBox3, richTextBox4);

            // Set focus back to the text box
            lastFocusedTextBox?.Focus();

            // Close the form
            this.Close();
        }

        public void LoadSettings()
        {
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(exeDirectory, "settings.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var content = JsonConvert.DeserializeObject<dynamic>(json);

                richTextBox1.Text = content.Pin1;
                richTextBox2.Text = content.Pin2;
                richTextBox3.Text = content.Pin3;
                richTextBox4.Text = content.Pin4;
            }
            else
            {
                MessageBox.Show("Settings file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
