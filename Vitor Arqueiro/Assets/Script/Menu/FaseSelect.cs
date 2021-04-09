using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FaseSelect : MonoBehaviour
{
    public GameObject Music;
    public Slider slider;

    AsyncOperation async;

    public int maxFase;
    public int fase;

    //Fases
    public GameObject fase_1;
    public GameObject lock_1;
    public GameObject fase_2;
    public GameObject lock_2;
    public GameObject fase_3;
    public GameObject lock_3;
    public GameObject fase_4;
    public GameObject lock_4;
    public GameObject fase_5;
    public GameObject lock_5;
    public GameObject fase_6;
    public GameObject lock_6;
    public GameObject fase_7;
    public GameObject lock_7;
    public GameObject fase_8;
    public GameObject lock_8;
    public GameObject fase_9;
    public GameObject lock_9;
    public GameObject fase_10;
    public GameObject lock_10;
    public GameObject fase_11;
    public GameObject lock_11;
    public GameObject fase_12;
    public GameObject lock_12;
    public GameObject fase_13;
    public GameObject lock_13;
    public GameObject fase_14;
    public GameObject lock_14;
    public GameObject fase_15;
    public GameObject lock_15;
    public GameObject fase_16;
    public GameObject lock_16;
    public GameObject fase_17;
    public GameObject lock_17;
    public GameObject fase_18;
    public GameObject lock_18;
    public GameObject fase_19;
    public GameObject lock_19;
    public GameObject fase_20;
    public GameObject lock_20;

    private List<String> fases = new List<string>();

    void Start()
    {
        fases.Add("Fase_1");
        for (int i = 0; i < maxFase; i++)
        {
            string fase = "Fase_" + i;
            if (PlayerPrefs.GetInt(fase) == 1)
            {
                OpenLevel("Fase_" + i);
            }
        }
    }
    public void GoToScene(string Scena)
    {
        PlayerDao.getInstance().saveString("lego",null);
        //salva a fase atual em numero, exemplo em fase_1 deixa apenas 1
        int faseAtual = StringToNullableInt(Scena.Substring(Scena.Length-2, 2));
        //FAKE
        //faseAtual = 20;
        //StartCoroutine(LoadScreen("Fase_D"));
        //FAKE
        PlayerDao.getInstance().saveInt("faseAtual", faseAtual);
        print("Scena" + Scena +" faseAtual"+faseAtual);
        if (fases.Contains("Fase_"+faseAtual))
        {
            if(faseAtual > 10){
                StartCoroutine(LoadScreen("Fase_D"));
            } else {
                StartCoroutine(LoadScreen("Fase_"+faseAtual));
            }
        }
    }
    public static int StringToNullableInt(string strNum)
    {
        int outInt;
        return int.TryParse(strNum, out outInt) ? outInt : (int)0;
    }
    private void OpenLevel(string level)
    {
        fases.Add(level);
        switch (level)
        {
            case "Fase_1":
                fase_1.SetActive(true);
                lock_1.SetActive(false);
                break;
            case "Fase_2":
                fase_2.SetActive(true);
                lock_2.SetActive(false);
                break;
            case "Fase_3":
                fase_3.SetActive(true);
                lock_3.SetActive(false);
                break;
            case "Fase_4":
                fase_4.SetActive(true);
                lock_4.SetActive(false);
                break;
            case "Fase_5":
                fase_5.SetActive(true);
                lock_5.SetActive(false);
                break;
            case "Fase_6":
                fase_6.SetActive(true);
                lock_6.SetActive(false);
                break;
            case "Fase_7":
                fase_7.SetActive(true);
                lock_7.SetActive(false);
                break;
            case "Fase_8":
                fase_8.SetActive(true);
                lock_8.SetActive(false);
                break;
            case "Fase_9":
                fase_9.SetActive(true);
                lock_9.SetActive(false);
                break;
            case "Fase_10":
                fase_10.SetActive(true);
                lock_10.SetActive(false);
                break;
            case "Fase_11":
                fase_11.SetActive(true);
                lock_11.SetActive(false);
                break;
            case "Fase_12":
                fase_12.SetActive(true);
                lock_12.SetActive(false);
                break;
            case "Fase_13":
                fase_13.SetActive(true);
                lock_13.SetActive(false);
                break;
            case "Fase_14":
                fase_14.SetActive(true);
                lock_14.SetActive(false);
                break;
            case "Fase_15":
                fase_15.SetActive(true);
                lock_15.SetActive(false);
                break;
            case "Fase_16":
                fase_16.SetActive(true);
                lock_16.SetActive(false);
                break;
            case "Fase_17":
                fase_17.SetActive(true);
                lock_17.SetActive(false);
                break;
            case "Fase_18":
                fase_18.SetActive(true);
                lock_18.SetActive(false);
                break;
            case "Fase_19":
                fase_19.SetActive(true);
                lock_19.SetActive(false);
                break;
            case "Fase_20":
                fase_20.SetActive(true);
                lock_20.SetActive(false);
                break;
        }
    }

    IEnumerator LoadScreen(string scena)
    {
        Debug.Log("scena" +scena);
        if(async == null )
        {
            slider.gameObject.SetActive(true);
            async = SceneManager.LoadSceneAsync(scena);
            async.allowSceneActivation = false;
            while (async.isDone == false) {
                slider.value = async.progress;
                if (async.progress == 0.9f)
                {
                    slider.value = 1f;
                    async.allowSceneActivation = true;
                }
                yield return null;          
            }

        }
    }
     public void BtnPreMenu() {
        SceneManager.LoadSceneAsync("PreMenu");
    }
}
