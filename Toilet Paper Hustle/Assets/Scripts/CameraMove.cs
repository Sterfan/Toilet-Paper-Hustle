using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    private new Transform camera;

    public float posSpeed = 1.0F;
    public float rotSpeed = 1.0F;

    void Start()
    {
        camera = GetComponent<Transform>();
    }

    void Update()
    {
        // position movement
        camera.position = Vector3.Lerp(camera.position, target.position, (posSpeed * Time.deltaTime));

        // rotation movement
        camera.rotation = Quaternion.Lerp(camera.rotation, target.rotation, (rotSpeed * Time.deltaTime));
        //camera.rotation = target.rotation;
    }
}
