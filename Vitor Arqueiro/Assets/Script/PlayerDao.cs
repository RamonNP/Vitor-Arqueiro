using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDao : MonoBehaviour
{
    public static PlayerDao instance;
    public static PlayerDao getInstance() {
        if(instance == null)
        {
            instance = GameObject.FindObjectOfType<PlayerDao>();
        }
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void saveString(string chave, string valor) {
        PlayerPrefs.SetString(chave, valor);
    }
    public string loadString(string chave) {
        return PlayerPrefs.GetString(chave);
    }
    public void saveInt(string chave, int valor) {
        PlayerPrefs.SetInt(chave, valor);
    }
    public int loadInt(string chave) {
        return PlayerPrefs.GetInt(chave);
    }

}
