using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BossController
{

    private float percVida;
    // Start is called before the first frame update
    public bool isFlip;
    void Start()
    {
        print("RESET VIDA");
        barrasVida.SetActive(false);
        healt = healtMax;
        hpBar.localScale = new Vector3(1,1,1);
        
        if(olhandoAEsquerda == false){
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            //transform.localScale = theScale;
            float x = (theScale.x/2) ;
            barrasVida.transform.localScale = new Vector3(x ,barrasVida.transform.localScale.y,barrasVida.transform.localScale.z);
        } else {
            float x = (transform.localScale.x/2) ;
            barrasVida.transform.localScale = new Vector3(x,barrasVida.transform.localScale.y,barrasVida.transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        return;
        float distance = PlayerDistance();
        isMoving = !(distance <= distanceAttack);
        //Debug.Log(distance +"----"+isMoving);
        if(isMoving)
        {
          
                if ((player.position.x-2 > transform.position.x && isFlip) ||
                (player.position.x+2 < transform.position.x && !isFlip))
            {
                Flip();
                isFlip = !isFlip;
            }
        }
    }
    private void FixedUpdate()
    {
        if(isDead)
        return;
        //Debug.Log("----"+isMoving);
        if(!isAtaking)
        {
           // if ((player.position.x + 2 > transform.position.x && speed > 0) ||
            //  (player.position.x - 2 < transform.position.x && speed < 0))
                //Debug.Log(PlayerDistance());
            if((PlayerDistance() < 2) && !isMoving)
            {

                    ///Debug.Log("atack");
                    StartCoroutine("atack");
                
            } else
            {
                //Debug.Log("walking TRUE");
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                anim.SetBool("walking", true);
            }
        } //else
            //Debug.Log("walking false");
            //anim.SetBool("walking", false);
        //{

        //}
    }
    public void DanoPulo() {
        StartCoroutine("Dano");
    }
    public IEnumerator Dano()
    {
        if(healt > 1) {
            percVida = (float)healt / (float)healtMax;
            barrasVida.SetActive(true);
            if(percVida < 0) {
                percVida = 0;
            } 
            hpBar.localScale = new Vector3(percVida,1,1);
            isAtaking = true;
            healt--;
            anim.SetTrigger("hurt");
            anim.SetBool("walking", false);
            yield return new WaitForSeconds(1f);
            isAtaking = false;
        } else {
            isDead = true;
            this.gameObject.layer = 22;
            anim.SetTrigger("morre");
            Instantiate(loot, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y+2f, this.gameObject.transform.position.z), this.gameObject.transform.rotation);
            saida.DestroyBlock();
        }

    }
    public IEnumerator atack()
    {
        isAtaking = true;
        anim.SetTrigger("atack");
        yield return new WaitForSeconds(1f);
        isAtaking = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        switch (other.gameObject.tag)
        {
            case "Arma" :
                if(isDead)
                    return;
                //loot();
                //mudar animação para explosão
                //Instantiate(explosion2, other.gameObject.transform.position, other.gameObject.transform.rotation);
                Destroy(other.gameObject); // destroi a flecha
                //Destroy(this.gameObject); // destroi a flecha
                StartCoroutine("Dano");
            break;
            default:
            break;
        } 
    }
}
