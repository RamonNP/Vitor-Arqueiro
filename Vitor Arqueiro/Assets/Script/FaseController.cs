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
        StartCoroutine ("animacaoMorrer");
    }
    
   private IEnumerator animacaoMorrer()
    {
        yield return new WaitForSeconds (2f);
    }
    private void createLego() {

        startPhase.gameObject.transform.position = getPosition(0);
        for (int i = 1; i < stageSize; i++)
        {
            int index = Random.Range(0,parts.Length);
            while(lego.ContainsKey(index)) {
                index = Random.Range(0,parts.Length);
            }
            lego.Add(index, parts[index]);
            GameObject obj = parts[index];
            obj.gameObject.transform.position = getPosition(i);
            //Debug.Log(getPosition(i)+ "Name - " +obj.gameObject.name);
            
        }
        endPhase.gameObject.transform.position = getPosition(stageSize);
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
