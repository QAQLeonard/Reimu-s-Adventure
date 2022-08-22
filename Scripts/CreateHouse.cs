using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHouse : MonoBehaviour
{
    public GameObject housePrefab;
    public bool havehouse;

    // Start is called before the first frame update
    void Start()
    {
        havehouse = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         
        if(!havehouse)
         {
            ReimuController e = collision.gameObject.GetComponent<ReimuController>();
            
            Vector2 position = transform.position;
            if (e != null)
            {
                if(e.transform.position.y<position.y)
                {
                    GameObject h=Instantiate(housePrefab, new Vector2(-3.0f,-1.45f), Quaternion.identity);
                    havehouse = true;
                }
            }
         }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("EXIT");
        if (havehouse)
        {
            ReimuController e = collision.gameObject.GetComponent<ReimuController>();
            Vector2 position = transform.position;
            if (e != null)
            {
                if (e.transform.position.y < position.y)
                {
                    //Debug.Log("EXIT");
                    Destroy(GameObject.Find("shrine5(Clone)"));
                    havehouse = false;
                }
            }
        }
    }
}
