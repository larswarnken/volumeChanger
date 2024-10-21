using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;

namespace volumeChanger
{
    internal class VolumeController
    {
        int[] _pinValues = new int[5];
        int[] _previousValues = [0, 0, 0, 0, 0];

        public event EventHandler<PinValueEventArgs> PinValueChanged;

        public VolumeController()
        {
        }

        // Method to raise the event
        protected virtual void OnPinValueChanged(int pinValue)
        {
            PinValueChanged?.Invoke(this, new PinValueEventArgs(pinValue));
            Console.WriteLine("event raised");
        }

        public void SplitData(string data)
        {
            string[] parts = data.Split('|');
            for (int i = 0; i < 5; i++)
            {
                _pinValues[i] = int.Parse(parts[i]);
            }
        }

        public void ProcessVolumeData(string data)
        {
            SplitData(data);
            Console.WriteLine(_pinValues[0]);

            if (_pinValues[0] != _previousValues[0])
            {
                SetGeneralVolume(_pinValues[0]);
                _previousValues[0] = _pinValues[0];

                OnPinValueChanged(_pinValues[0]);
            }

        }

        public int[] GetPinValues()
        {
            return _pinValues;
        }

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