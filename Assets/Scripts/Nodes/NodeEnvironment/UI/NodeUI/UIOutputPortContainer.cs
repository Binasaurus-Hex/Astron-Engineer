using UnityEngine;

namespace Nodes.NodeEnvironment.UI.NodeUI {
    public class UIOutputPortContainer : MonoBehaviour
    {
        public UIOutputPort uiPort;
        public UILabel title;

        public void SetPort(OutputPort port,string name) {
            title.SetText(name);
            uiPort.SetOutputPort(port);
        }
    }
}
