using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public bool grounded;
    public int jumpcount;
    public float speed;
    public float jumpheight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
    }
    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space))&&(jumpcount>0))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpheight), ForceMode2D.Impulse);
            grounded = false;
            jumpcount--;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") | collision.gameObject.CompareTag("destground"))
        {
            grounded = true;
            jumpcount = 2;
        }
    }

}
