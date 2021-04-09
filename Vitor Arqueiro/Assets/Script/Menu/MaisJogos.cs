using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaisJogos : MonoBehaviour
{
    public string url;
    public void Open ()
    {
        if (!string.IsNullOrEmpty (url)) {
            Application.OpenURL (url);
        } else {
            Debug.LogWarning ("Unable to open URL, invalid OS");
        }
    }
}
