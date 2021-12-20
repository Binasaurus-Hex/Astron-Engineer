using System.Collections;
using System.Collections.Generic;
using Nodes.ComponentReading;
using UnityEngine;

public class MainUI : MonoBehaviour {
    private Canvas canvas;

    private bool showing;
    // Start is called before the first frame update
    void Start() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            showing ^= true;
            canvas.enabled = showing;
        }
    }
}
