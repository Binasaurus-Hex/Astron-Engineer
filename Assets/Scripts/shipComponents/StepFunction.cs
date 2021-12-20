using System;
using Newtonsoft.Json;
using Nodes.Serialization;
using ShipAbstractComponents.Controls;
using UnityEngine;

namespace shipComponents {
	public class StepFunction : FunctionalComponent
	{
		public float value = 1;
		public OutputPort stepValue;
		public ClampedNumber multiplier;
		public Color color = Color.white;

		private void Awake() {
			stepValue = new OutputPort(typeof(Information));
			multiplier = new ClampedNumber(0,0,1000f);
		}

		private void Update() {
			if (stepValue.IsConnected()) {
				stepValue.Send(new Information(value * multiplier.CurrentValue));
			}
		}

		struct DataObject {
			public float multiplierValue;
		}

		public override void Load(string obj) {
			DataObject dataObject = JsonConvert.DeserializeObject<DataObject>(obj, new UnitySerialisationSettings());
			multiplier.CurrentValue = dataObject.multiplierValue;
		}

		public override string Save() {
			return JsonConvert.SerializeObject(new DataObject {
				multiplierValue = multiplier.CurrentValue
			}, new UnitySerialisationSettings());
		}
	}
}