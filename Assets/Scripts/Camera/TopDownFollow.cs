using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownFollow : MonoBehaviour {
    public GameObject target;

    public float distance = 200;
    // Start is called before the first frame update
    void Start() {
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        Vector3 targetPos = target.transform.position;
        transform.position = new Vector3(targetPos.x,targetPos.y+distance,targetPos.z);
    }
}
