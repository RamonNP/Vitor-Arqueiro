using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    public GameController gameController;
    // Start is called before the first frame update
    public void Menu()
    {
        SceneManager.LoadScene("MenuJairo");
    }

    void Start()
    {
        gameController = GameController.getInstance();
    }
    // Update is called once per frame
    public void Retry()
    {
        gameController.Retry();
    }
}
