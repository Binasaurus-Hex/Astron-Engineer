using Nodes.NodeEnvironment.VirtualCursor;
using UnityEngine;
using UnityEngine.UI;

namespace Nodes.NodeEnvironment {
	public class NodeEnvironmentController : IVirtualCursorListener {

		private NodeEnvironment environment;

		public NodeEnvironmentController(NodeEnvironment environment) {
			this.environment = environment;
		}
		public void OnPressed(VirtualCursorEvent @event) {
			
		}

		public void OnReleased(VirtualCursorEvent @event) {
			
		}

		public void OnDrag(VirtualCursorEvent @event) {
			if (@event.button.id == MouseButton.MIDDLE_MOUSE) {
				environment.Pan(@event.delta);
			}
		}

		public void OnMoved(VirtualCursorEvent @event) {
			
		}
	}
}