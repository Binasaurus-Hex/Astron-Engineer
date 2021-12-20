using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ScrollTexture : MonoBehaviour
{
    private Material texture;
    private bool initialized;
    public Vector2 offset;
    private Vector2 scale;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        texture = GetComponent<MeshRenderer>().material;
        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized) Init();
        texture.SetVector("_offset", offset/transform.localScale);
    }
}
