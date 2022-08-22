using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        ReimuController controller = other.gameObject.GetComponent<ReimuController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
