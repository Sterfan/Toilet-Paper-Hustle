using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneSequence : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject Cam2;
    public GameObject Cam3;

    public SpaceShipRB handScript;
    public GameObject mainCamera;

    //public Gameobject PlayerCam;

    // Start is called before the first frame update
    void Start()
    {
        handScript.enabled = false;
        handScript.gameObject.GetComponent<Collider>().enabled = false;
        mainCamera.SetActive(false);
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
        yield return new WaitForSeconds(5);
        handScript.enabled = true;
        mainCamera.SetActive(true);
        handScript.gameObject.GetComponent<Collider>().enabled = true;
        Cam3.SetActive(false);
    }
  
   
}
