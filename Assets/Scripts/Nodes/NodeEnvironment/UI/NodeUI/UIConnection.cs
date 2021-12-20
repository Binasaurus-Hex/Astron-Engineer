using System;
using UnityEngine;

namespace Nodes.NodeEnvironment.UI.NodeUI {
	public class UIConnection : MonoBehaviour {
		private LineRenderer line;
		private Transform startPos;
		private Transform endPos;
		private bool connected;

		public void SetConnection(Transform start, Transform end) {
			startPos = start;
			endPos = end;
			connected = true;
		}

		private void Start() {
			line = GetComponent<LineRenderer>();
			line.positionCount = 2;
		}

		private void Update() {
			if (!connected) return;
			Vector3 startPosition = startPos.position;
			startPosition.z -= 0f;
			Vector3 endPosition = endPos.position;
			endPosition.z -= 0f;
			line.SetPosition(0,startPosition);
			line.SetPosition(1,endPosition);
		}
	}
}