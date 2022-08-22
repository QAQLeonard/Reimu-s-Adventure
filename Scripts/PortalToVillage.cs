using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToVillage : MonoBehaviour
{
    bool sendable = false;

    // Update is called once per frame
    void Update()
    {
        if (sendable)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("KeyFDown");
                PlayerPrefs.SetFloat("pos_x", -2.84f);
                PlayerPrefs.SetFloat("pos_y", -14.89f);
                PlayerPrefs.SetInt("portal_flag", 1);
                //Debug.Log(PlayerPrefs.GetFloat("portal_flag"));
                GameObject.Find("Reimu").GetComponent<ReimuController>().my_Save();
                PlayerPrefs.SetInt("portal_flag", 0);//0 代表不需要read，1代表需要read
                PlayerPrefs.SetFloat("pos_x", 0);
                PlayerPrefs.SetFloat("pos_y", 0);
                PlayerPrefs.SetFloat("currentHealth", 0);
                PlayerPrefs.SetFloat("currentMagic", 0);
                PlayerPrefs.SetFloat("maxHealth", 0);
                PlayerPrefs.SetFloat("maxMagic", 0);
                PlayerPrefs.SetInt("s0", 0);
                PlayerPrefs.SetInt("s1", 0);
                PlayerPrefs.SetInt("s2", 0);

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
