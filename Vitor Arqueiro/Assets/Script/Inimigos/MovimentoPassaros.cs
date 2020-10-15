using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPassaros : Monster
{
    public bool colidde = false;

    private float move = -2;
    private GameController gameController;
    public GameObject explosion;
    // Use this for initialization
    void Start()
    {
        gameController = GameController.getInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && gameController.isPause == false)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(move, GetComponent<Rigidbody2D>().velocity.y);
            if (colidde)
            {
                Flip();
                colidde = false;
            }
        } else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        move *= -1;
        transform.localScale = theScale;
        colidde = false;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log("OnCollisionEnter");
        if (col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("StoneBlock") || col.gameObject.CompareTag("QuestionBlock") || col.gameObject.CompareTag("Monster"))
        {
            colidde = true;
        }
    }

   
}
