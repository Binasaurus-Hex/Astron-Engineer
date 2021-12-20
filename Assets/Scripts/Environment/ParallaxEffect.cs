using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private ScrollTexture scrollTexture;
    public float parallax = 0;
    void Start()
    {
        scrollTexture = GetComponent<ScrollTexture>();
    }

    // Update is called once per frame
    void Update()
    {
        float parallaxMovement = 1 - parallax;
        scrollTexture.offset = new Vector2(transform.position.x * parallaxMovement,transform.position.z * parallaxMovement);
    }
}
