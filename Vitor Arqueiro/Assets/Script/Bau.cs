using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bau : MonoBehaviour
{
    private bool aberto;
    public int qtdLootRandon;
     private Animator animator;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        aberto = false;
        animator = GetComponent<Animator>();
        gameController = GameController.getInstance();
    }

   
    public void abrirBau() {
        if(!aberto){
            aberto = true;
            animator.SetBool("aberto",true);
            StartCoroutine("lootCorotine");
        }
    }

    IEnumerator lootCorotine(){
        //this.gameObject.SetActive(false);
        // controle loot
        if(qtdLootRandon == 0) {
            qtdLootRandon = 10;
        }
        int qtdMoedas = Random.Range(1,qtdLootRandon);
        //Debug.Log("MOEDAS PARA GERAR "+qtdMoedas);
        for (int i = 0; i < qtdMoedas; i++)
        {
            GameObject lootTemp = Instantiate(gameController.loots[0], transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50,50), 100) );
            yield return new WaitForSeconds(0.2f);
            GameObject lootTemp2 = Instantiate(gameController.loots[1], transform.position, transform.localRotation);
            lootTemp2.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50,50), 100) );
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



        yield return new WaitForSeconds(2f);
        //Destroy(this.gameObject);
        //damage(this.gameObject);
    }
}
