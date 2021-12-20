using System;
using Nodes.NodeEnvironment.VirtualCursor;
using ShipAbstractComponents.Controls;
using UnityEngine;

namespace Nodes.NodeEnvironment.UI.NodeUI
{
    public class UIKeyInput : MonoBehaviour,ICursorEventHandler
    {
        private bool activated;
        public UILabel label;
        private Key key;

        private void Awake()
        {
            label.SetText("click to set value");
        }

        public void SetKey(Key k)
        {
            key = k;
            label.SetText(key.code.ToString());
        }

        private void Update()
        {
            if (activated)
            {
                string inputString = Input.inputString.ToUpper();
                if (inputString != "")
                {
                    KeyCode k = (KeyCode) Enum.Parse(typeof(KeyCode), inputString);
                    key.code = k; 
                    label.SetText(key.code.ToString());
                }
            }
        }

        public void OnPressed(VirtualCursorEvent @event)
        {
            label.SetText("type key");
            activated = true;
        }

        public void OnReleased(VirtualCursorEvent @event)
        {
            
        }

        public void OnDrag(VirtualCursorEvent @event)
        {
        }

        public void OnHover()
        {
        }

        public void OnCursorEnter(VirtualCursorEvent @event)
        {
            
        }

        public void OnCursorExit(VirtualCursorEvent @event)
        {
            activated = false;
        }
    }
}