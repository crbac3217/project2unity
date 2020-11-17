using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgescript : MonoBehaviour
{
    public GameObject mcamera;
    public GameObject player;
    public bool inter = true;
    public bool spawned = false;
    public bool check = false;
    private GameObject drawarea;
    private Vector3 spawnvec;
    public GameObject bridgebubblepref;
    public GameObject spawnedbb;
    // Start is called before the first frame update
    void Start()
    {
        drawarea = gameObject.transform.GetChild(0).gameObject;
        spawnvec = gameObject.transform.position;
        mcamera = GameObject.Find("Main Camera");
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
            if ((mcamera.GetComponent<Demo>().recogest == "bridge") && spawned == false && check == true)
        {
            Destroy(spawnedbb);
            mcamera.GetComponent<Demo>().instantobj(spawnvec);
            player.GetComponent<playermovement>().enabled = true;
            player.GetComponent<playermovement>().think = false;
            spawned = true;
            StartCoroutine(Tsecreset());
        }
        if (inter == false)
        {
            Destroy(gameObject);
        }  
    }
    IEnumerator Tsecreset()
    {
        yield return new WaitForSeconds(3.0f);
        spawned = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "Player")&&check == false)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            spawnedbb = Instantiate(bridgebubblepref);
            player.GetComponent<playermovement>().Thinkanim();
            collision.gameObject.GetComponent<playermovement>().enabled = false;
            drawarea.SetActive(true);
            check = true;
        }
    }
}
