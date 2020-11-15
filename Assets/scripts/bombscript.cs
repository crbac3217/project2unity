using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombscript : MonoBehaviour
{
    public GameObject player;
    public GameObject mcamera;
    public GameObject bombpref;
    private GameObject drawarea;
    private Vector3 spawnvec;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mcamera = GameObject.Find("Main Camera");
        drawarea = gameObject.transform.GetChild(0).gameObject;
        spawnvec = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
     if (mcamera.GetComponent<Demo>().recogest == "bomb")
        {
            mcamera.GetComponent<Demo>().getbombpos(gameObject);
            player.GetComponent<playermovement>().enabled = true;
            Destroy(gameObject);
        }   
    }
    public void bombinst(Vector3 avpoint)
    {
        avpoint.z = player.transform.position.z;
        var newbomb = Instantiate(bombpref);
        newbomb.transform.position = avpoint;
        newbomb.transform.name = "newbomb";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.GetComponent<playermovement>().enabled = false;
            drawarea.SetActive(true);
        }
    }
}
