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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            spawnedbb = Instantiate(movetutopref);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            player = GameObject.FindGameObjectWithTag("Player");
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
