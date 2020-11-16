using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public bool grounded;
    public bool faceright;
    public int jumpcount;
    public float speed;
    public float jumpheight;
    Animator myanim;
    public bool think = false;
    // Start is called before the first frame update
    void Start()
    {
        myanim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        if (Input.GetAxis("Horizontal") != 0)
        {
            myanim.SetBool("walking", true);
        }else if (Input.GetAxis("Horizontal") == 0)
        {
            myanim.SetBool("walking", false);
        }
        if (Input.GetAxis("Horizontal") < 0 && !faceright)
        {
            flip();
        } else if (Input.GetAxis("Horizontal") > 0 && faceright)
        {
            flip();
        }
        if (think == false)
        {
            myanim.SetBool("thinking", false);
        }
    }
    public void Thinkanim()
    {
        think = true;
        myanim.SetTrigger("think");
        myanim.SetBool("thinking", true);
    }
    void flip()
    {
        faceright = !faceright;
        var loscale = transform.localScale;
        loscale.x *= -1;
        transform.localScale = loscale;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&(jumpcount>0))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpheight), ForceMode2D.Impulse);
            grounded = false;
            jumpcount--;
            myanim.SetBool("grounded", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag==("ground") || collision.gameObject.tag==("destground"))
        {
            grounded = true;
            jumpcount = 2;
            myanim.SetBool("grounded", true);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("ground") || collision.gameObject.tag == ("destground"))
        {
            grounded = true;
            jumpcount = 2;
            myanim.SetBool("grounded", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("ground") || collision.gameObject.tag == ("destground"))
        {
            grounded = false;
            myanim.SetBool("grounded", false);
        }
    }



}