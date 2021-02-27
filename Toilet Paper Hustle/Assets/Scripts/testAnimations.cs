using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAnimations : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)){
            animator.SetBool("Grab", true);
        }
        if (Input.GetKeyDown(KeyCode.Y)){
            animator.SetBool("Point", true);
            animator.SetBool("Grab", false);
        }


    }
}
