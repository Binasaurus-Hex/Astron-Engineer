using System;
using UnityEngine;

namespace SaveLoad {
	public static class SaveManager {
		public delegate void Message();
		public static event Message onSave;
		public static event Message onLoad;
		public static event Message onLoadEnd;

		public static void Save() {
			onSave?.Invoke();
		}

		public static void Load() {
			onLoad?.Invoke();
			onLoadEnd?.Invoke();
		}
		
	}
}