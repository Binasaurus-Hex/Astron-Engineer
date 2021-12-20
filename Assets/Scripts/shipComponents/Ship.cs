using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SaveLoad;
using shipComponents;


public class Ship : MonoBehaviour {

    public FunctionalComponent[] components;

    private void Awake() {
        components = GetComponentsInChildren<FunctionalComponent>();
    }
}
