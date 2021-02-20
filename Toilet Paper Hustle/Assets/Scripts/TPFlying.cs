using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPFlying : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    float camRotation;

    private void Start()
    {
        camRotation = cam.rotation.eulerAngles.x;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angleY = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY, ref turnSmoothVelocity, turnSmoothTime);

            float targetAngleX = cam.eulerAngles.x - camRotation;
            float angleX = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleX, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(angleX, angleY, 0f);

            Vector3 moveDir = Quaternion.Euler(targetAngleX, targetAngleY, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
