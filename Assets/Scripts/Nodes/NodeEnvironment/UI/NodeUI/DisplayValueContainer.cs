using System;
using UnityEngine;

namespace Nodes.NodeEnvironment.UI.NodeUI {
	public class DisplayValueContainer : MonoBehaviour {
		private UILabel label;

		private void Awake() {
			GetComponentInChildren<UILabel>();
		}

		public void SetValue(string value) {
			label.SetText(value);
		}
	}
}