using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private float timeDestroy;
    private float speedPerson;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        timeDestroy = 1.0f;
        Destroy(gameObject, timeDestroy);
    }

    public void sSpeedPerson(float speedP)
    {
        speedPerson = speedP;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 velo = Vector2.right;
        velo.x = velo.x + speedPerson;
        transform.Translate((velo * speed * Time.deltaTime));

        
    }

    private void OnTriggerEnter2D(Collider2D collision2d)
    {
        //Debug.Log(collision2d.gameObject.tag);
        switch (collision2d.gameObject.tag)
        {
            case "Monster":
                // GetComponent<Rigidbody2D>().AddForce(new Vector2(distance, height*2));
                //Destroy(collision2d.gameObject);
                player.GuardaPosicao(collision2d.gameObject);
                Destroy(this.gameObject, 0);
                break;
            case "StoneBlock":
                Destroy(this.gameObject, 0);
                break;

            case "QuestionBlock":
                Destroy(this.gameObject, 0);
                break;

        }
        


    }
}
