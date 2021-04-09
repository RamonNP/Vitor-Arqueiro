using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioSource sMusic;  // FONTE DE MUSICA
    public AudioSource sFx;     // FONTE DE EFEITOS SONOROS

    [Header("Musicas")]
    public AudioClip musicaTitulo;
    public AudioClip musicaFase1;

    [Header("FX")]
    public AudioClip fxMiss;
    public AudioClip fxClick;

    public AudioClip fxSword;
    public AudioClip fxAxe;
    public AudioClip fxBow;
    public AudioClip fxHammer;
    public AudioClip fxMace;
    public AudioClip fxStaff;
    public AudioClip fxMagicEarth;
    public AudioClip fxShield;
    public AudioClip armBow;
    public AudioClip shotArrow;
    public AudioClip atackSword;
    public AudioClip StoneBlockAudioClip;

    // configurações dos audios
    public float volumeMaximoMusica;
    public float volumeMaximoFx;

    // configurações da troca de musica
    private AudioClip novaMusica;
    private string novaCena;
    private bool trocarCena;

    public static AudioController instance;
    public static AudioController getInstance() {
        return instance;
    }
    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != null) {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if(PlayerPrefs.GetInt("valoresIniciais") == 0)
        {
            PlayerPrefs.SetInt("valoresIniciais", 1);
            PlayerPrefs.SetFloat("volumeMaximoMusica", 1);
            PlayerPrefs.SetFloat("volumeMaximoFx", 1);
        }

        //CARREGA AS CONFIGURAÇÕES DE AUDIO DO APARELHO
        volumeMaximoMusica = PlayerPrefs.GetFloat("volumeMaximoMusica");
        volumeMaximoFx = PlayerPrefs.GetFloat("volumeMaximoFx");
                
    }

    public void trocarMusica(AudioClip clip, string nomeCena, bool mudarCena)
    {
        novaMusica = clip;
        novaCena = nomeCena;
        trocarCena = mudarCena;
        StartCoroutine("changeMusic");
    }

    IEnumerator changeMusic()
    {
        for(float volume = volumeMaximoMusica; volume >= 0; volume -= 0.1f)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            sMusic.volume = volume;
        }
        sMusic.volume = 0;

        sMusic.clip = novaMusica;
        sMusic.Play();

        for (float volume = 0; volume < volumeMaximoMusica; volume += 0.1f)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            sMusic.volume = volume;
        }
        sMusic.volume = volumeMaximoMusica;

        if(trocarCena == true)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(novaCena);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        GameController.getInstance().NoPause();
    }

    private AudioClip fx, fx1;
    private float voll;
    public void tocar2Fx(AudioClip lfx, AudioClip lfx1, float volume){
        fx=lfx;
        fx1=lfx1;
        voll = volume;
        StartCoroutine("tocar2FxEnum");
    }
    IEnumerator tocar2FxEnum()
    {
        tocarFx(fx, voll);
        yield return new WaitForSecondsRealtime(0.5f);
        tocarFx(fx1, voll);
    }
    public void tocarFx(AudioClip fx, float volume)
    {
        float tempVolume = volume;
        if(volume > volumeMaximoFx)
        {
            tempVolume = volumeMaximoFx;
        }
        sFx.volume = tempVolume;
        sFx.PlayOneShot(fx);
    }
    public void BtnMenu() {
        trocarMusica(musicaTitulo, "MenuJairo", true);
    }
   
}
