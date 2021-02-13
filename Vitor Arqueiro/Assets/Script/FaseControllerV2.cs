using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseControllerV2 : MonoBehaviour
{
    public int stageSize;

    public GameObject startPhase;
    public GameObject middle;
    public GameObject endPhase;
    public GameObject[] parts;
    public GameObject[] moedasPrefab;
    public Dictionary<int, GameObject> lego;

    // Start is called before the first frame update
    void Start()
    {
        //RewardedAdsScript.getInstance().ShowBanner();
        AdMobController.getInstance().RequestBanner();
        this.gameObject.transform.position = new Vector3(100,100,0);
        lego = new Dictionary<int, GameObject>();
        montarLego();
        //monta as moedas que vão aparecer conforme a fase
        int index = PlayerDao.getInstance().loadInt("faseAtual");
        GameObject obj = moedasPrefab[index];
        Instantiate (obj, new Vector3(-28f, 50, 0), startPhase.transform.localRotation);
        //Debug.Log("DEPOIS LEGO");            
        StartCoroutine ("animacaoMorrer");
    }
    
   private IEnumerator animacaoMorrer()
    {
        yield return new WaitForSeconds (2f);
    }

    private void montarLego() {
        //carrega lego da base caso exista, senão cria um lego novo
         string[] lego = PlayerDao.getInstance().loadString("lego").Split(';');
         if(lego.Length > 2) {
            loadLego(lego);
         } else {
            createLego();
         }
    }
    public static int StringToNullableInt(string strNum)
    {
        int outInt;
        return int.TryParse(strNum, out outInt) ? outInt : (int)0;
    }
    private void loadLego(string[] legoBanco) {
        startPhase.gameObject.transform.position = getPosition(0);
        startPhase.SetActive(true);
        for (int i = 0; i < legoBanco.Length; i++)
        {
            print(legoBanco[i]);
            if(legoBanco[i] != ""){
                // adiciona a nova peça ao lego para controlle
                lego.Add(StringToNullableInt(legoBanco[i]), parts[StringToNullableInt(legoBanco[i])]);
                GameObject obj = parts[StringToNullableInt(legoBanco[i])];
                //intancia o objeto
                Instantiate (obj, getPosition(i+1), startPhase.transform.localRotation);
                //coloca o Objeto na posição correta
                obj.gameObject.transform.position = getPosition(i);
                //obj.SetActive(true);
                //Debug.Log(getPosition(i)+ "Name - " +obj.gameObject.name);
            }
        }
        endPhase.gameObject.transform.position = getPosition(stageSize);
        endPhase.SetActive(true);
    }
    private void createLego() {

        //Instantiate (startPhase, getPosition(0), this.transform.localRotation);
        //pega a posição para o primeiro bloco
        startPhase.gameObject.transform.position = getPosition(0);
        startPhase.SetActive(true);
        //string que vai ser salva no banco para depois montar a mesma fase
        string faseIndex = "";
        for (int i = 1; i < stageSize; i++)
        {
            //Debug.Log("FOR");  
            //percorre o lego para não repetir peças de fases
            int index = Random.Range(0,parts.Length);
            while(lego.ContainsKey(index)) {
                //Debug.Log("while");  
                index = Random.Range(0,parts.Length);
            }
            faseIndex = faseIndex + index+";";
            // adiciona a nova peça ao lego para controlle
            lego.Add(index, parts[index]);
            GameObject obj = parts[index];
            //intancia o objeto
            Instantiate (obj, getPosition(i), startPhase.transform.localRotation);
            //coloca o Objeto na posição correta
            obj.gameObject.transform.position = getPosition(i);
            //obj.SetActive(true);
            //Debug.Log(getPosition(i)+ "Name - " +obj.gameObject.name);
            
        }
        //Instantiate (endPhase, getPosition(stageSize), this.transform.localRotation);
        endPhase.gameObject.transform.position = getPosition(stageSize);
        endPhase.SetActive(true);
        PlayerDao.getInstance().saveString("lego", faseIndex);
    }
    private Vector3 getPosition(int i){
        float positionX = 0f;
        switch (i)
        {
            case 1 :
                positionX = 50f;
            break;
            case 2 :
                positionX = 100f;
            break;
            case 3 :
                positionX = 150f;
            break;
            case 4 :
                positionX = 200f;
            break;
            case 5 :
                positionX = 250f;
            break;
            case 6 :
                positionX = 300f;
            break;
            default:
            break;
        }
        //Debug.Log("positionX - " + positionX);
        return new Vector3(positionX, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
