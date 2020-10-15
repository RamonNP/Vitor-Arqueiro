using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarsControl : MonoBehaviour
{
    public int fase;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        star1.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);
        loadStars();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void loadStars()
    {
        //Debug.Log(PlayerPrefs.GetInt("star3"));
        if (PlayerPrefs.GetInt("star" + fase + "_1") == 1)
        {
            star1.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("star" + fase + "_2") == 1)
        {
            star2.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("star" + fase + "_3") == 1)
        {
            star3.gameObject.SetActive(true);
        }
    }
    public void GotoGame()
    {
        SceneManager.LoadScene("Fase_"+fase.ToString());
    }
}
