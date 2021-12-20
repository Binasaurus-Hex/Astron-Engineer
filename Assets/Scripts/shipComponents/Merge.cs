using System;
using UnityEngine;

namespace shipComponents {
	public class Merge : FunctionalComponent {

		public InputPort a;
		public InputPort b;
		public OutputPort output;

		private void Awake() {
			a = new InputPort(typeof(Information));
			b = new InputPort(typeof(Information));
			output = new OutputPort(typeof(Information));
		}

		private void Update() {
			if (output.IsConnected()) {
				Information aValue = a.ReadValue() as Information;
				Information bValue = b.ReadValue() as Information;
				float aFloat = aValue?.GetValue() ?? 0;
				float bFloat = bValue?.GetValue() ?? 0;
				if (aFloat != 0 || bFloat != 0) {
					output.Send(new Information(aFloat + bFloat));
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