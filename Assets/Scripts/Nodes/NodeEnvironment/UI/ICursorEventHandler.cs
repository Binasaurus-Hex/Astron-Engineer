using Nodes.NodeEnvironment.VirtualCursor;

namespace Nodes.NodeEnvironment.UI {
	public interface ICursorEventHandler {
		void OnPressed(VirtualCursorEvent @event);
		void OnReleased(VirtualCursorEvent @event);
		void OnDrag(VirtualCursorEvent @event);
		void OnHover();
		void OnCursorEnter(VirtualCursorEvent @event);
		void OnCursorExit(VirtualCursorEvent @event);
	}
}