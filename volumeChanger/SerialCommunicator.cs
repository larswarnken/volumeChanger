using System;
using System.IO.Ports;

namespace volumeChanger
{
    public class SerialCommunicator
    {
        private SerialPort _serialPort;
        private string _comPort;
        private int _baudRate;
        private readonly object _lock = new object();
        private Thread? _readThread; // Declare _readThread as nullable

        // Event for incoming data
        public event Action<string> DataReceived = delegate { };

        // Constructor to initialize with COM port and baud rate
        public SerialCommunicator(string comPort = "COM3", int baudRate = 9600)
        {
            _comPort = comPort;
            _baudRate = baudRate;

            // Initialize SerialPort object
            _serialPort = new SerialPort
            {
                PortName = _comPort,
                BaudRate = _baudRate,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeout = 10000,
                WriteTimeout = 1000
            };
        }

        // Opens the serial port and starts the read thread
        public void OpenPort()
        {
            lock (_lock)
            {
                if (!_serialPort.IsOpen)
                {
                    try
                    {
                        _serialPort.Open();
                        Console.WriteLine("Serial port opened successfully.");

                        _readThread = new Thread(ReadFromPort)
                        {
                            IsBackground = true // Set the thread as a background thread
                        };
                        _readThread.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to open port: {ex.Message}");
                    }
                }
            }
        }

        // Closes the serial port and stops the read thread
        public void ClosePort()
        {
            lock (_lock)
            {
                if (_serialPort.IsOpen)
                {
                    _readThread?.Join(); // Wait for the read thread to finish
                    _serialPort.Close();
                    Console.WriteLine("Serial port closed.");
                }
            }
        }

        // Method to continuously read from the serial port
        private void ReadFromPort()
        {
            while (true)
            {
                try
                {
                    string incomingData = _serialPort.ReadLine(); // Read data as string
                    //Console.WriteLine(incomingData);

                    // Trigger the DataReceived event so other classes can process the data
                    DataReceived?.Invoke(incomingData);
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Read timeout occurred.");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operation was canceled.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }
    }
}
