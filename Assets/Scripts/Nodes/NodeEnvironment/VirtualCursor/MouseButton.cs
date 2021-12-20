using UnityEngine;

namespace Nodes.NodeEnvironment.VirtualCursor {
	public class MouseButton {
		public int id;
		public bool pressedLastFrame;

		public static int LEFT_MOUSE = 0;
		public static int RIGHT_MOUSE = 1;
		public static int MIDDLE_MOUSE = 2;

		public MouseButton(int id) {
			this.id = id;
		}

		public bool IsPressed() {
			return Input.GetMouseButtonDown(id);
		}

		public bool IsReleased() {
			return Input.GetMouseButtonUp(id);
		}

		public bool IsDown() {
			return Input.GetMouseButton(id);
		}

	}
}