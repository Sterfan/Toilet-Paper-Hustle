using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingLimb : MonoBehaviour
{
    public KeyCode theKey;
    public bool countingUp;
    public int count;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(theKey))
        {
            if(countingUp == true)
            {
                count += 1;
                anim.SetInteger("count", count);
            }
            else
            {
                count -= 1;
                anim.SetInteger("count", count);
            }
        }
      if(count == 4)
        {
            countingUp = false;
        }  
      if(count == 0)
        {
            countingUp = true;
        }
    }

    public void MakeManDance()
    {
        if (countingUp == true)
        {
            count += 1;
            anim.SetInteger("count", count);
        }
        else
        {
            count -= 1;
            anim.SetInteger("count", count);
        }
    }
}
