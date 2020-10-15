using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    public float distance;
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Direction(bool right)
    {
        if (right)
        {
            distance = distance * -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision2d)
    {
        Debug.Log("BATEUU");
        switch (collision2d.gameObject.tag)
        {
            case "Platform":
               // GetComponent<Rigidbody2D>().AddForce(new Vector2(distance, height*2));
                break;
            case "CheckPoint":
                break;

            case "nada":
                break;

        }
        
        
    }
}
