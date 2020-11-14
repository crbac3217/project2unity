using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxscript : MonoBehaviour
{
    private GameObject drawarea;
    public GameObject mcamera;
    public GameObject player;
    public bool inter = false;
    // Start is called before the first frame update
    void Start()
    {
        drawarea = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if ((mcamera.GetComponent<Demo>().recogest == "scratch") && (inter == true))
        {
            player.GetComponent<playermovement>().enabled = true;
            Destroy(gameObject);
            inter = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.gameObject.tag == "Player")
            {
                drawarea.SetActive(true);
                inter = true;
            }
            player.GetComponent<playermovement>().enabled = false;
        }
    }
}
