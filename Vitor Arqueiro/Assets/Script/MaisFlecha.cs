using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaisFlecha : MonoBehaviour
{
    private GameController gameController;
    private void Start() {
        gameController = GameController.getInstance();
    }
    public void ativarPause() {
        gameController.isPause = true;
    }
    public void desativarPause() {
        gameController.isPause = false;
    }
}
