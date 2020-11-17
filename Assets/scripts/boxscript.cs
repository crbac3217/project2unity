using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxscript : MonoBehaviour
{
    private GameObject drawarea;
    public GameObject mcamera;
    public GameObject player;
    public GameObject brokenpref;
    public GameObject thoughtbubblepref;
    public GameObject spawnedbb;
    public bool inter = false;
    bool breakb = false;
    Animator myanim;
    // Start is called before the first frame update
    void Start()
    {
        myanim = gameObject.GetComponent<Animator>();
        drawarea = gameObject.transform.GetChild(0).gameObject;
        mcamera = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        spawnedbb = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        var bubblespawn = player.transform.GetChild(0);
        var spwnpos = bubblespawn.transform.position;
        spawnedbb.transform.position = spwnpos;
        if ((mcamera.GetComponent<Demo>().recogest == "scratch") && (inter == true))
        {
            player.GetComponent<playermovement>().enabled = true;
            player.GetComponent<SpriteRenderer>().enabled = true;
            myanim.SetTrigger("destroy");
            inter = false;
            breakb = true;
        if (breakb == true)
            {
                StartCoroutine(Boxbreak());
            }
        }
    }
    IEnumerator Boxbreak()
    {
        Destroy(spawnedbb);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        Instantiate(brokenpref);
        yield return new WaitForSeconds(3f);
        Destroy(brokenpref);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.gameObject.tag == "Player")
            {
                myanim.SetTrigger("close");
                drawarea.SetActive(true);
                inter = true;
                player.GetComponent<SpriteRenderer>().enabled = false;
                player.GetComponent<playermovement>().enabled = false;
                spawnedbb = Instantiate(thoughtbubblepref);
            }
        }
    }
}
