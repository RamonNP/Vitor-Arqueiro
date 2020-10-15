using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHorizontalMove : MonoBehaviour
{
    public bool dead;
    public GameObject smoke;
    private Animator animator;
    private bool colidde = false;

    public float move = -2;
    private float xInimigo ;

    public Transform groundCheck;
    public Transform wallCheck;
    public bool isGround;
    public bool wall;
    private Rigidbody2D rigidbody2d;
    public GameObject explosion;
    private GameController gameController;
    public LayerMask wallCheckLayer;

    // Use this for initialization
    void Start()
    {
        gameController = GameController.getInstance();
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       //move = transform.localScale.x ;
    }

    bool flip;
    IEnumerator Flip()
    {
        flip = true;
        float localScaleX = transform.localScale.x;
        localScaleX *= -1;
        move *= -1;
 //       Debug.Log("VARIAVEL X "+ move );
        transform.localScale = new Vector3 (localScaleX, transform.localScale.y, transform.localScale.z);
//        Debug.Log("transform.localScale "+ transform.localScale.x );
        colidde = false;
        yield return new WaitForSeconds(0.4f);
        flip = false;
        //GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
         //Debug.Log("OnCollisionEnter" + col.gameObject.CompareTag("Platform") + col.gameObject.tag+" Name"+ col.gameObject.name);
        if (col.gameObject.CompareTag("Platform"))
        {
            //Debug.Log("TRUE");
            colidde = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
         //Debug.Log("OnCollisionExit"+ col.gameObject.tag+ col.gameObject.name);
        if (col.gameObject.CompareTag("Platform"))
        {
            //Debug.Log("TRUE");
            colidde = false;
            //  Debug.Log("grounded=false");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        switch (other.gameObject.tag)
        {
            case "Arma" :
                //loot();
                //mudar animação para explosão
                Instantiate(gameController.explosion2, other.gameObject.transform.position, other.gameObject.transform.rotation);
                Destroy(other.gameObject); // destroi a flecha
                Destroy(this.gameObject); // destroi a flecha
            break;
            default:
            break;
        } 
    }

    

    private void FixedUpdate()
    {
       if(dead){
           rigidbody2d.velocity = new Vector2(0, 0);
           GetComponent<SpriteRenderer>().sprite = null;
           GetComponent<BoxCollider2D>().enabled = false;
           return;
       }
        if(gameController.isPause == false)
        {
            /*
            if (xInimigo == rigidbody.transform.position.x)
            {
                // Debug.Log("Bugo" + colidde);
                rigidbody.transform.position = new Vector2(rigidbody.transform.position.x + 0.001f, rigidbody.transform.position.y);
            } */
            isGround = Physics2D.OverlapCircle(groundCheck.position, 0.02f, wallCheckLayer);
            //wall = Physics2D.OverlapCircle(wallCheck.position, 0.02f, wallCheckLayer);
            Vector3 dir = Vector3.right;
            dir.x = transform.localScale.x ;
            wall = Physics2D.Raycast(wallCheck.gameObject.transform.position, dir, 0.1f, gameController.interacaoMaskInimigoHorizontal);
            if(wall && !flip){
                 StartCoroutine("Flip");
            }
             //Debug.Log("Update" + " dead" + dead);
            if (!dead)
            {
                // Debug.Log("move" + move +" Y"+ GetComponent<Rigidbody2D>().velocity.y );
                //new Vector2(movimento * velocidade, rigidbody.velocity.y);
                //rigidbody.velocity = new Vector2(transform.localScale.x, rigidbody.velocity.y);
                rigidbody2d.velocity = new Vector2(move, rigidbody2d.velocity.y);

                if (colidde || !isGround )
                //if (colidde )
                {

                    if(!flip){
                        StartCoroutine("Flip");
                    }
                }
            }
            //xInimigo = rigidbody2d.transform.position.x;
        }

    }
    
    //verifica em que objeto colidiu
    public bool checkPlayerColison()
    {
        Collider2D[] result = new Collider2D[1];
        Physics2D.OverlapCircle(wallCheck.position, 0.02f, new ContactFilter2D(), result);
        foreach (Collider2D res in result)
        {
            if (res != null && res.tag == "Player")
            {
                //dead = true;
                return false;
            }
            if (res != null && res.tag == "CheckPoint")
            {
                return false;
            }
        }
        return true;
    }

    
}