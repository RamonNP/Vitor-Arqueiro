using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject loot;
    public int healt;
    public float distanceAttack;
    public int speed;

    protected bool isDead;
    protected bool isMoving;
    protected bool isAtaking;

    public Rigidbody2D rb2d;
    public CapsuleCollider2D cld2d;
    public Saida saida;
    protected Animator anim;
    protected Transform player;
    public Transform scaleToFlip;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cld2d = GetComponent<CapsuleCollider2D>();
        scaleToFlip = GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        saida = FindObjectOfType(typeof(Saida)) as Saida;
    }

    protected float PlayerDistance()
    {
        return Vector2.Distance(player.position, transform.position);
    }

    //protected void Flip()
   // {
        //sprite.flipX = !sprite.flipX;
        
        //scaleToFlip.position = new Vector2(scaleToFlip.position.x * -1, scaleToFlip.position.y);
   // }
    protected void Flip()
    {
        speed *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //lookLeft = !lookLeft;
    }
}
