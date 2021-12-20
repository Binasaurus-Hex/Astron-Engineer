using System;
using System.Collections.Generic;
using Nodes.ComponentReading;
using Nodes.NodeEnvironment.UI.NodeUI;
using ShipAbstractComponents.Controls;
using shipComponents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utils;

namespace Nodes.NodeEnvironment.UI {
	public class UINode : MonoBehaviour {
		private UILabel titleLabel;
		public GameObject inputPortContainerPrefab;
		public GameObject outputPortContainerPrefab;
		public GameObject sliderContainerPrefab;
		public GameObject keyInputContainerPrefab;
		public GameObject footerPrefab;
		private FunctionalComponent functionalComponent;
		private ThemeSetting themeSetting;
		private Color color;

		private void Awake()
		{
			titleLabel = GetComponentInChildren<UILabel>();
			themeSetting = GetComponent<ThemeSetting>();
		}

		private void Start()
		{
			themeSetting.SetStyle(ThemeStyling.Style.Highlight, color);
		}

		public void SetFunctionalComponent(FunctionalComponent component) {
			functionalComponent = component;
			GenerateFromComponent(functionalComponent);
		}

		private void GenerateFromComponent(FunctionalComponent component) {
			ObjectReader reader = new ObjectReader(component);
			titleLabel.SetText(reader.GetName());
			List<MemberVariablePackage> memberPackages = reader.GetPackage();
			foreach (MemberVariablePackage memberPackage in memberPackages)
			{
				object member = memberPackage.GetObject();
				if (member is InputPort)
				{
					GameObject inputPort = Instantiate(inputPortContainerPrefab, transform);
					inputPort.GetComponent<UIInputPortContainer>().SetPort(member as InputPort, memberPackage.GetName());
				}
				else if (member is OutputPort)
				{
					GameObject outputPort = Instantiate(outputPortContainerPrefab, transform);
					outputPort.GetComponent<UIOutputPortContainer>().SetPort(member as OutputPort, memberPackage.GetName());
				}
				else if (member is ClampedNumber)
				{
					GameObject slider = Instantiate(sliderContainerPrefab, transform);
					slider.GetComponent<UISliderContainer>().SetClampedNumber(member as ClampedNumber, memberPackage.GetName());
				}
				else if (member is Color)
				{
					color = (Color) member;
				}
				else if (member is Key)
				{
					GameObject keyInput = Instantiate(keyInputContainerPrefab, transform);
					keyInput.GetComponentInChildren<UIKeyInput>().SetKey(member as Key);
				}
			}

			Instantiate(footerPrefab, transform);
		}
	}
}