using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudActions : MonoBehaviour
{

    public GameObject jogador;
    public void MoverLeft()
    {
        Vector2 velo = Vector2.right;
        velo.x = velo.x + 10f;
        jogador.
            transform.Translate((velo * 10f * Time.deltaTime));

        Debug.Log("CLiquei");
        //jogador.GetComponent<Player>().movimento = -1;
        jogador.GetComponent<Player>().Fire();
        GameObject temp = GameObject.Find("Player");
        //temp.GetComponent<Player>().movimento = -1;
    }
    public void MoverRight()
    {
        //GetComponent<Player>().movimento = 1;
    }
}
