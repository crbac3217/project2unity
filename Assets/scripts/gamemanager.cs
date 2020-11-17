using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    GameObject maincamera;
    public GameObject player;
    public GameObject leftpage;
    public GameObject rightpage;
    public float trspeed = 3;
    bool movetomain = false;
    bool begin = false;
    bool camerabegin = false;
    bool flipend = false;
    bool toend = false;
    GameObject posend;
    GameObject posmain;
    GameObject posflip;
    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.Find("Main Camera");
        posmain = GameObject.Find("Cameraposmain");
        posflip = GameObject.Find("Cameraposflip");
        posend = GameObject.Find("Cameraposend");
    }

    // Update is called once per frame
    void Update()
    {
        if (movetomain == true)
        {
            Movetomain();
        }
        if (begin == true)
        {
            Startgame();
        }
        if (flipend == true)
        {
            Moveflipend();
        }
        if (toend == true)
        {
            Movetoend();
        }
    }
    private void LateUpdate()
    {
        if (camerabegin == true)
        {
            Startcam();
        }
    }
    public void Movetoflip()
    {
        if (maincamera.transform.position != posflip.transform.position) 
        {
            maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, posflip.transform.position, trspeed * Time.deltaTime);
            maincamera.transform.rotation = Quaternion.Lerp(maincamera.transform.rotation, posflip.transform.rotation, trspeed * Time.deltaTime);
        }
        if (posflip.transform.position.x - maincamera.transform.position.x > -0.1)
        {
            GameObject.Find("button").GetComponent<startbutton>().mflip = false;
            leftpage.GetComponent<Animator>().SetBool("openpage", true);
            movetomain = true;
        }
    }
    public void Movetomain()
    {
        if (maincamera.transform.position != posmain.transform.position)
        {
            maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, posmain.transform.position, trspeed/3 * Time.deltaTime);
            maincamera.transform.rotation = Quaternion.Lerp(maincamera.transform.rotation, posmain.transform.rotation, trspeed/3 * Time.deltaTime);
        }
        if (posmain.transform.position.x - maincamera.transform.position.x > -0.1)
        {
            movetomain = false;
            begin = true;
        }
    }
    public void Startgame()
    {
        begin = false;
        var playpref = Instantiate(player);
        playpref.transform.position = GameObject.Find("playerpos").transform.position;
        playpref.transform.localScale = GameObject.Find("playerpos").transform.localScale;
        camerabegin = true;
    }
    void Startcam()
    {
        camerabegin = false;
        maincamera.GetComponent<Demo>().enabled = true;
        maincamera.GetComponent<cameramove>().enabled = true;
    }
    public void Gameend()
    {
        Destroy(GameObject.Find("instantiatedobjs"));
        maincamera.GetComponent<Demo>().enabled = false;
        maincamera.GetComponent<cameramove>().enabled = false;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        flipend = true;
    }
    void Moveflipend()
    {
            maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, posflip.transform.position, trspeed * Time.deltaTime);
            maincamera.transform.rotation = Quaternion.Lerp(maincamera.transform.rotation, posflip.transform.rotation, trspeed * Time.deltaTime);
        if (posflip.transform.position.x - maincamera.transform.position.x > -0.1)
        {
            rightpage.GetComponent<Animator>().SetBool("closepage", true);
        }
        if (posflip.transform.position.x - maincamera.transform.position.x > -0.01)
        {
            flipend = false;
            toend = true;
        }
    }
    void Movetoend()
    {
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, posend.transform.position, trspeed * Time.deltaTime);
        maincamera.transform.rotation = Quaternion.Lerp(maincamera.transform.rotation, posend.transform.rotation, trspeed * Time.deltaTime);
        if (posend.transform.position.x - maincamera.transform.position.x < -0.1)
        {
            toend = false;
        }
    }
}
