using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementutorialscript : MonoBehaviour
{
    public GameObject player;
    public GameObject movetutopref;
    public GameObject spawnedbb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        spawnedbb = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        var bubblespawn = player.transform.GetChild(0);
        var spwnpos = bubblespawn.transform.position;
        spawnedbb.transform.position = spwnpos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            spawnedbb = Instantiate(movetutopref);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(spawnedbb);
        }
    }
}
