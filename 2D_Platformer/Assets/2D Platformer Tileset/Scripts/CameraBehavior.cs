using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 camOffset = new Vector3(0f,0f,-10f);

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Main-Character").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.TransformPoint(camOffset);

        this.transform.LookAt(target);
        // this.transform.position.z = mainChar.transform.position.z + 2;
        // this.transform.position = mainChar.transform.position;
    }
}
