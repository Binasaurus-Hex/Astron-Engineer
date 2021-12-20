using System;
using ModularContainers;
using Nodes.NodeEnvironment.UI;
using Nodes.NodeEnvironment.VirtualCursor;
using SaveLoad;
using shipComponents;
using UnityEngine;

namespace Nodes.NodeEnvironment {
	public class NodeGenerator : MonoBehaviour {
		public ModularContainer container;
		public GameObject nodeTemplate;
		private NodeEnvironment environment;

		private void Awake() {
			container.onContainerUpdate += () => GenerateNodes();
		}

		public void GenerateNodes() {
			environment = GetComponent<NodeEnvironment>();
			ClearEnvironment();
			int iteration = 0;
			int spacing = 40;
			int offset = -150;
			foreach (FunctionalComponent functionalComponent in container.components) {
				GameObject node = Instantiate(nodeTemplate, transform);
				node.transform.localPosition = new Vector3(offset + iteration * spacing,0 , 0);
				node.GetComponent<UINode>().SetFunctionalComponent(functionalComponent);
				environment.AddChild(node);
				iteration++;
			}
		}

		public void ClearEnvironment() {
			environment.Clear();
		}
	}
}