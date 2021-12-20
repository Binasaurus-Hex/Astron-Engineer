using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nodes.NodeEnvironment.UI {
	[ExecuteInEditMode]
	public class UILabel : MonoBehaviour {
		private TextMeshPro text;
		private LayoutElement layout;
		private RectTransform rectT;
		private UIRect rect;
		public float xPadding;
		public float yPadding;
		public bool affectWidth;
		public bool affectHeight;
		private bool initialised;
		private void Awake() {
			text = GetComponentInChildren<TextMeshPro>();
			rectT = GetComponent<RectTransform>();
			layout = GetComponent<LayoutElement>();
			rect = GetComponent<UIRect>();
		}

		private void Init()
		{
			text = GetComponentInChildren<TextMeshPro>();
			rectT = GetComponent<RectTransform>();
			layout = GetComponent<LayoutElement>();
			rect = GetComponent<UIRect>();
			initialised = true;
		}

		public void SetText(string toValue) {
			text.text = toValue;
		}

		private void Update()
		{
			if (!initialised) Init();
			if(affectWidth) layout.minWidth = text.preferredWidth + xPadding;
			if(affectHeight) layout.minHeight = text.preferredHeight + yPadding;
			PositionText();
		}

		private void PositionText()
		{
			Vector3 localPos = text.transform.localPosition;
			float offsetThickness = 0.05f;
			localPos.z = -rect.thickness/2 - offsetThickness;
			text.transform.localPosition = localPos;
		}
	}
}