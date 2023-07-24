using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int cameraSize = 5;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 2f, -10f);
    //private Vector3 cameraOffsetFix;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    cameraOffsetFix = cam.transform.position - transform.position;
    //    cameraOffsetFix += new Vector3(cameraOffset.x, cameraOffset.y, 0f);
    //}

    // Update is called once per frame
    void Update()
    {
        //cam.transform.position = transform.position + cameraOffsetFix;
        cam.transform.position = transform.position + cameraOffset;
    }
}
