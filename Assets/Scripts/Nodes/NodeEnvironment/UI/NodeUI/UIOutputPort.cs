using System;
using System.Collections.Generic;
using Nodes.NodeEnvironment.UI.NodeUI;
using Nodes.NodeEnvironment.VirtualCursor;
using ShipAbstractComponents.ports;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Nodes.NodeEnvironment.UI {
	public class UIOutputPort : MonoBehaviour, ICursorEventHandler, IOutputPortListener{
		private LineRenderer connectionPreview;
		private OutputPort outputPort;
		public Color hoverColor;
		public Color defaultColor;
		private Renderer renderer;
		private bool pressed;
		private UIConnection connection;

		private void Awake() {
			connectionPreview = GetComponent<LineRenderer>();
			connectionPreview.positionCount = 0;
			renderer = GetComponent<Renderer>();
			renderer.material.color = defaultColor;
			connection = GetComponentInChildren<UIConnection>();
		}

		private void Start() {
			if (outputPort.IsConnected()) {
				UIInputPort connectedInput = UIInputPort.inputPortMap[outputPort.input];
				connection.SetConnection(connectedInput.transform,transform);
			}
		}

		public void SetOutputPort(OutputPort port) {
			outputPort = port;
			outputPort.AddListener(this);
		}

		public void OnPressed(VirtualCursorEvent @event) {
			Vector3 centrePos = transform.position;
			centrePos.z -= 0f;
			connectionPreview.positionCount = 2;
			connectionPreview.SetPosition(0,centrePos);
			connectionPreview.SetPosition(1,centrePos);
			pressed = true;
		}

		public void OnReleased(VirtualCursorEvent @event) {
			if (!pressed) {
				return;
			}

			pressed = false;
			
			Ray releaseRay = new Ray(@event.position,Vector3.back);
			if (Physics.Raycast(releaseRay, out RaycastHit hit)) {
				if (hit.collider.gameObject.TryGetComponent<UIInputPort>(out UIInputPort port)) {
					if (AttachToInputPort(port.GetInputPort())) {
						connection.SetConnection(port.transform,transform);
					}
				}
			}
			connectionPreview.positionCount = 0;
		}

		private bool AttachToInputPort(InputPort inputPort) {
			if (AreCompatible(inputPort, outputPort)) {
				outputPort.SetInput(inputPort);
				return true;
			}

			return false;
		}

		private bool AreCompatible(InputPort input, OutputPort output) {
			return input.mediumType == output.mediumType;
		}

		public void OnDrag(VirtualCursorEvent @event) {
			connectionPreview.SetPosition(1, new Vector3(@event.position.x,@event.position.y,transform.position.z));
		}

		public void OnHover() {
			
		}

		public void OnCursorEnter(VirtualCursorEvent @event) {
			renderer.material.color = hoverColor;
		}

		public void OnCursorExit(VirtualCursorEvent @event) {
			renderer.material.color = defaultColor;
		}

		public void OnSend()
		{
			sent = true; 
		}

		public void OnConnection(OutputPort from, InputPort to) {
			
		}

		private bool sent;

		private void Update()
		{
			connection.GetComponent<Renderer>().material.color = sent ? Color.green : Color.gray;
			sent = false;
		}
		
	}
}