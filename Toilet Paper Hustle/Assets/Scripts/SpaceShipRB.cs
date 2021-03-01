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

    public AudioManager audioManager;

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

    bool DDLR = false, frozen = false;

    float lookAcceleration = 10f;

    float manboyCooldown = 5f, manboyTimer = 0f;
    int manboyProb = 30;

    [HideInInspector]
    public float stability = 0.3f, speed = 2.0f;

    bool stopRotation = false;

    public int maxMass = 40;


    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        SetBaseConstraints();
    }

    // Update is called once per frame
    void Update()
    {
        if (!DDLR)
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
            if (!stopRotation)
            {
                Vector3 currentRotation = rb.rotation.eulerAngles;
                currentRotation.x = Mathf.Clamp((currentRotation.x <= 180) ? currentRotation.x : -(360 - currentRotation.x), -maxAngle, maxAngle);
                rb.MoveRotation(Quaternion.Euler(currentRotation.x + mouseDistance.y * lookRotateSpeed * Time.deltaTime, currentRotation.y + mouseDistance.x * lookRotateSpeed * Time.deltaTime, 0f));
            }

            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
            activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

            //transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
            //transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
            //transform.position += transform.up * activeHoverSpeed * Time.deltaTime;



            Interact();
            CheckForDDLR();
        }

        if (DDLR)
        {
            mouseDistance = new Vector2(0f, 0f);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseDDLR();
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            OpenDDLR();
            Debug.Log("Opened");
        }
    }


    private void FixedUpdate()
    {
        if (!DDLR)
        {
            ////Vector3 yaw = transform.up * mouseDistance.x * lookRotateSpeed;
            ////Vector3 pitch = transform.right * mouseDistance.y * lookRotateSpeed;
            ////rb.AddTorque(yaw + pitch, ForceMode.Acceleration);
            //float zRotation = Mathf.Deg2Rad * currentRotation.x;
            //Vector3 currentTorque = transform.InverseTransformDirection(rb.angularVelocity);
            //currentTorque.x = Mathf.MoveTowards(currentTorque.x, mouseDistance.y * lookRotateSpeed, lookAcceleration * Time.deltaTime);
            //currentTorque.y = Mathf.MoveTowards(currentTorque.y, mouseDistance.x * lookRotateSpeed, lookAcceleration * Time.deltaTime);
            ////currentTorque.z = Mathf.MoveTowards(currentTorque.z, 0f, lookAcceleration * Time.deltaTime);
            ////currentTorque.z = 0f;
            ////currentTorque.z = FixZRotation();
            //currentTorque = transform.TransformDirection(currentTorque);
            //rb.angularVelocity = currentTorque;
            //FixZRotation();
            rb.AddForce(-transform.forward.normalized * activeForwardSpeed, ForceMode.Acceleration);
            rb.AddForce(-transform.right.normalized * activeStrafeSpeed, ForceMode.Acceleration);
            //rb.AddForce(transform.up.normalized * activeHoverSpeed, ForceMode.Acceleration);
            rb.AddForce(new Vector3(0f, 1f, 0f) * activeHoverSpeed, ForceMode.Acceleration);

            CheckIfManboy();

        }
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
        
        joint = pickUp.AddComponent<FixedJoint>();
        joint.connectedBody = rb;
        if (pickUp.CompareTag("Door"))
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        if (pickUp.GetComponent<Rigidbody>().mass >= maxMass)
        {
            stopRotation = true;
        }
        if (pickUp.GetComponent<AudioCue>())
        {
            pickUp.GetComponent<AudioCue>().pickedUp = true;
        }
    }

    void Drop()
    {
        //pickedUpObject.GetComponent<Rigidbody>().useGravity = true;
        //pickedUpObject.GetComponent<Rigidbody>().isKinematic = false;
        //pickedUpObject.transform.SetParent(null);
        //pickedUpObject = null;
        if (pickedUpObject.CompareTag("Hangable"))
        {
            pickedUpObject.GetComponent<Hangable>().dropped = true;
        }
        if (pickedUpObject.GetComponent<AudioCue>())
        {
            pickedUpObject.GetComponent<AudioCue>().pickedUp = false;
        }
        SetBaseConstraints();
        Destroy(joint);
        animator.SetBool("Grab", false);
        stopRotation = false;
    }

    void CheckForDDLR()
    {
        if (pickedUpObject != null && pickedUpObject.tag == "DDLR")
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                OpenDDLR();
            }
        }
    }

    void FreezePosAndRot()
    {
        if (frozen)
        {
            SetBaseConstraints();
            if (pickedUpObject != null)
            {
                pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            return;
        }
        rb.constraints = RigidbodyConstraints.FreezeAll;
        pickedUpObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        frozen = true;
    }
    void OpenDDLR()
    {
        FreezePosAndRot();
        SceneManager.LoadScene("Axel's Scene", LoadSceneMode.Additive);
        DDLR = true;
    }

    private void CloseDDLR()
    {
        SceneManager.UnloadSceneAsync(1);
        FreezePosAndRot();
        DDLR = false;
        Drop();
        audioManager.PlaySound("Screw this game");
    }

    void FixZRotation()
    {
        //var delta = Quaternion.Euler(0, 0, 0) * Quaternion.Inverse(rb.rotation);
        //float angle; Vector3 axis;
        //delta.ToAngleAxis(out angle, out axis);
        //if (float.IsInfinity(axis.x))
        //    return 0;
        //if (angle > 180f)
        //    angle -= 360f;
        //Vector3 angular = (0.9f * Mathf.Deg2Rad * angle / 4) * axis.normalized;
        //return angular.z;
        Vector3 predictedUp = Quaternion.AngleAxis(
         rb.angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed,
         rb.angularVelocity) * transform.up;

        Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
        torqueVector = Vector3.Project(torqueVector, transform.forward);
        rb.AddTorque(torqueVector * speed * speed);
    }

    void SetBaseConstraints()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
    }

    void CheckIfManboy()
    {
        manboyTimer += Time.deltaTime;
        manboyCooldown += Time.deltaTime;
        if (manboyTimer > 2f)
        {
            if (manboyCooldown > 10f)
            {
                Debug.Log("Checked");
                int rand = Random.Range(1, manboyProb + 1);
                if (rand == 1)
                {
                    audioManager.PlaySound("Manboy");
                    manboyCooldown = 0f;
                    manboyProb *= 2;
                }
            }

            manboyTimer = 0f;
        }
    }


}
