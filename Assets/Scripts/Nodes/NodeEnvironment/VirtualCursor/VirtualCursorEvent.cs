using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nodes.NodeEnvironment.VirtualCursor {
	public class VirtualCursorEvent {
		public Vector2 position;
		public Vector2 previousPosition;
		public Vector2 delta;
		public MouseButton button;

		public VirtualCursorEvent(MouseButton button) {
			this.button = button;
		}
	}
}