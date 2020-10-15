using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.getInstance();
    }

    public void loot() {
        //CreateExplosion(smoke);
        Debug.Log("CHEGOU NO LOTTTTT");
        StartCoroutine("lootCorotine");
    }

    IEnumerator lootCorotine(){
        //this.gameObject.SetActive(false);
        // controle loot
        int qtdMoedas = Random.Range(1,4);
        Debug.Log("MOEDAS PARA GERAR "+qtdMoedas);
        for (int i = 0; i < qtdMoedas; i++)
        {
            GameObject lootTemp = Instantiate(gameController.loots[0], transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50,50), 100) );
            yield return new WaitForSeconds(0.2f);
        }
        /*int qtdFlexas = Random.Range(0,2);
        Debug.Log("FLECHAS PARA GERAR "+qtdMoedas);
        for (int i = 0; i < qtdMoedas; i++)
        {
            GameObject lootTemp = Instantiate(gameController.loots[1], transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50,50), 100) );
            yield return new WaitForSeconds(0.2f);
        } */

        int qtdFlexas = Random.Range(0,50);
        if(qtdFlexas < 10 ) {
            GameObject lootTemp = Instantiate(gameController.loots[1], transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50,50), 100) );
            yield return new WaitForSeconds(0.2f);
        }


        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        //damage(this.gameObject);
    }
}
