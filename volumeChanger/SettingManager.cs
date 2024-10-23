using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace volumeChanger
{
    internal class SettingManager
    {
        public SettingManager()
        {
            // Constructor logic here
        }

        public void SaveSettings(RichTextBox rtb1, RichTextBox rtb2, RichTextBox rtb3, RichTextBox rtb4)
        {
            var content = new
            {
                Pin1 = rtb1.Text,
                Pin2 = rtb2.Text,
                Pin3 = rtb3.Text,
                Pin4 = rtb4.Text
            };

            string json = JsonConvert.SerializeObject(content, Newtonsoft.Json.Formatting.Indented);
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(exeDirectory, "settings.json");

            File.WriteAllText(filePath, json);
        }
    }
}
