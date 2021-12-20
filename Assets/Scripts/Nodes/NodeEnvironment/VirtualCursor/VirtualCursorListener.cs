using UnityEngine;

namespace Nodes.NodeEnvironment.VirtualCursor {
	public interface IVirtualCursorListener {
		void OnPressed(VirtualCursorEvent @event);
		void OnReleased(VirtualCursorEvent @event);
		void OnDrag(VirtualCursorEvent @event);
		void OnMoved(VirtualCursorEvent @event);
	}
}