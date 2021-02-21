using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    Vector3 mousePos;

    Camera cam;

    public float moveSpeed = 10;

    Rigidbody m_Rigidbody;

    Vector3 m_EulerAngleVelocity;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_EulerAngleVelocity = new Vector3(0, 100, 0);

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        m_Rigidbody.position = new Vector3(mousePos.x, mousePos.y, m_Rigidbody.position.z);

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            m_Rigidbody.velocity = transform.forward * moveSpeed;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            m_Rigidbody.velocity = -transform.forward * moveSpeed;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(-m_EulerAngleVelocity * Time.deltaTime);
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        }
    }
}
