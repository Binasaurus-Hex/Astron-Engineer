using System;
using System.Collections.Generic;
using Nodes.NodeEnvironment.VirtualCursor;
using UnityEngine;

namespace Nodes.NodeEnvironment.UI {
	public class UIInputPort : MonoBehaviour, ICursorEventHandler {

		private InputPort inputPort;
		private Type mediumType;
		private UILabel portName;
		private Renderer renderer;
		public Color hoverColor;
		public Color defaultColor;
		public static Dictionary<InputPort, UIInputPort> inputPortMap = new Dictionary<InputPort, UIInputPort>();

		private void Awake() {
			renderer = GetComponent<Renderer>();
			renderer.material.color = defaultColor;
		}

		public void SetInputPort(InputPort port) {
			inputPort = port;
			mediumType = inputPort.mediumType;
			inputPortMap[port] = this;
		}

		public InputPort GetInputPort() {
			return inputPort;
		}

		public void OnPressed(VirtualCursorEvent @event) {
			UpdateDisplay();
		}

		private void UpdateDisplay() {
			if (mediumType == typeof(Coolant)) {
				renderer.material.color = Color.yellow;
				
			}
			else if (mediumType == typeof(Power)) {
				renderer.material.color = Color.blue;
				print($"port value : {inputPort.ReadValue()}");
			}
		}

		public void OnReleased(VirtualCursorEvent @event) {
			
		}

		public void OnDrag(VirtualCursorEvent @event) {
			
		}

		public void OnHover() {
		}

		public void OnCursorEnter(VirtualCursorEvent @event) {
			renderer.material.color = hoverColor;
		}

		public void OnCursorExit(VirtualCursorEvent @event) {
			renderer.material.color = defaultColor;
		}

		public void OnMoved(VirtualCursorEvent @event) {
			
		}
	}
}