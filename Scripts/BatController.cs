using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public int currentHealth = 1;
    public int maxHealth = 1;

    public float speed = 1.0f;

    public float timeInvincible = 1.0f;
    bool isInvincible = false;
    float invincibleTimer;

    public Vector2 start;
    public Vector2 end;

    Vector2 player_pos;
    Vector2 player_dir;

    Animator animator;

    Rigidbody2D rb_bc;

    void Awake()
    {
        rb_bc = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        start = transform.position;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        player_pos = GameObject.Find("Reimu").GetComponent<Transform>().position;

            player_dir.x = player_pos.x - transform.position.x;
            player_dir.y = player_pos.y - transform.position.y;
            player_dir.Normalize();
    }

    private void FixedUpdate()
    {
        Timer();
        track();
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                Debug.Log("invincible");
                return;
            }


            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth += amount;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            ReimuController controller = GameObject.Find("Reimu").GetComponent<ReimuController>();
            controller.kill_num++;
            Destroy(gameObject);
        }
    }

    void track()
    {
        start = transform.position;
        end.x = start.x + player_dir.x * speed * Time.fixedDeltaTime;
        end.y = start.y + player_dir.y * Time.fixedDeltaTime * speed;
        transform.position = end;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ReimuController controller = collision.gameObject.GetComponent<ReimuController>();
        if (controller != null)
        {
            controller.ChangeHealth(-5);
            Destroy(gameObject);
        }
    }

    void Timer()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.fixedDeltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rb_bc.AddForce(direction * force);
    }
}
