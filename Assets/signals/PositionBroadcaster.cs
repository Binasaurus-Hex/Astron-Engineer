using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class PositionBroadcaster : MonoBehaviour {
	public List<IPositionListener> listeners = new List<IPositionListener>();
	private Vector3 previousPosition = Vector3.zero;

	private void FixedUpdate() {
		Vector3 currentPosition = transform.position;
		Vector3 transformation = currentPosition - previousPosition;
		if (transformation == Vector3.zero) {
			return;
		}
		previousPosition = currentPosition;
		foreach (IPositionListener listener in listeners) {
			listener.onPositionChanged(currentPosition);
		}
	}
}