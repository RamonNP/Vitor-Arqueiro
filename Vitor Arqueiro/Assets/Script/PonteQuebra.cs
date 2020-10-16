using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonteQuebra : MonoBehaviour
{

    public FixedJoint2D graoPonte;
    // Start is called before the first frame update
    void Start()
    {
        graoPonte = GetComponent<FixedJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator DestroyBridge() {
        yield return new WaitForSeconds(0.8f);
        print("DESTRUINDO OBJETOS");
        Destroy(this.gameObject);
    } 
    private void OnJointBreak2D(Joint2D brokenJoint) {
        print(brokenJoint.name);
        StartCoroutine("DestroyBridge");
    }
}
