using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using WindowsInput;
using WindowsInput.Native;

namespace volumeChanger
{
    internal class SwitchController
    {
        int _outputValue = 0;

        public event EventHandler<SwitchValueEventArgs>? SwitchValueChanged;

        public SwitchController()
        { 
        }

        // Method to raise the event
        protected virtual void OnSwitchValueChanged(int switchValue)
        {
            SwitchValueChanged?.Invoke(this, new SwitchValueEventArgs(switchValue));
        }

        public int getSwitchValue()
        {
            return _outputValue;
        }

        public void ProcessSwitchData(string data)
        {
            string[] parts = data.Split('|');
            int switchValue = int.Parse(parts[5]);

            if (switchValue != _outputValue)
            {
                OnSwitchValueChanged(switchValue);

                var sim = new InputSimulator();

                if (switchValue == 0)
                {
                    sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.SHIFT, VirtualKeyCode.MENU }, VirtualKeyCode.VK_Q);
                    _outputValue = switchValue;
                }
                else if (switchValue == 1)
                {
                    sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.SHIFT, VirtualKeyCode.MENU }, VirtualKeyCode.VK_W);
                    _outputValue = switchValue;
                }
                else
                {
                    Console.WriteLine("Invalid switch value");
                }




            }
        }
    }
}


public class SwitchValueEventArgs : EventArgs
{
    public int SwitchValue { get; }

    public SwitchValueEventArgs(int switchValue)
    {
        SwitchValue = switchValue;
    }
}