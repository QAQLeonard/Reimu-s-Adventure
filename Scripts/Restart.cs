using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    bool sendable = false;

    void Update()
    {
        if (sendable)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene("Loading");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sendable = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        sendable = false;
    }
}
