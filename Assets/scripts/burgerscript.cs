using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burgerscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.gameObject.tag == "Player")
            {
                GameObject.Find("gamemanager").GetComponent<gamemanager>().Gameend();
                StartCoroutine(Sec());
            }
        }
    }
    IEnumerator Sec()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("burger", true);
        yield return new WaitForSeconds(1.5f);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(gameObject);
    }
}
