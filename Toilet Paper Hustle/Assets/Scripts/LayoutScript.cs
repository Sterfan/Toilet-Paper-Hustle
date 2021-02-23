using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutScript : MonoBehaviour
{
    [SerializeField]
    float xCenter = 0;
    [SerializeField]
    float yCenter = 0;
    [SerializeField]
    float middleDistance = 2;
    [SerializeField]
    float b = 2;
    [SerializeField]
    float xPos;
    [SerializeField]
    float yPos;

    [SerializeField]
    GameObject note1 = null;
    [SerializeField]
    GameObject note2 = null;

    [SerializeField]
    float screenWidth = 5;




    // Start is called before the first frame update
    void Start()
    {
        for (float i = xCenter - 10; i < 10; i += 0.05f)
        {
            F(i);
            if (xPos < xCenter && xPos > -screenWidth)
            {
                Instantiate(note1, new Vector3(xPos, yPos, 1), Quaternion.identity);
            }
            else if (xPos > xCenter && xPos < screenWidth)
            {
                Instantiate(note2, new Vector3(xPos, yPos, 1), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void F(float t)
    {
        xPos = x(t);
        yPos = y(t);
    }

    float x(float t)
    {
        return middleDistance * (1 / Mathf.Cos(t)) + xCenter;
    }
    float y(float t)
    {
        return b * Mathf.Tan(t) + yCenter;
    }
}
