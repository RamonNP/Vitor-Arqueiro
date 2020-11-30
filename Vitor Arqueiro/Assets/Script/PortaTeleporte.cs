using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaTeleporte : MonoBehaviour
{
    private Fade fade;
    public Transform tPlayer;
    public GameObject destino;
    public GameObject cameras;
    public GameObject iconeInteracao;
    public GameObject botaoPulo;
    public GameObject botaoInteracao;
    public bool caverna;
    // Start is called before the first frame update
    private void Awake() {
            botaoPulo = GameObject.Find("botaoPulo");
            botaoInteracao = GameObject.Find("botaoInteracao");
    }
    void Start()
    {
        fade = FindObjectOfType(typeof(Fade)) as Fade;
        try
        {
            botaoInteracao.SetActive(false); 
        }
        catch (System.Exception)
        {
            //throw;

        }
    }
    

    // Update is called once per frame
    void Update()
    {

    }
    public void addInteracao() {
        if(botaoInteracao.gameObject.activeInHierarchy == false){
            botaoInteracao.SetActive(true);
            //botaoPulo.SetActive(false);
        }
        
    }
    public void removeInteracao() {
        if(botaoInteracao.gameObject.activeInHierarchy == true){
            botaoInteracao.SetActive(false);
            //botaoPulo.SetActive(true);
        }
        
    }
    public void interacao() {
        StartCoroutine("acionarPorta");
    }
    IEnumerator acionarPorta() {
        fade.fadeIn();
        yield return new WaitWhile(() => fade.fume.color.a < 0.9f);
        if(caverna) {
            cameras.gameObject.transform.position = new Vector3(cameras.gameObject.transform.position.x, 53.38f,0);
            cameras.gameObject.transform.localScale = new Vector3(1, 1,1);
            tPlayer.position = destino.gameObject.transform.position;
        } else {
            tPlayer.position = destino.gameObject.transform.position;
            cameras.gameObject.transform.position = new Vector3(cameras.gameObject.transform.position.x, 32.46f,0);
            cameras.gameObject.transform.localScale = new Vector3(1, 1.5f,1);
        }
        yield return new WaitForSeconds(0.5f); 
        fade.fadeOut();
    }
    
}
