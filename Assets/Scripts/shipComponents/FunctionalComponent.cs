using ModularContainers;
using Saving;
using UnityEditor;
using UnityEngine;

namespace shipComponents {
	public abstract class FunctionalComponent : MonoBehaviour,ISavable{
		public abstract void Load(string obj);
		public abstract string Save();
	}
}