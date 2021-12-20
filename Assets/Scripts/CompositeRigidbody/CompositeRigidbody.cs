using System;
using UnityEngine;

namespace CompositeRigidbody {
	public class CompositeRigidbody : MonoBehaviour
	{
		[HideInInspector]
		public Rigidbody body;

		private void Awake()
		{
			body = GetComponent<Rigidbody>();
		}

		public void Join(RigidbodyElement element)
		{
			float totalMass = body.mass + element.mass;
			Vector3 relativePosition = element.transform.position - body.position;
			Vector3 updatedCentreOfMass = (body.centerOfMass * body.mass + relativePosition * element.mass) / totalMass;
			body.centerOfMass = updatedCentreOfMass;
			body.mass = totalMass;
		}
	}
}