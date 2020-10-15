using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BossController
{
    // Start is called before the first frame update
    public bool isFlip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = PlayerDistance();
        isMoving = (distance <= distanceAttack);
        if(isMoving)
        {
          
                if ((player.position.x-2 > transform.position.x && sprite.flipX) ||
                (player.position.x+2 < transform.position.x && !sprite.flipX))
            {
                Flip();
                isFlip = !isFlip;
            }
        }
        //Debug.Log(distance);
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            if ((player.position.x + 2 > transform.position.x && sprite.flipX) ||
              (player.position.x - 2 < transform.position.x && !sprite.flipX))
            {
                GetComponent<Animator>().SetTrigger("atack");
            } else
            {
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                GetComponent<Animator>().SetBool("walking", true);
            }
        } else
            GetComponent<Animator>().SetBool("walking", false);
        {

        }
    }
    public IEnumerator Dano()
    {
        healt--;
        yield return new WaitForSeconds(0.5f);
    }
}
