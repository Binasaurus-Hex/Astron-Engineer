using UnityEngine;

namespace DataObjects.Thrusters {
	[CreateAssetMenu(menuName = "DataObjects/FunctionalComponents/ThrusterData")]
	public class ThrusterData : ScriptableObject {
		public GameObject prefab;
		public int size;
		public float maxThrust;
	}
}