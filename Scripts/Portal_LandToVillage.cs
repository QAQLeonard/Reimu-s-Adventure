using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_LandToVillage : MonoBehaviour
{
    bool sendable;

    // Update is called once per frame
    void Update()
    {
        if (sendable)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {

                GameObject.Find("Reimu").GetComponent<ReimuController>().my_Save();
                PlayerPrefs.SetFloat("pos_x", -17.63f);
                PlayerPrefs.SetFloat("pos_y", -14.84f);
                PlayerPrefs.SetInt("portal_flag", 1);
                SceneManager.LoadScene("SampleScene");
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
