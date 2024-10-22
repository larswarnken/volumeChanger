using System.Windows.Forms;

namespace volumeChanger
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set text rendering before creating any controls
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // CancellationTokenSource erstellen, um den Task später abbrechen zu können
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            // SerialCommunicator erstellen
            SerialCommunicator serialCommunicator = new SerialCommunicator("COM3", 9600);

            // VolumeController erstellen
            VolumeController volumeController = new VolumeController();

            // Form1 erstellen und den SerialCommunicator übergeben
            Form1 form = new Form1(serialCommunicator, cancellationTokenSource);

            // Abonnieren des DataReceived-Events und Daten an die Form1 und VolumeController weiterleiten
            serialCommunicator.DataReceived += volumeController.ProcessVolumeData;
            volumeController.PinValueChanged += form.volumeController_OnPinValueChanged;


            // Starte die serielle Kommunikation in einem separaten Task
            Task.Run(() => StartSerialCommunication(serialCommunicator, cancellationTokenSource.Token))
            .ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Console.WriteLine($"Task failed: {t.Exception}");
                }
                else
                {
                    Console.WriteLine("Task completed successfully.");
                }
            });

            // Start der GUI
            Application.Run(form);
        }

        private static void ProcessIncomingData(string data)
        {
            Console.WriteLine($"Processing data: {data}");
            // Hier könntest du den Wert der Potentiometer verarbeiten und die Lautstärke anpassen
        }

        private static void StartSerialCommunication(SerialCommunicator communicator, CancellationToken token)
        {
            communicator.OpenPort();

            // Solange kein Abbruchsignal vorliegt, weiter Daten empfangen
            while (!token.IsCancellationRequested)
            {
                // Hier würde die normale serielle Kommunikation laufen
            }

            // Wenn das Abbruchsignal kommt, wird der Port geschlossen
            communicator.ClosePort();
        }
    }
}