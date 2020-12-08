using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseController : MonoBehaviour
{
    public int stageSize;

    public GameObject startPhase;
    public GameObject middle;
    public GameObject endPhase;
    public GameObject[] parts;
    public Dictionary<int, GameObject> lego;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = new Vector3(100,100,0);
        lego = new Dictionary<int, GameObject>();
        createLego();
        Debug.Log("DEPOIS LEGO");            
        StartCoroutine ("animacaoMorrer");
    }
    
   private IEnumerator animacaoMorrer()
    {
        yield return new WaitForSeconds (2f);
    }
    private void createLego() {

        //Instantiate (startPhase, getPosition(0), this.transform.localRotation);
        startPhase.gameObject.transform.position = getPosition(0);
        startPhase.SetActive(true);
        for (int i = 1; i < stageSize; i++)
        {
            //Debug.Log("FOR");  
            int index = Random.Range(0,parts.Length);
            while(lego.ContainsKey(index)) {
                //Debug.Log("while");  
                index = Random.Range(0,parts.Length);
            }
            lego.Add(index, parts[index]);
            GameObject obj = parts[index];
            Instantiate (obj, getPosition(i), startPhase.transform.localRotation);
            obj.gameObject.transform.position = getPosition(i);
            //obj.SetActive(true);
            //Debug.Log(getPosition(i)+ "Name - " +obj.gameObject.name);
            
        }
        //Instantiate (endPhase, getPosition(stageSize), this.transform.localRotation);
        endPhase.gameObject.transform.position = getPosition(stageSize);
        endPhase.SetActive(true);
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
