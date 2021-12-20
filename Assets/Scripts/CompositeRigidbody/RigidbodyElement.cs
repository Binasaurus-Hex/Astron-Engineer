using System;
using UnityEngine;

namespace CompositeRigidbody {
    public class RigidbodyElement : MonoBehaviour
    {
        public CompositeRigidbody connectedBody;
        private bool connected;
        public float mass;

        private void Start() {
            transform.IterateParents(t => t.TryGetComponent<CompositeRigidbody>(out connectedBody));
            if (connectedBody == null) {
                return;
            }
            connectedBody.Join(this);
            connected = true;
        }

        public void AddForce(Vector3 force) {
            if (!connected) return;
            connectedBody.body.AddForceAtPosition(force,transform.position);
        }

        private void OnDrawGizmos() {
        }
    }
}