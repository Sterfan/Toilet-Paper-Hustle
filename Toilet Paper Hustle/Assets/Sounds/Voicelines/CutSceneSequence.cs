using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneSequence : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject Cam2;
    public GameObject Cam3;

    //public Gameobject PlayerCam;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence()
    {
        yield return new WaitForSeconds(8);
        Cam2.SetActive(true);
        Cam1.SetActive(false);
        yield return new WaitForSeconds(6);
        Cam3.SetActive(true);
        Cam2.SetActive(false);
       // yield return new WaitForSeconds(7);
        //Turn on playerstuff
        //Cam3.SetActive(false);
    }
  
   
}
