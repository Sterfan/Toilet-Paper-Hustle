using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divider : MonoBehaviour
{
    [SerializeField]
    GameObject firstCircle = null;
    [SerializeField]
    GameObject secondCircle = null;

    float x1;
    float x2;
    float y1;
    float y2;
    Vector2 position;

    [SerializeField]
    int numberOfSegments = 3;
    [SerializeField]
    int placeInSegments = 1;

    float slope;
    float perpendicularSlope;

    // Start is called before the first frame update
    void Start()
    {
        x1 = firstCircle.transform.position.x;
        x2 = secondCircle.transform.position.x;
        y1 = firstCircle.transform.position.y;
        y2 = secondCircle.transform.position.y;
        slope = (y1 - y2) / (x1 - x2);
        perpendicularSlope = -1 / slope;
        //position = new Vector2((firstCircle.transform.position.x + secondCircle.transform.position.x) * placeInSegments / numberOfSegments, (firstCircle.transform.position.y + secondCircle.transform.position.y) * placeInSegments / numberOfSegments);
    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector2((firstCircle.transform.position.x + secondCircle.transform.position.x) * placeInSegments / numberOfSegments, (firstCircle.transform.position.y + secondCircle.transform.position.y) * placeInSegments / numberOfSegments);
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = position;
        }
    }
}
