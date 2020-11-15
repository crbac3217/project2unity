using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour
{
    public GameObject player;
    Vector3 offset;
    public Vector3 newpos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        newpos = new Vector3();
        offset = gameObject.transform.position - player.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newpos = player.gameObject.transform.position + offset;
    }
    private void LateUpdate()
    {
        Checkcol();
        Movecam();
    }
    void Checkcol()
    {
        
        Ray up = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height, 0));
        Ray down = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, 0, 0));
        Ray left = Camera.main.ScreenPointToRay(new Vector3(0, Screen.height, 0));
        Ray right = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height / 2, 0));
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("border");
        if (Physics.Raycast(up, out hit, layer_mask))
        {
            if (hit.collider.gameObject.name == "upborder")
            {
                var tempos = gameObject.transform.position;
                if (newpos.y > tempos.y)
                {
                    newpos.y = tempos.y;
                }
            }
        }
        if (Physics.Raycast(down, out hit, layer_mask))
        {
            if (hit.collider.gameObject.name == "downborder")
            {
                
                var tempos = gameObject.transform.position;
                if (newpos.y < tempos.y)
                {
                    newpos.y = tempos.y;
                }

            }
        }
        if (Physics.Raycast(left, out hit, layer_mask))
        {
            if (hit.collider.gameObject.name == "leftborder")
            {
                var tempos = gameObject.transform.position;
                if (newpos.x < tempos.x)
                {
                    newpos.x = tempos.x;
                }

            }
        }
        if (Physics.Raycast(right, out hit, layer_mask))
        {
            if (hit.collider.gameObject.name == "rightborder")
            {
                var tempos = gameObject.transform.position;
                if (newpos.x > tempos.x)
                {
                    newpos.x = tempos.x;
                }

            }
        }
    }
    void Movecam()
    {
        gameObject.transform.position = newpos;
    }
}
