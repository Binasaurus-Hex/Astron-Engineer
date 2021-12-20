using System;
using Nodes.NodeEnvironment;
using Nodes.NodeEnvironment.UI;
using Nodes.NodeEnvironment.VirtualCursor;
using UnityEngine;

namespace Nodes {
	/**
	 * class to 'join' together the events from the pure mouse control, and the mouse collision events, when the cursor enters an object
	 */
	public class CursorEventCombiner : MonoBehaviour, IVirtualCursorListener {
		private bool hovering;
		private bool hoveringLastFrame;
		private bool selected;
		private VirtualCursorEvent currentEvent;
		private ICursorEventHandler target;

		private void Start() {
			target = GetComponent<ICursorEventHandler>();
		}

		private void LateUpdate() {

			// check for events
			if (!hovering && hoveringLastFrame) {
				target.OnCursorExit(currentEvent);
			}

			if (hovering && !hoveringLastFrame) {
				target.OnCursorEnter(currentEvent);
			}
			
			// set state
			if (hovering) {
				target.OnHover();
				hoveringLastFrame = true;
			}
			else {
				hoveringLastFrame = false;
			}
			hovering = false;
		}

		public void OnCursorOver() {
			if (gameObject.TryGetComponent<UIOutputPort>(out UIOutputPort component)) {
			}
			hovering = true;
		}

		public void OnPressed(VirtualCursorEvent @event) {
			if (hovering) {
				selected = true;
				target.OnPressed(@event);
			}
			else {
				selected = false;
			}
		}

		public void OnReleased(VirtualCursorEvent @event) {
			target.OnReleased(@event);
		}

		public void OnDrag(VirtualCursorEvent @event) {
			if (selected) {
				target.OnDrag(@event);
			}
		}

		public void OnMoved(VirtualCursorEvent @event) {
			currentEvent = @event;
		}
	}
}