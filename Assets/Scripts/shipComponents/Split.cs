using System;

namespace shipComponents
{
    public class Split : FunctionalComponent
    {
        public InputPort signal;
        public OutputPort outputA;
        public OutputPort outputB;

        private void Awake()
        {
            signal = new InputPort(typeof(Information));
            outputA = new OutputPort(typeof(Information));
            outputB = new OutputPort(typeof(Information));
        }

        private void Update()
        {
            if (outputA.IsConnected() && outputB.IsConnected())
            {
                Information inputSignal = signal.ReadValue() as Information;
                if (inputSignal != null)
                {
                    outputA.Send(inputSignal);
                    outputB.Send(inputSignal);
                }
            }
        }

        public override void Load(string obj) {
            
        }

        public override string Save() {
            return default;
        }
    }
}