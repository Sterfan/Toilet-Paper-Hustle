using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halfer : MonoBehaviour
{
    [SerializeField]
    GameObject firstCircle = null;
    [SerializeField]
    GameObject secondCircle = null;

    [SerializeField]
    float changePos = 0.25f;
    Vector2 position;

    float slope;
    float perpendicularSlope;

    float x1;
    float x2;
    float y1;
    float y2;

    // Start is called before the first frame update
    void Start()
    {
        x1 = firstCircle.transform.position.x;
        x2 = secondCircle.transform.position.x;
        y1 = firstCircle.transform.position.y;
        y2 = secondCircle.transform.position.y;
        slope = (y1 - y2) / (x1 - x2);
        perpendicularSlope = -1 / slope;

        position = new Vector2((firstCircle.transform.position.x + secondCircle.transform.position.x) / 2, (firstCircle.transform.position.y + secondCircle.transform.position.y) / 2);
        gameObject.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            position += new Vector2(changePos, perpendicularSlope * changePos);
            gameObject.transform.position = position;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            position -= new Vector2(changePos, perpendicularSlope * changePos);
            gameObject.transform.position = position;
        }
    }
}
