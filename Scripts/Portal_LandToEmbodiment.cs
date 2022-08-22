using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_LandToEmbodiment : MonoBehaviour
{
    bool sendable;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("s1") == 1)
        {
            GameObject.Find("Canvas").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sendable)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("rnmd");
                ReimuController reimuController = GameObject.Find("Reimu").GetComponent<ReimuController>();
                reimuController.currentHealth = reimuController.maxHealth;
                reimuController.currentMagic = reimuController.maxMagic;
                GameObject.Find("Reimu").GetComponent<ReimuController>().my_Save();
                /*pos.x = -2;
                pos.y = 0;*/
                PlayerPrefs.SetFloat("pos_x", -13.73f);
                PlayerPrefs.SetFloat("pos_y", -1.85f);
                PlayerPrefs.SetInt("portal_flag", 1);
                PlayerPrefs.SetInt("s1", 1);
                //rei.chuansong=true;
                //rei.portal_pos = new Vector2(3.59f, -10.64f);
                //DontDestroyOnLoad(rei);
                SceneManager.LoadScene("Embodiment");

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("EXIT");
        sendable = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log("EXIT");
        sendable = false;
    }
}
