    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum GameState
{
	DIALOGO,
	FIMDIALOGO,
	GAMEPLAY,
	ITENS,
	LOADGAME,
	PAUSE,
    MORRER
}
public class GameController : MonoBehaviour
{
    public bool controle;
    private Player player;
    public LayerMask interacaoMaskInimigoHorizontal;

    public GameObject explosion2;
    public GameObject[] loots;
    private static GameController instance;
    public Vector3 lastCheckpoint;
    private AudioController audioController;
    public bool isPause = false;
    public GameObject Music;

    public void FinishAudio()
    {
        Music.GetComponent<AudioSource>().Stop();
    }

    // Start is called before the first frame update
    public void FinishGame(int fase)
    {
        //AudioListener.volume = 0;
        
        fase++;
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("Fase_"+ fase, 1);
        isPause = false;
        SceneManager.LoadScene("MenuJairo");
        //FinishCanvasGroup.interactable = true;
        //StartCoroutine(Fade(FinishCanvasGroup, 0f, 1f, 2f));
    }

    void Update() {
        //MovimentoPassaros Controle USB
        int movimento = (int) Input.GetAxisRaw("Horizontal");
        if(movimento != 0 ){
            controle = true;
            move(movimento);
        } else if(controle) {
            move(0);
            controle = false;
        }

        int btnJump = (int) Input.GetAxisRaw("Fire3");
        if(btnJump != 0){
            Jump();
        }
        int btnFlexa = (int) Input.GetAxisRaw("Fire2");
        if(btnFlexa != 0){
            brnFlexa();
        }

    }
    
    void Start()
    {
        if(lastCheckpoint.x != 0){
            
        }
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        DontDestroyOnLoad(this.gameObject);
    }

    private bool click;
    public void Retry()
    {
        if(!click) {
            click = true;
            string nomeCena = SceneManager.GetActiveScene ().name;
            audioController.trocarMusica(audioController.musicaFase1, nomeCena, true);
        }
        StartCoroutine("WaitForNexCLick");
    }
    public void NoPause() {
        isPause = false;
    }
     private IEnumerator WaitForNexCLick ()
    {
        yield return new WaitForSeconds (5f);
        click = false;
    }

        public static GameController getInstance() {
        if(instance == null)
        {
            instance = GameObject.FindObjectOfType<GameController>();
        }
        return instance;
    }

    public void move(int mov){
        if(player == null){
            player = FindObjectOfType(typeof(Player)) as Player;
        } else {
            player.mover(mov);
        }
    }
    public void Jump(){
        if(player == null){
            player = FindObjectOfType(typeof(Player)) as Player;
        } else {
            player.JumpUp();
        }
    }
    public void brnFlexa(){
        if(player == null){
            player = FindObjectOfType(typeof(Player)) as Player;
        } else {
            if(!player.attacking && Time.time > player.nextFire) {
                player.btnAtacarFlexa();
            }
        }
    }
}
