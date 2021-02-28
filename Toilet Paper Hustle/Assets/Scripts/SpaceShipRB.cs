using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShipRB : MonoBehaviour
{
    public float 
        forwardSpeed = 25f,
        strafeSpeed = 10f, 
        hoverSpeed = 5f;

    public float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float
        activeForwardSpeed,
        activeStrafeSpeed,
        activeHoverSpeed;

    private float
        forwardAcceleration = 8f,
        strafeAcceleration = 8f,
        hoverAcceleration = 8f;

    private float rollInput;
    public float
        rollSpeed = 90f,
        rollAcceleration = 3.5f;

    float xRotation;

    public float maxAngle = 60f;

    public float deadZoneX = 80f, deadZoneY = 45f, slowSpeed = 0.5f;

    public float range = 5f;

    GameObject pickedUpObject;

    Rigidbody rb;

    Animator animator;

    FixedJoint joint;

    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        CheckMouseX();
        CheckMouseY();

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        //transform.Rotate(mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, 0f, Space.Self);




        //xRotation = transform.localEulerAngles.x;
        //xRotation = Mathf.Clamp((xRotation <= 180) ? xRotation : -(360 - xRotation), -maxAngle, maxAngle);
        //transform.localEulerAngles = new Vector3(xRotation, transform.localEulerAngles.y, 0f);



        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        //transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        //transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        //transform.position += transform.up * activeHoverSpeed * Time.deltaTime;



        Interact();

        if (Input.GetKeyDown(KeyCode.V))
        {
            OpenDDLR();
            Debug.Log("Opened");
        }
    }

    private void FixedUpdate()
    {
        Vector3 currentRotation = rb.rotation.eulerAngles;
        currentRotation.x = Mathf.Clamp((currentRotation.x <= 180) ? currentRotation.x : -(360 - currentRotation.x), -maxAngle, maxAngle);
        rb.MoveRotation(Quaternion.Euler(currentRotation.x + mouseDistance.y * lookRotateSpeed, currentRotation.y + mouseDistance.x * lookRotateSpeed, 0f));
        rb.AddForce(-transform.forward.normalized * activeForwardSpeed);
        rb.AddForce(-transform.right.normalized * activeStrafeSpeed);
        rb.AddForce(transform.up.normalized * activeHoverSpeed);
    }

    void CheckMouseX()
    {
        if (lookInput.x < screenCenter.x - deadZoneX)
        {
            mouseDistance.x = (lookInput.x - (screenCenter.x - deadZoneX)) / screenCenter.y;
        }
        else if (lookInput.x > screenCenter.x + deadZoneX)
        {
            mouseDistance.x = (lookInput.x - (screenCenter.x + deadZoneX)) / screenCenter.y;
        }
        else
        {
            mouseDistance.x = 0f;
        }
    }

    void CheckMouseY()
    {
        if (lookInput.y > screenCenter.y + deadZoneY)
        {
            mouseDistance.y = (lookInput.y - (screenCenter.y + deadZoneY)) / screenCenter.x;
        }
        else if (lookInput.y < screenCenter.y - deadZoneY)
        {
            mouseDistance.y = (lookInput.y - (screenCenter.y - deadZoneY)) / screenCenter.x;
        }
        else
        {
            mouseDistance.y = 0f;
        }
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("Grab", true);
            if (CheckIfObjectInteractable() != null)
            {
                pickedUpObject = CheckIfObjectInteractable();
                PickUp(pickedUpObject);
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("Grab", false);
            if (pickedUpObject != null)
            {
                Drop();
            }
        }

    }

    private GameObject CheckIfObjectInteractable()
    {
        RaycastHit raycastHit;
        Vector3 target = (transform.position + Camera.main.transform.forward * range);
        if (Physics.Linecast(transform.position, target, out raycastHit))
        {
            return raycastHit.collider.gameObject;
        }
        return null;
    }

    void PickUp(GameObject pickUp)
    {
        //pickUp.GetComponent<Rigidbody>().useGravity = false;
        //pickUp.GetComponent<Rigidbody>().isKinematic = true;
        //pickUp.transform.SetParent(transform);
        pickUp.AddComponent<FixedJoint>();
        joint = pickUp.GetComponent<FixedJoint>();
        joint.connectedBody = rb;
    }

    void Drop()
    {
        //pickedUpObject.GetComponent<Rigidbody>().useGravity = true;
        //pickedUpObject.GetComponent<Rigidbody>().isKinematic = false;
        //pickedUpObject.transform.SetParent(null);
        //pickedUpObject = null;
        Destroy(joint);
    }

    void OpenDDLR()
    {
        SceneManager.LoadScene("Axel's Scene", LoadSceneMode.Additive);
    }
}
