using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedVoice : MonoBehaviour
{
    public bool grabbed = false;
    public int timesToPlay = 1;
    public int probabilityToPlay = 1;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbed)
        {
            timer += Time.deltaTime;

        }
    }
}
