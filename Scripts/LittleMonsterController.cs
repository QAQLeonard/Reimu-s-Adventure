using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class LittleMonsterController : MonoBehaviour
{

    public int currentHealth = 5;
    public int maxHealth = 5;

    public float speed = 1.0f;

    public float timeInvincible = 1.0f;
    bool isInvincible = false;
    float invincibleTimer;

    float DieTimer = 1.0f;
    bool died = false;


    public Vector2 start;
    public Vector2 end;

    Vector2 player_pos;
    Vector2 player_dir;

    Animator animator;

    //public float timeInvincible = 1.0f;
    bool wanderFlag = false;
    float wanderTime = 0;
    float wanderTimer = 0;
    Vector2 wanderDirection;

    bool AttackFlag = false;//标记有没有发现敌人

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        start = transform.position;
        CreateRandomWander();
        currentHealth = maxHealth;
        Debug.Log(DieTimer);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(DieTimer);
        player_pos = GameObject.Find("Reimu").GetComponent<Transform>().position;
        if (AttackFlag)
        {
            player_dir.x = player_pos.x - transform.position.x;
            player_dir.y = player_pos.y - transform.position.y;
            player_dir.Normalize();

            if (Vector2.Distance(transform.position, player_pos) > 14)
            {
                AttackFlag = false;
                speed = 1.0f;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, player_pos) < 10)
            {
                AttackFlag = true;
                speed = 3.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        Timer();
        if (!died)
        {
            if (!AttackFlag)
            {
                wander();
            }
            else
            {
                track();
            }
        }
    }

    void CreateRandomWander()
    {
        float Angle = Random.Range(0, 360);
        wanderTime = (float)Random.Range(0, 100) / 100 + 2;
        wanderDirection = new Vector2(Mathf.Cos(Angle * Mathf.PI / 180), Mathf.Sin(Angle * Mathf.PI / 180));
        wanderDirection.Normalize();
        wanderTimer = wanderTime;
        wanderFlag = true;
        
        animator.SetFloat("speed", speed);
        //Debug.Log(wanderTime+" "+wanderDirection);
    }

    void wander()
    {
        if (wanderFlag)
        {
            wanderTimer -= Time.fixedDeltaTime;
            start = transform.position;
            end.x = start.x + wanderDirection.x * speed * Time.fixedDeltaTime;
            end.y = start.y + wanderDirection.y * Time.fixedDeltaTime * speed;
            transform.position = end;

            if (wanderTimer <= 0)
            {
                wanderTimer = wanderTime;
                wanderFlag = false;
                animator.SetFloat("speed", 0);
            }

            if (wanderDirection.x >= 0) animator.SetFloat("x", 1.0f);
            else animator.SetFloat("x", 0);
        }
        else
        {
            wanderTimer -= Time.deltaTime;
            if (wanderTimer <= 0)
            {
                CreateRandomWander();

            }
        }
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
            animator.SetTrigger("Died");
            died = true;
            ReimuController controller = GameObject.Find("Reimu").GetComponent<ReimuController>();
            controller.kill_num++;
        }
    }

    void track()
    {
        start = transform.position;
        end.x = start.x + player_dir.x * speed * Time.fixedDeltaTime;
        end.y = start.y + player_dir.y * Time.fixedDeltaTime * speed;
        transform.position = end;
        animator.SetFloat("speed", speed);

        if (end.x >= start.x) animator.SetFloat("x", 1.0f);
        else animator.SetFloat("x", 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ReimuController controller = collision.gameObject.GetComponent<ReimuController>();


        if (controller != null)
        {
            controller.ChangeHealth(-2);
            
            animator.SetTrigger("Attack");
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

        if (died)
        {
            DieTimer -= Time.fixedDeltaTime;
            if (DieTimer < 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
