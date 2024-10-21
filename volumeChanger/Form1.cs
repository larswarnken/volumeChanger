using System;
using System.Windows.Forms;

namespace volumeChanger
{
    public partial class Form1 : Form
    {
        private SerialCommunicator _serialCommunicator;
        private CancellationTokenSource _cancellationTokenSource;

        // Konstruktor der Form, erh‰lt den SerialCommunicator und den CancellationTokenSource aus der Main()
        public Form1(SerialCommunicator serialCommunicator, CancellationTokenSource cancellationTokenSource)
        {
            InitializeComponent();
            _serialCommunicator = serialCommunicator;
            _cancellationTokenSource = cancellationTokenSource;
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
    }
}
