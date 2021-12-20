using UnityEngine;
using System.Collections;

public class Information : Medium {
	private float value;
	public Information(float value) {
		this.value = value;
	}

	public float GetValue() {
		return value;
	}
}
