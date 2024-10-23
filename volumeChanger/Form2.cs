using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace volumeChanger
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            pictureBoxListIcon1.Image = Image.FromFile("icons/brave.png");
            pictureBoxListIcon2.Image = Image.FromFile("icons/spotify.png");
            pictureBoxListIcon3.Image = Image.FromFile("icons/discord.png");
            pictureBoxListIcon4.Image = Image.FromFile("icons/gaming.png");
        }
    }
}
