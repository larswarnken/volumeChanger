using System;
using System.Drawing.Design;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Gui;

namespace volumeChanger
{
    public partial class Form1 : Form
    {

        private SerialCommunicator _serialCommunicator;
        private CancellationTokenSource _cancellationTokenSource;

        private Form2 form2Instance;

        private VolumeController _volumeController;

        // Konstruktor der Form, erh‰lt den SerialCommunicator und den CancellationTokenSource aus der Main()
        public Form1(SerialCommunicator serialCommunicator, CancellationTokenSource cancellationTokenSource)
        {
            InitializeComponent();

            _volumeController = new VolumeController();
            _volumeController.PinValueChanged += volumeController_OnPinValueChanged;


            _serialCommunicator = serialCommunicator;
            _cancellationTokenSource = cancellationTokenSource;
        }

        public void volumeController_OnPinValueChanged(object sender, PinValueEventArgs e)
        {
            //volumeMeter1.Amplitude = e.PinValue;
            UpdatePanelHeight(200 - (e.PinValue * 2));
        }

        public void UpdatePanelHeight(int newHeight)
        {
            if (panelVolWhite.InvokeRequired)
            {
                // If the current thread is not the UI thread, use Invoke to marshal the call to the UI thread
                panelVolWhite1.Invoke(new Action<int>(UpdatePanelHeight), newHeight);
            }
            else
            {
                // Update the height of the panel
                panelVolWhite1
                    .Height = newHeight;
            }
        }

        // Methode zur Verarbeitung der empfangenen Daten
        public void UpdateLabel(string data)
        {
            if (label1.InvokeRequired)
            {
                label1.Invoke(new Action(() => UpdateLabel(data)));
            }
            else
            {
                label1.Text = data;
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

        private void panelVolWhite1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}