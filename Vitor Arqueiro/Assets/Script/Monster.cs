using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject PuffPrefab;
    protected bool dead = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Dead(Vector3 hammerDirection, Action finishAttackAction)
    {
        StartCoroutine(DestroyWithDelay(hammerDirection, finishAttackAction));
    }


    private IEnumerator DestroyWithDelay(Vector3 hammerDirection, Action finishAttackAction)
    {
        dead = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<Rigidbody2D>().AddForce(hammerDirection * 2.5f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        Instantiate(PuffPrefab, this.transform.position, Quaternion.identity);
        finishAttackAction();
    }

    protected void CreateExplosion(GameObject explosion)
    {
        GameObject temp = Instantiate(explosion, transform.position, transform.localRotation);
        Destroy(temp, 0.6f);
        Destroy(this.gameObject);
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

}