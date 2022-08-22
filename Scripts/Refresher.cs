using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresher : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject reimu;
    ReimuController reimuc;
    
    void Start()
    {
        reimu = GameObject.Find("Reimu");
        reimuc = GameObject.Find("Reimu").GetComponent<ReimuController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reimuc.kill_num >= 5)
        {
            Destroy(GameObject.Find("Wall (14)"));
        }
    }
}
