﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Fade fade;
    public GameObject objInteracao;
    public Text LOG;
    private AudioController audioController;
    public float velocidadeFlexa;
    public GameObject flexa;
    public int quantidadeFlechas;
    public Text qtdFlexa;
    public Transform spawnFlecha;
    public bool attacking; 
    public bool lookLeft;
    public int fase;
    public int TempoFase;
    public int qtdMoedasInt;
    private float timeFloat;
    public Text qtdMoedas;
    public Text time;
    public Text TXTDebug;
    public GameObject estrela0;
    public GameObject estrela3;
    public GameObject estrela2;
    public GameObject estrela1;

    public float forcaPulo;
    public float velocidade;
    //public GameObject lastCheckpoint;
    public GameObject fire;

    public Vector2 offset = new Vector2(0.4f, 0.1f);
    public Vector2 velocityFireball;


    //Explosion
    public GameObject explosion;

    //variaveis do Tiro
    public Transform bulletSpawn;
    public float fireRate;
    public float nextFire;

    //Verifica chao
    public bool isGround;
    public bool isWall;
    public Transform groundCheck;
    public LayerMask wallCheckLayer;
    public Transform wallCheck;
    //public LayerMask wallCheckLayerSlider;

    //Estrelas na fase
    public GameObject starFase1;
    public GameObject starFase2;
    public GameObject starFase3;
    public bool boss;

    public GameObject gameOverUi;

    public AudioClip CoinAudioClip;
    public AudioClip JumpAudioClip;
    public AudioClip EnemeDeadAudioClip;
    public AudioClip StoneBlockAudioClip;
    public AudioClip HitBlockAudioClip;
    public AudioClip DeadkAudioClip;
    public AudioClip LevelClearAudioClip;
    public AudioClip ShootClearAudioClip;
    private List<GameObject> ObjetosDesativados = new List<GameObject>();
    private Animator animator;

    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
    public float ShakeAmplitude = 3.2f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 3.0f;         // Cinemachine Noise Profile Parameter

    public float ShakeElapsedTime = 0f;


    private GameController gameController;
    public float movimento;
    public Text qtdMoedasIAP;
    public Text QtdFlechasIAP;
    public Text preco2000Moedas;
    public Text preco10000Moedas;
    private bool morto;
    void Start()
    {
        morto = false;
        starFase1 = GameObject.Find("Star1");
        starFase2 = GameObject.Find("Star2");
        starFase3 = GameObject.Find("Star3");

        qtdFlexa = GameObject.Find("QtdFlecha").GetComponent<Text>();
        qtdMoedas = GameObject.Find("QtdMoedas").GetComponent<Text>();
        

        time = GameObject.Find("Time").GetComponent<Text>();
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        gameController = GameController.getInstance();
        gameController.isPause = false;
        animator = GetComponent<Animator>();
        //this.rewardedAd = new RewardedAd(adUnitId);
        //Screen.orientation = ScreenOrientation.LandscapeLeft;

        loadStars();
        //PlayerDao.getInstance().saveInt("notaFinal" + idTema.ToString(), (int) notaFinal);
        //idTema = PlayerPrefs.GetInt("idTema");
        qtdMoedasInt = PlayerPrefs.GetInt("coins");
        qtdMoedas.text = qtdMoedasInt.ToString().PadLeft(4, '0');

        quantidadeFlechas = PlayerPrefs.GetInt("arrow");
        qtdFlexa.text = quantidadeFlechas.ToString().PadLeft(4, '0');

        ShowStars();
        if(gameController.lastCheckpoint != null) {
            // TODO: Fazer o personagem aparecer no CheckPOint
            //transform.position = gameController.lastCheckpoint;
        }
        //AdmobScript.getInstance().RequestInterstitial();
        //AdmobScript.getInstance().RequestRewardedAd();
        fade = FindObjectOfType(typeof(Fade)) as Fade;
        if(fade != null) {
            fade.fadeOut();
        }
        if(gameController.lastCheckpoint.x != 0){
            transform.position = new Vector3(gameController.lastCheckpoint.x, gameController.lastCheckpoint.y + 1.5f, gameController.lastCheckpoint.z);
        }
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null){
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>(); 
        }
        fase =  PlayerDao.getInstance().loadInt("faseAtual");
        
    }
    public bool estaMorto() {
        return morto;
    }
    public void carregarQtdMoedasIAP() { 
        IAPController.instancia.inicializaPrecos();
        qtdMoedasInt = PlayerDao.getInstance().loadInt("coins");
        qtdMoedasIAP.text = qtdMoedasInt.ToString().PadLeft(4, '0');
        quantidadeFlechas = PlayerDao.getInstance().loadInt("arrow");
        QtdFlechasIAP.text = quantidadeFlechas.ToString().PadLeft(4, '0');
        preco2000Moedas.text = IAPController.instancia.preco2000Moedas;
        preco10000Moedas.text = IAPController.instancia.preco10000Moedas;
    }
    public void cameraShake() {
         // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameController.isPause == false)
        {
            cameraShake();
            //movimento = Input.GetAxisRaw("Horizontal");
            //ShowStars();
            CheckDead();
            if (!GetComponent<SpriteRenderer>().flipX)
            {
                bulletSpawn.position = new Vector3(this.transform.position.x + 0.3f, bulletSpawn.position.y, bulletSpawn.position.z);
            }
            else
            {
                bulletSpawn.position = new Vector3(this.transform.position.x - 0.3f, bulletSpawn.position.y, bulletSpawn.position.z);
            }
            //check chao
            //Debug.DrawRay(groundCheck.position, 0.50f, Color.red);
            isGround = Physics2D.OverlapCircle(groundCheck.position, explosionRadius, wallCheckLayer);
            //isWall = Physics2D.OverlapCircle(wallCheck.position, explosionRadius, wallCheckLayerSlider);
            animator.SetBool("grounded", isGround);
            //contador de tempo
            timeFloat += Time.deltaTime;
            time.text = ((((int)timeFloat) - TempoFase) * -1).ToString().PadLeft(3, '0');
            if (timeFloat > TempoFase)
            {
                GameOver();
            }


            //Input.GetAxisRaw("Horizontal");
            if(!attacking) {
                //Debug.Log("ENTRO NA FLECHAAAAAAAAAAA");
                //movimento = Input.GetAxisRaw("Horizontal");
                if(movimento == 0)
                {
                    //movimento = Input.GetAxisRaw("Horizontal");
                }
                GetComponent<Rigidbody2D>().velocity = new Vector2(movimento * velocidade, GetComponent<Rigidbody2D>().velocity.y);
                /*if (movimento < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }  else if (movimento > 0) {
                    GetComponent<SpriteRenderer>().flipX = false;
                } */

                if (movimento > 0 || movimento < 0)
                {
                    animator.SetBool("parado", false);
                    if (movimento> 0 && lookLeft == true)
                    {
                        Flip();
                    } else if(movimento < 0 && lookLeft == false)
                    {
                        Flip();
                    }
                } else {
                    animator.SetBool("parado", true);
                }
            } else {
                //caso ja tenha começado a andar e atira o movimento volta a 0
                //movimento = 0;
            }


            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                GetComponent<AudioSource>().PlayOneShot(JumpAudioClip);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcaPulo));
            }
            //Apagar memoria estrelas
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerPrefs.DeleteAll();
            }
            /*if (Input.GetKeyDown(KeyCode.Tab) && Time.time > nextFire)
            {

                Fire();
 
            } */
            animator.SetFloat("speedY", GetComponent<Rigidbody2D>().velocity.y);
        }
       
    }

    public void comprar(string qtd) {
        qtdMoedasInt = PlayerDao.getInstance().loadInt("coins");
        int novasQtdMoedas = qtdMoedasInt;
        print("COMPRA "+qtd);
        switch (qtd)
        {
            case "10":
                if(qtdMoedasInt > 200){
                    novasQtdMoedas = novasQtdMoedas-200;
                    addFlexa(10);
                }
                break;
            case "50":
                if(qtdMoedasInt > 1000){
                    novasQtdMoedas = novasQtdMoedas-1000;
                    addFlexa(50);
                }
                break;
            case "100":
                if(qtdMoedasInt > 2000){
                    novasQtdMoedas = novasQtdMoedas-2000;
                    addFlexa(100);
                }
                break;
            case "2000moedas":
                if(IAPController.instancia.CompraProduto(qtd)){
                    //addCoinsQtd(2000);
                    //novasQtdMoedas = novasQtdMoedas+2000;
                }
                break;
            case "10000moedas":
                if(IAPController.instancia.CompraProduto(qtd)){
                    //addCoinsQtd(10000);
                    //novasQtdMoedas = novasQtdMoedas+10000;
                }
                break;
            default:
                break;
        }
        //Atualia a quantidade de flechas do panel compras IAP
        quantidadeFlechas = PlayerDao.getInstance().loadInt("arrow");
        QtdFlechasIAP.text = quantidadeFlechas.ToString().PadLeft(4, '0');
        //grava as moedas no banco
        PlayerDao.getInstance().saveInt("coins",novasQtdMoedas);
        //atualiza HUD de quantidade de moedas apos a compra
        qtdMoedas.text = novasQtdMoedas.ToString().PadLeft(4, '0');
        //atualiza as moedas do painel apos a compra
        qtdMoedasIAP.text = novasQtdMoedas.ToString().PadLeft(4, '0');
    }
    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        lookLeft = !lookLeft;
    }

    private void OnTriggerExit2D(Collider2D collision2d) {
        switch (collision2d.gameObject.tag)
            {
                case "PortaTeleporte":
                    collision2d.gameObject.GetComponent<PortaTeleporte>().tPlayer = null;
                    objInteracao = null;
                    collision2d.gameObject.SendMessage("removeInteracao", SendMessageOptions.DontRequireReceiver);
                    break;
                default:
                {
                    break;
                }
                
            }
}

    public void interacao() {
        objInteracao.gameObject.SendMessage("interacao", SendMessageOptions.DontRequireReceiver);
    }
    private void OnTriggerEnter2D(Collider2D collision2d)
    {
        //Debug.Log(collision2d.gameObject.tag);
        switch (collision2d.gameObject.tag)
        {
             case "Chefe":
                collision2d.gameObject.SendMessage("DanoPulo", SendMessageOptions.DontRequireReceiver);
                StartCoroutine("DanoPulo");
                break;
            case "Bau":
                collision2d.gameObject.SendMessage("abrirBau", SendMessageOptions.DontRequireReceiver);
                break;
            case "PortaTeleporte":
                collision2d.gameObject.GetComponent<PortaTeleporte>().tPlayer = this.transform;
                collision2d.gameObject.SendMessage("addInteracao", SendMessageOptions.DontRequireReceiver);
                objInteracao = collision2d.gameObject;
                break;
            case "Moedas":
                GetComponent<AudioSource>().PlayOneShot(CoinAudioClip);
                Destroy(collision2d.gameObject);
                addCoins();
                break;
            case "Door":
                GetComponent<AudioSource>().PlayOneShot(LevelClearAudioClip);
                gameController.isPause = true;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //animator.SetBool("walking", false);
                //animator.SetBool("grounded", true);
                //animator.SetTrigger("Idle");
                StartCoroutine(FinishGame(fase));
                break;
            case "CheckPoint":
                gameController.lastCheckpoint = collision2d.gameObject.transform.position;
                SaveStars();
                //lastCheckpoint.transform.position.y = lastCheckpoint.transform.position.y + 10f;
                //lastCheckpoint.transform.position = new Vector3(lastCheckpoint.transform.position.x, lastCheckpoint.transform.position.y+10, lastCheckpoint.transform.position.z);
                break;
            case "Star":
                //Debug.Log(collision2d.gameObject.GetComponent<StarFase>().numberOfStar);
                collision2d.gameObject.GetComponent<StarFase>().Ativo = true;
                ShowStars();
                collision2d.gameObject.SetActive(false);
                break;
            case "Armadilha":
                GameOver();
                break;
            
        }
       
    }
    IEnumerator DanoPulo() {
        if(!jump) {
            jump = true;
            GetComponent<AudioSource>().PlayOneShot(EnemeDeadAudioClip);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcaPulo*2));
        }
        yield return new WaitForSeconds(1f);
        jump = false;
    }
    public void mover(int direcao) {
        movimento = direcao;
        //GetComponent<Rigidbody2D>().velocity = new Vector2(direcao * velocidade, GetComponent<Rigidbody2D>().velocity.y);
        //print(movimento);
    }
    private IEnumerator FinishGame(int fase)
    {
        SaveStars();
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //GetComponent<AudioSource>().PlayOneShot(CompleteAudioClip);
        //GameObject.Find("Door").GetComponent<Door>().Open();
        yield return new WaitForSeconds(7f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        gameController.FinishGame(fase);
    }

    public void btnAtacarFlexa() {
        if(movimento == 0) {
            if(quantidadeFlechas > 0 ) {
            
                if (movimento >= 0 && !attacking) {                
                    animator.SetTrigger("flexa");
                    audioController.tocar2Fx(audioController.armBow, audioController.shotArrow, 2);
                }

            }else {
                audioController.tocarFx(audioController.fxMiss, 2);
            }
        }
    }

    public void AttackFlecha (int atk)
    {
        switch (atk)
        {
            case 0:
            {
                //Debug.Log("0 - "+"EsperarNovoAtaque");
                //attacking = false;
                //arcos[2].SetActive (false);
                StartCoroutine ("EsperarNovoAtaque");
                break;
            }

            case 1:
            {
                //Debug.Log("1 - "+"atack- true");
                attacking = true;
                break;
            }

            case 2:
            {
                //print("DIMINUINDO FLEXAS");
                removeFlexa(1);
                GameObject flechaTemp = Instantiate (flexa, spawnFlecha.position, spawnFlecha.localRotation);
                flechaTemp.transform.localScale = new Vector3 (flechaTemp.transform.localScale.x * transform.localScale.x, flechaTemp.transform.localScale.y, flechaTemp.transform.localScale.z);
                flechaTemp.GetComponent<Rigidbody2D>().velocity = new Vector2 ( velocidadeFlexa * transform.localScale.x, 0);
                Destroy (flechaTemp, 2f);

                break;
            }
            
            default:
            {
                break;
            }
        }
    }

    public void menu() {
        audioController.trocarMusica(audioController.musicaTitulo, "MenuJairo", true);
    }
    private void removeFlexa(int qtd)
    {
        quantidadeFlechas -= qtd;
        qtdFlexa.text = quantidadeFlechas.ToString().PadLeft(4, '0');
        PlayerDao.getInstance().saveInt("arrow", quantidadeFlechas);
    }
    private void addFlexa(int qtd)
    {
        quantidadeFlechas += qtd;
        qtdFlexa.text = quantidadeFlechas.ToString().PadLeft(4, '0');
        PlayerDao.getInstance().saveInt("arrow", quantidadeFlechas);
    }
    private IEnumerator EsperarNovoAtaque ()
    {
        yield return new WaitForSeconds (0.2f);
        attacking = false;
    }

    public void Morrer()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<AudioSource>().PlayOneShot(DeadkAudioClip);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameController.isPause = true;
        // TODO: adicionar  Time.timeScale = 0;, verificar como deixar o botão e o personagem fora do time scale
        //Time.timeScale = 0;
        animator.SetTrigger("morrer");
        StartCoroutine ("animacaoMorrer");

    }
    private IEnumerator animacaoMorrer()
    {
        yield return new WaitForSeconds (2f);
        gameOverUi.SetActive(true);
        if(gameController.lastCheckpoint != null){
            transform.position = new Vector3(gameController.lastCheckpoint.x, gameController.lastCheckpoint.y + 1.5f, gameController.lastCheckpoint.z);
        }
        //string nomeCena = "fase_2";
        //audioController.trocarMusica(audioController.musicaFase1, nomeCena, true);
    }

    

  
    private void addCoins()
    {
        qtdMoedasInt += 1;
        qtdMoedas.text = qtdMoedasInt.ToString().PadLeft(4, '0');
        PlayerDao.getInstance().saveInt("coins", qtdMoedasInt);
    }
    public void addCoinsQtd(int qtd)
    {
        qtdMoedasInt += qtd;
        qtdMoedas.text = qtdMoedasInt.ToString().PadLeft(4, '0');
        PlayerDao.getInstance().saveInt("coins", qtdMoedasInt);
        //atualiza as moedas do painel apos a compra
        qtdMoedasIAP.text = qtdMoedasInt.ToString().PadLeft(4, '0');
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Arrow":
                //Debug.Log("Arrow");
                audioController.tocarFx(audioController.fxBow, 1);
                Destroy(collision.gameObject);
                addFlexa(10);
                break;
            case "Chefe":
                if(!jump) {
                    GameOver();
                }
                break;
            case "Monster":
                if (transform.position.y < collision.transform.position.y)
                {
                    GameOver();
                } else {
                    GetComponent<AudioSource>().PlayOneShot(EnemeDeadAudioClip);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcaPulo));

                    //collision.gameObject.SendMessage("damage",collision.gameObject ,SendMessageOptions.DontRequireReceiver);
                    //Colcoar as informaçoes de morte no monstro
                    //CreateExplosion(collision.gameObject);
                    //ObjetosDesativados.Add(collision.gameObject);
                    //Debug.Log("CHAMANDOO LOOT");
                    //collision.gameObject.SendMessage("loot", SendMessageOptions.DontRequireReceiver);
                    //collision.gameObject.SetActive(false);
                    Destroy(collision.gameObject);
                    print("DESTROINDO");
                    Instantiate(gameController.explosion2, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
                    //collision.gameObject.GetComponent<Animator>().SetTrigger("explosion2");
                }
            break;

        }


        if (collision.gameObject.CompareTag("Monster"))
        {

            
        }


        switch (collision.gameObject.tag)
        {
            case "QuestionBlock":
                
                GetComponent<AudioSource>().PlayOneShot(HitBlockAudioClip);
                foreach (ContactPoint2D hitPos in collision.contacts)
                {
                    if (hitPos.normal.y < 0)
                    {
                        addCoins();
                        collision.gameObject.GetComponent<QuestionBlock>().QuestionBlockBounce();
                    }
                }
                break;
            case "StoneBlock":
                foreach (ContactPoint2D hitPos in collision.contacts)
                {
                    if(hitPos.normal.y < 0)
                    {
                        collision.gameObject.GetComponent<StoneBlock>().DestroyBlock();
                        GetComponent<AudioSource>().PlayOneShot(StoneBlockAudioClip);
                        collision.gameObject.SetActive(false);
                        ObjetosDesativados.Add(collision.gameObject);
                        //Destroy(collision.gameObject);
                    }
                }
                break;
            case "Boss":
                //Debug.Log("Boss");
                if (transform.position.y < collision.transform.position.y)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    //GameOver();
                    //Debug.Log(fase);
                    SceneManager.LoadSceneAsync("Fase_"+fase);
                }
                else
                {
                    collision.gameObject.GetComponent<Boss>().healt--;
                    GetComponent<AudioSource>().PlayOneShot(EnemeDeadAudioClip);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcaPulo));
                    if (collision.gameObject.GetComponent<Boss>().healt < 1)
                    {
                        gameController.FinishAudio();
                        CreateExplosion(collision.gameObject);
                        Destroy(collision.gameObject);
                        GetComponent<AudioSource>().PlayOneShot(LevelClearAudioClip);
                        gameController.isPause = true;
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        StartCoroutine(FinishGame(fase));
                    } else
                    {
                        if(collision.gameObject.GetComponent<Boss>().speed > 0)
                        {
                            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(500, 200));   
                        } else
                        {
                            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 200));   
                        }
                        collision.gameObject.GetComponent<Animator>().SetTrigger("dano");                       
                        //collision.gameObject.GetComponent<Boss>().Dano();
                    }
                }
                break;
            case "nada":
                break;

        }
     
    }

    void CheckDead()
    {
        if (transform.position.y < -50f)
        {
            GameOver();
        }
    }
    void checkColisionMonster()
    {
        
    }

    void GameOver()
    {
        morto = true;
        timeFloat = 0;
        Morrer();
        AdMobController.getInstance().ShowInterstitial();

    }
    public void MoreArrows(int qtd){
        //print("mais Flexas");
        RewardedAdsScript.getInstance().ShowRewardedVideo();
        AdMobController.getInstance().RequestRewords();
    }
    public void CallBackmoreArrows(int qtd){
        addFlexa(qtd);
    }
    public void Fire()
    {
        if(Time.time > nextFire)
        {
            GetComponent<AudioSource>().PlayOneShot(ShootClearAudioClip);
            nextFire = Time.time + fireRate;
            GameObject cloneBullet = Instantiate(gameController.explosion2, bulletSpawn.position, bulletSpawn.rotation);
            cloneBullet.gameObject.GetComponent<Bullet>().sSpeedPerson(Vector2.right.x);
            if (GetComponent<SpriteRenderer>().flipX)
            {
                cloneBullet.transform.eulerAngles = new Vector3(0,0,180);
            }

        }

    }
    void ShowStars()
    {
        if (!boss)
        {

            int qtd = 0;
            if(starFase1 != null && starFase1.GetComponent<StarFase>().Ativo)
            {
                starFase1.SetActive(false);
                qtd++;
            }
            if (starFase2 != null && starFase2.GetComponent<StarFase>().Ativo)
            {
                starFase2.SetActive(false);
                qtd++;
            }
            if (starFase3 != null && starFase3.GetComponent<StarFase>().Ativo)
            {
                starFase3.SetActive(false);
                qtd++;
            }
            switch (qtd)
            {
                case 0:
                    estrela0.SetActive(true);
                    estrela1.SetActive(false);
                    estrela2.SetActive(false);
                    estrela3.SetActive(false);
                    break;
                case 1:
                    estrela0.SetActive(false);
                    estrela1.SetActive(true);
                    estrela2.SetActive(false);
                    estrela3.SetActive(false);
                    break;
                case 2:
                    estrela0.SetActive(false);
                    estrela1.SetActive(false);
                    estrela2.SetActive(true);
                    estrela3.SetActive(false);
                    break;
                case 3:
                    estrela0.SetActive(false);
                    estrela1.SetActive(false);
                    estrela2.SetActive(false);
                    estrela3.SetActive(true);
                    break;
            }
        }
    }
    void SaveStars()
    {
        //PlayerDao.getInstance().saveInt("notaFinal" + idTema.ToString(), (int) notaFinal);
        //idTema = PlayerPrefs.GetInt("idTema");
        //salva estrelas
        //Debug.Log(starFase3.GetComponent<StarFase>().Ativo);
        if (starFase1 != null && starFase1.GetComponent<StarFase>().Ativo)
        {
            
            PlayerDao.getInstance().saveInt("star"+ fase+"_1",1);
        }
        if (starFase2 != null && starFase2.GetComponent<StarFase>().Ativo)
        {
            PlayerDao.getInstance().saveInt("star" + fase + "_2", 1);
        }
        if (starFase3 != null && starFase3.GetComponent<StarFase>().Ativo)
        {
            PlayerDao.getInstance().saveInt("star" + fase + "_3", 1);
        }
    }
    void loadStars()
    {

        //Debug.Log(PlayerPrefs.GetInt("star3"));
        if (PlayerPrefs.GetInt("star" + fase + "_1") == 1)
        {
            try
            {
                starFase1.GetComponent<StarFase>().Ativo = true;
            }
            catch (System.Exception ex)
            {
                 // TODO
            }
        }
        if (PlayerPrefs.GetInt("star" + fase + "_2") == 1)
        {
             try
            {
                starFase2.GetComponent<StarFase>().Ativo = true;
            }
            catch (System.Exception ex)
            {
                 // TODO
            }
        }
        if (PlayerPrefs.GetInt("star" + fase + "_3") == 1)
        {
             try
            {
                starFase3.GetComponent<StarFase>().Ativo = true;
            }
            catch (System.Exception ex)
            {
                 // TODO
            }
        }
        ShowStars();
    }

    private bool jump;
    public void JumpUp()
    {
        if (isGround && !jump)
        {
            jump = true;
            GetComponent<AudioSource>().PlayOneShot(JumpAudioClip);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcaPulo));
            StartCoroutine("waitForNewJump");
        }
    }
    private IEnumerator waitForNewJump ()
    {
        yield return new WaitForSeconds (0.2f);
        jump = false;
    }

    
    /*
    public void ShowInterstitialAd()
    {
        //Show Ad
        TXTDebug.text = TXTDebug.text + "  interstitial.IsLoaded() "+ interstitial.IsLoaded();
        if (interstitial.IsLoaded())
        {
            TXTDebug.text = TXTDebug.text + "  interstitial.IsLoaded() INICIOU";
            interstitial.Show();
        } else
        {
            TXTDebug.text = TXTDebug.text + "  interstitial.IsLoaded() NÂO INICIOU";
            interstitial.Show();
            RequestInterstitial();
        }

    }*/ 

    public float explosionRadius = 0.001f;

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawSphere(groundCheck.position, explosionRadius);
    }

    protected void CreateExplosion(GameObject objPos)
    {
        GameObject temp = Instantiate(explosion, objPos.transform.position, objPos.transform.localRotation);
        Destroy(temp, 0.6f);
    }
    public void GuardaPosicao(GameObject collision2d)
    {
        //collision2d.gameObject.SetActive(false);
        CreateExplosion(collision2d.gameObject);
        //ObjetosDesativados.Add(collision2d.gameObject);
    }

}

