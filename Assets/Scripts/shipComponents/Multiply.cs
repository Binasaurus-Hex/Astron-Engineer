using System;
using UnityEngine;

namespace shipComponents
{
    public class Multiply : FunctionalComponent
    {
        public InputPort a;
        public InputPort b;
        public OutputPort result;
        public Color color = Color.blue;

        private void Awake()
        {
            a = new InputPort(typeof(Information));
            b = new InputPort(typeof(Information));
            result = new OutputPort(typeof(Information));
        }

        private void Update()
        {
            if (result.IsConnected())
            {
                Information aVal = a.ReadValue() as Information;
                Information bVal = b.ReadValue() as Information;
                if (aVal != null && bVal != null)
                {
                    result.Send(new Information(aVal.GetValue() * bVal.GetValue()));
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