using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombscript : MonoBehaviour
{
    public GameObject player;
    public GameObject mcamera;
    public GameObject bombpref;
    private GameObject drawarea;
    public GameObject bombbubblepref;
    public GameObject spawnedbb;
    public bool inter = false;
    public bool check = false;
    // Start is called before the first frame update
    void Start()
    {
        mcamera = GameObject.Find("Main Camera");
        drawarea = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedbb != null)
        {
            var bubblespawn = player.transform.GetChild(0);
            var spwnpos = bubblespawn.transform.position;
            spawnedbb.transform.position = spwnpos;
        }
        if (mcamera.GetComponent<Demo>().recogest == "bomb" && inter == false && check == true)
        {
            Destroy(spawnedbb);
            mcamera.GetComponent<Demo>().getbombpos(gameObject);
            player.GetComponent<playermovement>().enabled = true;
            player.GetComponent<playermovement>().think = false;
            Destroy(gameObject);
        }   
    }
    public void bombinst(Vector3 avpoint)
    {
        if (inter == false && check == true)
        {
            avpoint.z = player.transform.position.z;
            var newbomb = Instantiate(bombpref);
            newbomb.transform.position = avpoint;
            newbomb.transform.name = "newbomb";
            inter = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            spawnedbb = Instantiate(bombbubblepref);
            player.GetComponent<playermovement>().Thinkanim();
            player.GetComponent<playermovement>().enabled = false;
            drawarea.SetActive(true);
            check = true;
        }
    }
}
