using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_VillageToLand : MonoBehaviour
{
    bool sendable;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("s0"));
        if(PlayerPrefs.GetInt("s0")==1)
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
                //ReimuController rei=GameObject.Find("Reimu").GetComponent<ReimuController>();
                /*pos.x = -2;
                pos.y = 0;*/
                GameObject.Find("Reimu").GetComponent<ReimuController>().my_Save();
                PlayerPrefs.SetFloat("pos_x", 3.59f);
                PlayerPrefs.SetFloat("pos_y", -10.64f);
                PlayerPrefs.SetInt("portal_flag", 1);
                PlayerPrefs.SetInt("s0", 1);
                //rei.chuansong=true;
                //rei.portal_pos = new Vector2(3.59f, -10.64f);
                //DontDestroyOnLoad(rei);
                SceneManager.LoadScene("Land");
                
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
