using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxscript : MonoBehaviour
{
    private GameObject drawarea;
    public GameObject mcamera;
    public GameObject player;
    public GameObject brokenpref;
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
    }

    // Update is called once per frame
    void Update()
    {
        if ((mcamera.GetComponent<Demo>().recogest == "scratch") && (inter == true))
        {
            player.GetComponent<playermovement>().enabled = true;
            player.GetComponent<playermovement>().think = false;
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
        yield return new WaitForSeconds(myanim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
        Instantiate(brokenpref, this.gameObject.transform);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.gameObject.tag == "Player")
            {
                myanim.SetTrigger("close");
                drawarea.SetActive(true);
                inter = true;
                player.GetComponent<playermovement>().Thinkanim();
                player.GetComponent<playermovement>().enabled = false;
            }
        }
    }
}
