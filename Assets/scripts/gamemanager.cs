using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    GameObject maincamera;
    public GameObject player;
    public GameObject leftpage;
    public GameObject rightpage;
    public GameObject endlogo;
    public GameObject endcat;
    public float trspeed = 3;
    public Texture2D cursortex;
    public CursorMode cursormode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;
    bool movetomain = false;
    bool begin = false;
    bool camerabegin = false;
    bool flipend = false;
    bool toend = false;
    bool drop = false;
    bool finale = false;
    int objcount = 0;
    bool spwnprf = false;
    private List<Sprite> spritelist = new List<Sprite>();
    private List<GameObject> prefabs = new List<GameObject>();
    GameObject posend;
    GameObject posmain;
    GameObject posflip;
    GameObject posendobj;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] prefs = Resources.LoadAll<GameObject>("Assets/prefabs/instpref");
        foreach (GameObject i in prefs)
        {
            prefabs.Add(i);
        }
        Sprite[] sprites = Resources.LoadAll<Sprite>("Assets/Tiles");
        foreach (Sprite i in sprites)
        {
            spritelist.Add(i);
        }
        posendobj = GameObject.Find("endobjspawn");
        maincamera = GameObject.Find("Main Camera");
        posmain = GameObject.Find("Cameraposmain");
        posflip = GameObject.Find("Cameraposflip");
        posend = GameObject.Find("Cameraposend");
    }

    // Update is called once per frame
    void Update()
    {
        if (finale == true)
        {
            Theend();
        }
        if (drop == true)
        {
            Dropobj();
        }
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
    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursortex, hotspot, cursormode);
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursormode);
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
            drop = true;
        }
    }
    void Dropobj()
    {
        var spawnpoint = posendobj.transform.position;
        spawnpoint.x = Random.Range(-16, 16);
        if (spwnprf == false)
        {
            var tempobj = Instantiate(new GameObject("nonpref"));
            var spren = tempobj.AddComponent<SpriteRenderer>();
            int spritenum = Random.Range(0, spritelist.Count);
            spren.sprite = spritelist[spritenum];
            tempobj.AddComponent<Rigidbody2D>();
            tempobj.AddComponent<BoxCollider2D>();
            tempobj.transform.position = spawnpoint;
            tempobj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }else if (spwnprf == true)
        {
            int prefnum = Random.Range(0, prefabs.Count);
            var tempobj = Instantiate(prefabs[prefnum]);
            tempobj.transform.position = spawnpoint;
            tempobj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        objcount++;
        if(objcount == 8)
        {
            finale = true;
            drop = false;
        }
        if (objcount == 20)
        {
            drop = false;
        }
        StartCoroutine(Sec());
    }
    void Theend()
    {
        StartCoroutine(Sec());
        drop = true;
        finale = false;
    }

    private IEnumerator Sec()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
