using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public GameObject maincamera;
    public GameObject player;
    public float trspeed = 3;
    GameObject posmain;
    GameObject postitle;
    GameObject posflip;
    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.Find("Main Camera");
        posmain = GameObject.Find("Cameraposmain");
        postitle = GameObject.Find("Camerapostitle");
        posflip = GameObject.Find("Cameraposflip");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Movetoflip()
    {
        while (maincamera.transform.position != posmain.transform.position)
        {
            maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, posflip.transform.position, trspeed * Time.deltaTime);
            maincamera.transform.rotation = Quaternion.Lerp(maincamera.transform.rotation, posflip.transform.rotation, trspeed * Time.deltaTime);
        }
    }
}
