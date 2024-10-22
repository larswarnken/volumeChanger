using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System;
using System.Linq;

namespace volumeChanger
{
    internal class VolumeController
    {
        int[] _pinValues = new int[5];
        int[] _previousValues = new int[5];


        // Erstelle ein festes Array mit der Größe 4, das 4 Listen von Strings enthält
        List<string>[] programArray = new List<string>[4];



        public event EventHandler<PinValueEventArgs>? PinValueChanged;

        public VolumeController()
        {
            PinValueChanged = null;

            // Initialisiere jede Liste in dem Array
            for (int i = 0; i < programArray.Length; i++)
            {
                programArray[i] = new List<string>();
            }

            // Füge Werte zu den Listen hinzu
            programArray[0].Add("brave");
            programArray[0].Add("chrome");
            programArray[1].Add("Spotify");
            programArray[1].Add("spotify");
            programArray[2].Add("discord");
            programArray[3].Add("valorant");

        }

        // Method to raise the event
        protected virtual void OnPinValueChanged(int pinValue)
        {
            PinValueChanged?.Invoke(this, new PinValueEventArgs(pinValue));
        }

        // speichert die einzelnen Werte in dem pin values array
        public void SplitData(string data)
        {
            string[] parts = data.Split('|');
            for (int i = 0; i < 5; i++)
            {
                _pinValues[i] = int.Parse(parts[i]);
            }
        }

        // Method to process the volume data
        public void ProcessVolumeData(string data)
        {
            // split data into individual values
            SplitData(data);

            // general volume
            if (_pinValues[0] != _previousValues[0])
            {
                SetGeneralVolume(_pinValues[0]);
                _previousValues[0] = _pinValues[0];

                OnPinValueChanged(_pinValues[0]);
            }

            // process volume
            for (int i = 1; i < 5; i++)
            {
                if (_pinValues[i] != _previousValues[i])
                {
                    foreach (var program in programArray[i - 1])
                    {
                        SetProcessVolume(program, _pinValues[i]);

                        _previousValues[i] = _pinValues[i];
                    }
                }
            }


        }

        // Method to get the pin values
        public int[] GetPinValues()
        {
            return _pinValues;
        }

        // Method to set the general volume
        public void SetGeneralVolume(int volumeLevel)
        {
            if (volumeLevel < 0 || volumeLevel > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(volumeLevel), "Volume level must be between 0 and 100");
            }

            float volumeScalar = volumeLevel / 100.0f;

            var deviceEnumerator = new MMDeviceEnumerator();
            var defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volumeScalar;
        }

        // Method to set the volume of a specific program
        public void SetProcessVolume(string processName, int volume)
        {
            if (volume < 0 || volume > 100)
                throw new ArgumentOutOfRangeException(nameof(volume), "Lautstärke muss zwischen 0 und 100 liegen.");

            // Verwende den Audio-Endpunkt
            var deviceEnumerator = new MMDeviceEnumerator();
            var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            // Suche nach allen aktiven Audio-Sessions
            var sessions = device.AudioSessionManager.Sessions;


            // Iteriere über alle Sessions, um den Prozess zu finden
            for (int i = 0; i < sessions.Count; i++)
            {
                var session = sessions[i];
                var processId = session.GetProcessID;


                try
                {
                    var process = Process.GetProcessById((int)processId);

                    // Überprüfen, ob der Prozessname übereinstimmt
                    if (process.ProcessName.Equals(processName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Setze die Lautstärke (zwischen 0.0 und 1.0)
                        session.SimpleAudioVolume.Volume = volume / 100f;
                        Console.WriteLine($"Setze Lautstärke für {processName} (PID: {process.Id}) auf {volume}%.");
                        return;
                    }
                }
                catch (ArgumentException)
                {
                    // Ignoriere Prozesse, die nicht mehr existieren
                    continue;
                }
            }
        }
    }
}

public class PinValueEventArgs : EventArgs
{
    public int PinValue { get; }

    public PinValueEventArgs(int pinValue)
    {
        PinValue = pinValue;
    }
}
