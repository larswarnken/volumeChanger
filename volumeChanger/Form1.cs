using System;
using System.Drawing.Design;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Gui;
using static System.Windows.Forms.DataFormats;

namespace volumeChanger
{
    public partial class Form1 : Form
    {
        // instances
        private SerialCommunicator _serialCommunicator;
        private CancellationTokenSource _cancellationTokenSource;

        private Form2? form2Instance;

        private VolumeController _volumeController;
        private SwitchController _switchController;

        Panel[] panelsArray = new Panel[5];
        Label[] volumeLabelArray = new Label[5];

        private bool isFirstTimeOpened = true;

        // Konstruktor der Form, erh‰lt den SerialCommunicator und den CancellationTokenSource aus der Main()
        public Form1(SerialCommunicator serialCommunicator, CancellationTokenSource cancellationTokenSource)
        {
            InitializeComponent();

            this.Load += new EventHandler(MainForm_Load);

            this.Resize += new EventHandler(Form1_Resize);
            notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseDoubleClick);

            _volumeController = new VolumeController();
            _volumeController.PinValueChanged += volumeController_OnPinValueChanged;

            _switchController = new SwitchController(); 
            _switchController.SwitchValueChanged += switchController_OnSwitchValueChanged;

            _serialCommunicator = serialCommunicator;
            _cancellationTokenSource = cancellationTokenSource;

            panelsArray[0] = panelVolWhite1;
            panelsArray[1] = panelVolWhite2;
            panelsArray[2] = panelVolWhite3;
            panelsArray[3] = panelVolWhite4;
            panelsArray[4] = panelVolWhite5;

            volumeLabelArray[0] = labelVol1;
            volumeLabelArray[1] = labelVol2;
            volumeLabelArray[2] = labelVol3;
            volumeLabelArray[3] = labelVol4;
            volumeLabelArray[4] = labelVol5;

            try
            {
                if(_switchController.getSwitchValue() == 0 )
                {
                    pictureBox1.Image = Image.FromFile("icons/headphones.png");
                }
                else if (_switchController.getSwitchValue() == 1)
                {
                    pictureBox1.Image = Image.FromFile("icons/speaker2.png");
                }

                pictureBoxIcon1.Image = Image.FromFile("icons/speakerVolume.png");
                pictureBoxIcon2.Image = Image.FromFile("icons/brave.png");
                pictureBoxIcon3.Image = Image.FromFile("icons/spotify.png");
                pictureBoxIcon4.Image = Image.FromFile("icons/discord.png");
                pictureBoxIcon5.Image = Image.FromFile("icons/gaming.png");

            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Image file not found: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (isFirstTimeOpened)
            {
                this.WindowState = FormWindowState.Minimized;
                isFirstTimeOpened = false;
            }
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }


        // Eventhandler f¸r das PinValueChanged-Event des VolumeControllers
        public void volumeController_OnPinValueChanged(object? sender, PinValueEventArgs e)
        {
            //volumeMeter1.Amplitude = e.PinValue;
            UpdatePanelHeight(e.Pin, 200 - (e.PinValue * 2));
            UpdateVolumeLabel(e.Pin, e.PinValue);
        }

        public void switchController_OnSwitchValueChanged(object? sender, SwitchValueEventArgs e)
        {
            try
            {
                if (e.SwitchValue == 0)
                {
                    pictureBox1.Image = Image.FromFile("icons/headphones.png");
                }
                else if (e.SwitchValue == 1)
                {
                    pictureBox1.Image = Image.FromFile("icons/speaker2.png");
                }
                else
                {
                    Console.WriteLine("Invalid switch value");
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Image file not found: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        // update the height of the panel
        public void UpdatePanelHeight(int panelNumber, int newHeight)
        {
            if (panelVolWhite.InvokeRequired)
            {
                // If the current thread is not the UI thread, use Invoke to marshal the call to the UI thread
                panelVolWhite.Invoke(new Action(() => UpdatePanelHeight(panelNumber, newHeight)));
            }
            else
            {
                // Update the height of the panel
                panelsArray[panelNumber].Height = newHeight;
            }
        }

        public void UpdateVolumeLabel(int pin, int value)
        {
            if (labelVol1.InvokeRequired)
            {
                labelVol1.Invoke(new Action(() => UpdateVolumeLabel(pin, value)));
            }
            else
            {
                volumeLabelArray[pin].Text = value.ToString();
            }
        }

        // Form schlieﬂen und den Thread stoppen
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Sende das Abbruchsignal
            _cancellationTokenSource.Cancel();

            // Warte einen Moment, um sicherzustellen, dass der Task beendet wird
            System.Threading.Thread.Sleep(500);

            base.OnFormClosed(e);
        }

        // Button zum ÷ffnen des Form2
        private void buttonPrograms_Click(object sender, EventArgs e)
        {
            if (form2Instance == null || form2Instance.IsDisposed)
            {
                form2Instance = new Form2();
                form2Instance.Show();
                form2Instance.Activate(); // Set focus to Form2
            }
            else
            {
                form2Instance.BringToFront();
            }
        }
    }
}