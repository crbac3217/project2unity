using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgescript : MonoBehaviour
{
    public GameObject mcamera;
    public GameObject player;
    public bool inter = true;
    public bool spawned = false;
    private bool check = false;
    private GameObject drawarea;
    private Vector3 spawnvec;
    // Start is called before the first frame update
    void Start()
    {
        drawarea = gameObject.transform.GetChild(0).gameObject;
        spawnvec = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((mcamera.GetComponent<Demo>().recogest == "bridge") & spawned == false)
        {
            mcamera.GetComponent<Demo>().instantobj(spawnvec);
            player.GetComponent<playermovement>().enabled = true;
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
        yield return new WaitForSeconds(2.0f);
        spawned = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "Player")&&check == false)
        {
            collision.gameObject.GetComponent<playermovement>().enabled = false;
            drawarea.SetActive(true);
            check = true;
        }
    }
}
