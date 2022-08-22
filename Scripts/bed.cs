using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bed : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        ReimuController e = other.collider.gameObject.GetComponent<ReimuController>();
        e.currentHealth=e.maxHealth;
        e.currentMagic=e.maxMagic;
    }
}
