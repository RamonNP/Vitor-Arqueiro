using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaisFlecha : MonoBehaviour
{
    private Player player;
    private GameController gameController;
    private void Start() {
        gameController = GameController.getInstance();
        player = FindObjectOfType(typeof(Player)) as Player;
    }
    public void ativarPause() {
        gameController.isPause = true;
    }
    public void desativarPause() {
        if(!player.estaMorto()){
            gameController.isPause = false;
        } else {
            gameController.Retry();
        }
    }
}
