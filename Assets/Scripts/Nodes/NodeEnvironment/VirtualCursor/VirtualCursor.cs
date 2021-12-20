using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nodes.NodeEnvironment.VirtualCursor {
	public class VirtualCursor : MonoBehaviour {
		private Vector2 delta;
		private Vector2 previousPosition;
		private Vector2 position;
		private List<MouseButton> mouseButtons;

		private List<IVirtualCursorListener> eventListeners;

		public VirtualCursor() {
			eventListeners = new List<IVirtualCursorListener>();
			mouseButtons = new List<MouseButton> {
				new MouseButton(0),new MouseButton(1),new MouseButton(2)
			};
		}

		public void AddListener(IVirtualCursorListener listener) {
			eventListeners.Add(listener);
		}

		public void RemoveListener(IVirtualCursorListener listener) {
			eventListeners.Remove(listener);
		}

		public void SetCursorPosition(Vector2 position) {
			this.position = position;
		}

		private void Update() {
			CalculateEvents();
		}

		public void CalculateEvents() {
			delta = position - previousPosition;
			mouseButtons.ForEach(mouseButton => {

				VirtualCursorEvent @event = new VirtualCursorEvent(mouseButton) {
					position = position, previousPosition = previousPosition, delta = delta
				};

				if (delta != Vector2.zero) {
					eventListeners.ForEach(listener => listener.OnMoved(@event));
				}

				if (mouseButton.IsPressed()) {
					eventListeners.ForEach(listener => listener.OnPressed(@event));
				}
				if (mouseButton.IsReleased()) {
					eventListeners.ForEach(listener => listener.OnReleased(@event));
				}

				if (mouseButton.IsDown()) {
					if (mouseButton.pressedLastFrame && delta != Vector2.zero) {
						eventListeners.ForEach(listener => listener.OnDrag(@event));
					}

					mouseButton.pressedLastFrame = true;
				}
				else {
					mouseButton.pressedLastFrame = false;
				}
			});

			previousPosition = position;
		}
	}
}