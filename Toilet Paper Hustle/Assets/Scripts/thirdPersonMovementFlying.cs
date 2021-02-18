using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonMovementFlying : MonoBehaviour
{
    public float forwardSpeed = 25f;
    public float strafeSpeed = 10f;
    public float hoverSpeed = 5f;

    private float
        activeForwardSpeed,
        activeStrafeSpeed,
        activeHoverSpeed;


    // Update is called once per frame
    void Update()
    {
        activeForwardSpeed = Input.GetAxisRaw("Vertical") * forwardSpeed;
        activeStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        activeHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;

        transform.position += transform.forward
    }
}
