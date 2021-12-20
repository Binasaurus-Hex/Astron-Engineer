using Nodes.NodeEnvironment.VirtualCursor;
using UnityEngine;

namespace Nodes.NodeEnvironment.UI {
	public class NodeCursorEventHandler : MonoBehaviour, ICursorEventHandler {
		public Color hoverColor;
		public Color defaultColor;

		private void Update() {
			GetComponent<Renderer>().material.color = defaultColor;
		}

		public void OnPressed(VirtualCursorEvent @event) {
			
		}

		public void OnReleased(VirtualCursorEvent @event) {
			
		}

		public void OnDrag(VirtualCursorEvent @event) {
			if (@event.button.id == MouseButton.LEFT_MOUSE) {
				GetComponent<Renderer>().material.color = hoverColor;
				transform.position += new Vector3(@event.delta.x,@event.delta.y,0);
			}
		}

		public void OnHover() {
			GetComponent<Renderer>().material.color = hoverColor;
		}

		public void OnCursorEnter(VirtualCursorEvent @event) {
			
		}

		public void OnCursorExit(VirtualCursorEvent @event) {
			
		}
	}
}