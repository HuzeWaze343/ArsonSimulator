using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    [SerializeField]
    bool lockYToZero = false;
    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cameraDir = cam.forward;
        if (lockYToZero) cameraDir.y = 0;
        transform.LookAt(transform.position + cameraDir);
    }
}
