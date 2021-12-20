using Nodes.NodeEnvironment.VirtualCursor;

namespace Nodes.NodeEnvironment {
	public interface ICursorPointedListener {
		void OnCursorEnter(VirtualCursorEvent @event);
		void OnCursorExit(VirtualCursorEvent @event);
	}
}