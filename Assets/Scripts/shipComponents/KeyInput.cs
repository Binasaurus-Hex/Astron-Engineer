using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nodes.Serialization;
using ShipAbstractComponents.Controls;
using UnityEngine;

namespace shipComponents
{
    public class KeyInput : FunctionalComponent
    {
        public OutputPort clickedSignal;
        public Key key;

        private void Awake()
        {
            clickedSignal = new OutputPort(typeof(Information));
            key = new Key();
        }

        private void Update()
        {
            if (clickedSignal.IsConnected() && Input.GetKey(key.code))
            {
                clickedSignal.Send(new Information(1));
            }
        }

        public struct DataObject {
            public KeyCode keyCode;
        }

        public override void Load(string obj) {
            DataObject dataObject = JsonConvert.DeserializeObject<DataObject>(obj, new UnitySerialisationSettings());
            key.code = dataObject.keyCode;
        }

        public override string Save() {
            return JsonConvert.SerializeObject(new DataObject {
                keyCode = key.code
            },new UnitySerialisationSettings());
        }
    }
}