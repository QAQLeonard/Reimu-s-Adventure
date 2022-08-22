using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteAttack1 : MonoBehaviour
{
    Rigidbody2D rb_ra1;

    public int damage;

    void Awake()
    {
        rb_ra1 = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("RemoteAttack"), LayerMask.NameToLayer("RemoteAttack"), true);
        damage = 2;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, -5);
    }
    public void Launch(Vector2 direction, float force)
    {
        rb_ra1.AddForce(direction * force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        LittleMonsterController controller = collision.gameObject.GetComponent<LittleMonsterController>();
        CirnoController controller0= collision.gameObject.GetComponent<CirnoController>();
        RemiliaController controller1 = collision.gameObject.GetComponent<RemiliaController>();
        FlandreController controller2 = collision.gameObject.GetComponent<FlandreController>();
        BatController controller3= collision.gameObject.GetComponent<BatController>();
        
        if (controller != null)
        {
            controller.ChangeHealth(-damage);
        }
        else if (controller0 != null)
        {
            controller0.ChangeHealth(-damage);
        }
        else if (controller1 != null)
        {
            controller1.ChangeHealth(-damage);
        }
        else if (controller2 != null)
        {
            controller2.ChangeHealth(-damage);
        }
        else if (controller3 != null)
        {
            controller3.ChangeHealth(-damage);
        }

        Destroy(gameObject);
    }
}
