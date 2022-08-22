using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirnoAttackC : MonoBehaviour
{
    Rigidbody2D rb_ca;

    public int damage;

    void Awake()
    {
        rb_ca = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        damage = 3;
    }
    // Update is called once per frame
    public void Launch(Vector2 direction, float force)
    {
        transform.Rotate(0, 0, -90);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.Rotate(0, 0, angle);


        rb_ca.AddForce(direction * force);
        Debug.Log(force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        ReimuController controller = collision.gameObject.GetComponent<ReimuController>();
        if (controller != null)
        {
            controller.ChangeHealth(-2);
        }
        Destroy(gameObject);
    }
}
