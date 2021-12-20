using System;
using Nodes.NodeEnvironment.UI.NodeUI;
using UnityEngine;

namespace Nodes.NodeEnvironment.UI {
    public class UIInputPortContainer : MonoBehaviour {
        public UIInputPort uiPort;
        public UILabel title;

        public void SetPort(InputPort port,string name) {
            title.SetText(name);
            uiPort.SetInputPort(port);
        }
    }
}
