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

    float camY;

    public float turnSpeed;

    public float distanceFromCam = 4f;


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
        Debug.Log(mousePos);

        //m_Rigidbody.position = mousePos + m_Rigidbody.transform.forward.normalized * distanceFromCam;
        m_Rigidbody.MovePosition(mousePos + m_Rigidbody.transform.forward.normalized * distanceFromCam);

        //m_Rigidbody.position = new Vector3(mousePos.x, mousePos.y, m_Rigidbody.position.z);

        //if (Input.GetAxisRaw("Horizontal") == 0)
        //{
        //    m_Rigidbody.position = new Vector3(mousePos.x, mousePos.y, m_Rigidbody.position.z);
        //}
        //else
        //{
        //    m_Rigidbody.position = m_Rigidbody.position;
        //}

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            cam.transform.position += new Vector3(cam.transform.forward.normalized.x, 0f, cam.transform.forward.normalized.z) * moveSpeed * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            //m_Rigidbody.velocity = -transform.forward * moveSpeed;
            cam.transform.position -= new Vector3(cam.transform.forward.normalized.x, 0f, cam.transform.forward.normalized.z) * moveSpeed * Time.deltaTime;

        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
            //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
            cam.transform.RotateAround(transform.position, new Vector3(0, 1, 0), turnSpeed * Time.deltaTime);

        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //Quaternion deltaRotation = Quaternion.Euler(-m_EulerAngleVelocity * Time.deltaTime);
            //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
            cam.transform.RotateAround(transform.position, new Vector3(0, 1, 0), -turnSpeed * Time.deltaTime);
        }
        camY = cam.transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, camY, transform.eulerAngles.z);

    }

}
