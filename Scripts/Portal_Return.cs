using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_Return : MonoBehaviour
{
    bool sendable = false;
    void Update()
    {
        if (sendable)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
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
