using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speedH = 10.0f;
    public float speedV = 10.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 Vec = transform.localPosition;
        Vec.y += Input.GetAxis("Jump") * Time.deltaTime * 40;
        Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * 40;
        Vec.z += Input.GetAxis("Vertical") * Time.deltaTime * 40;
        transform.localPosition = Vec;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}
