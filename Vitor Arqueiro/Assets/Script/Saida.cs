using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saida : MonoBehaviour
{
    public GameObject[] objs;
    private AudioController audioController;
    // Start is called before the first frame update
    void Start()
    {
         audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyBlock()
    {
        StartCoroutine("DestroyBlockEnum");
    }

    public IEnumerator DestroyBlockEnum()
    {
        foreach (GameObject obj in objs)
        {
            obj.GetComponent<StoneBlock>().DestroyBlock();
            Destroy(obj);
            audioController.tocarFx(audioController.StoneBlockAudioClip, 2);
            yield return new WaitForSeconds(1f);
        }
    }
}
