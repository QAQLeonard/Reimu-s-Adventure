using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Protal_EmbodimentToLand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("s2") == 1)
        {
            GameObject.Find("Canvas").SetActive(false);
        }
    } 
}
