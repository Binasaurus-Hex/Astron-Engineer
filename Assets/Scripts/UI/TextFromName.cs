using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextFromName : MonoBehaviour
{
    private Text _text;
    void Start()
    {
        _text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = name;
    }
}
