using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startbutton : MonoBehaviour
{
    GameObject gamemanager;
    Animator myanim;
    public bool mflip = false;
    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("gamemanager");
        myanim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == "button" && Input.GetMouseButtonDown(0))
            {
                myanim.SetBool("button", true);
                mflip = true;
            }
        }
        if (mflip == true)
        {
            gamemanager.GetComponent<gamemanager>().Movetoflip();
        }
    }
}
