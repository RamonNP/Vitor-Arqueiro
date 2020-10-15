using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudControle : MonoBehaviour
{
    public int TempoFase;
    private float timeFloat;
    public Text qtdMoedas;
    public Text time;
    public GameObject estrela0;
    public GameObject estrela3;
    public GameObject estrela2;
    public GameObject estrela1;
    // Start is called before the first frame update
    void Start()
    {
        qtdMoedas.text = "001";
        time.text = "50";
        estrela0.SetActive(false);
        estrela1.SetActive(false);
        estrela2.SetActive(false);
        estrela3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timeFloat += Time.deltaTime;
        time.text = ((((int)timeFloat)- TempoFase)*-1).ToString().PadLeft(3,'0');
        if(timeFloat > TempoFase)
        {
            GameOver();
        }
    }

    void GameOver()
    {

    }

}
