using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlock : MonoBehaviour
{
    public GameObject partOfBlock;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyBlock()
    {
        GameObject temp1 = Instantiate(partOfBlock, transform.position, transform.localRotation);
        GameObject temp2 = Instantiate(partOfBlock, transform.position, transform.localRotation);
        GameObject temp3 = Instantiate(partOfBlock, transform.position, transform.localRotation);
        GameObject temp4 = Instantiate(partOfBlock, transform.position, transform.localRotation);
        //temp4.gameObject.
        temp1.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50, 300f));
        temp2.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(50, 300f));
        temp3.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50, 200f));
        temp4.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(50, 200f));


        //temp4.rigidbody AddForce(new Vector2(0, forcaPulo));
        Destroy(temp1, 0.6f);
        Destroy(temp2, 0.6f);
        Destroy(temp3, 0.6f);
        Destroy(temp4, 0.6f);
    }
}
