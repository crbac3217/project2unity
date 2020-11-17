using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgendscript : MonoBehaviour
{
    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && parent.GetComponent<bridgescript>().spawned == true)
        {
            parent.GetComponent<bridgescript>().inter = false;
        }
    }
}
