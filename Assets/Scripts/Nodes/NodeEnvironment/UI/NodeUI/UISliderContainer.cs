using System;
using ShipAbstractComponents.Controls;
using UnityEngine;

namespace Nodes.NodeEnvironment.UI.NodeUI
{
    public class UISliderContainer : MonoBehaviour
    {
        public UISlider slider;
        public UILabel title;

        public void SetClampedNumber(ClampedNumber number, string name)
        {
            slider.number = number;
            title.SetText(name);
        }
    }
}