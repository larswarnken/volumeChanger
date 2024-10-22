using System;
using System.Drawing.Design;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Gui;

namespace volumeChanger
{
    public partial class Form1 : Form
    {
        // instances
        private SerialCommunicator _serialCommunicator;
        private CancellationTokenSource _cancellationTokenSource;

        private Form2? form2Instance;

        private VolumeController _volumeController;

        Panel[] panelsArray = new Panel[5];
        Label[] volumeLabelArray = new Label[5];

        // Konstruktor der Form, erh‰lt den SerialCommunicator und den CancellationTokenSource aus der Main()
        public Form1(SerialCommunicator serialCommunicator, CancellationTokenSource cancellationTokenSource)
        {
            InitializeComponent();

            _volumeController = new VolumeController();
            _volumeController.PinValueChanged += volumeController_OnPinValueChanged;

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
                pictureBox1.Image = Image.FromFile("icons/speaker2.png");
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

        // Eventhandler f¸r das PinValueChanged-Event des VolumeControllers
        public void volumeController_OnPinValueChanged(object? sender, PinValueEventArgs e)
        {
            //volumeMeter1.Amplitude = e.PinValue;
            UpdatePanelHeight(e.Pin, 200 - (e.PinValue * 2));
            UpdateVolumeLabel(e.Pin, e.PinValue);
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
            }
            else
            {
                form2Instance.BringToFront();
            }
        }
    }
}