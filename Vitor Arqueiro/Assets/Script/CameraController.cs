using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{

    public GameState estadoAtual;
    void Start()
    {

    }

   // Define novo estado do jogo
	public void MudarEstado (GameState novoEstado)
	{
		/*this.estadoAtual = novoEstado;

		switch (novoEstado)
		{
			case GameState.GAMEPLAY:
			{
				Time.timeScale = 1;
				break;
			}

			case GameState.PAUSE: case GameState.ITENS:
			{
				Time.timeScale = 0;
				break;
			}

			case GameState.FIMDIALOGO:
			{
				StartCoroutine ("fimConversa");
				break;
			}

			default:
			{
				break;
			}
		}*/
	}
}
