using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CirnoController : MonoBehaviour
{
    public int currentHealth = 30;
    public int maxHealth = 30;

    public float speed = 1.0f;

    public float timeInvincible = 1.0f;
    bool isInvincible = false;
    float invincibleTimer;

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

    bool AttackFlag = false;//�����û�з��ֵ���

    public GameObject projectilePrefab0;

    public float timeRomoteAttackable = 1.0f;
    bool isRomoteAttackable = true;
    float RomoteAttackableTimer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        start = transform.position;
        CreateRandomWander();
        currentHealth = maxHealth;
        player_pos = GameObject.Find("Reimu").GetComponent<Transform>().position;
        player_dir.x = player_pos.x - transform.position.x;
        player_dir.y = player_pos.y - transform.position.y;
        player_dir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(DieTimer);
        player_pos = GameObject.Find("Reimu").GetComponent<Transform>().position;
        player_dir.x = player_pos.x - transform.position.x;
        player_dir.y = player_pos.y - transform.position.y;
        player_dir.Normalize();
        Debug.Log(player_dir);
        if (!AttackFlag)
        {
            if (Vector2.Distance(transform.position, player_pos) < 20)
            {
                AttackFlag = true;
                speed = 1.5f;
            }
        }
    }

    private void FixedUpdate()
    {
        Timer();

        if (!AttackFlag)
        {
            wander();
        }
        else
        {
            track();
            Launch();
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
            animator.SetFloat("x", wanderDirection.x);
            animator.SetFloat("y", wanderDirection.y);


            if (wanderTimer <= 0)
            {
                wanderTimer = wanderTime;
                wanderFlag = false;
            }
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

            ReimuController controller = GameObject.Find("Reimu").GetComponent<ReimuController>();
            controller.kill_num++;
            Destroy(GameObject.Find("Wall (13)"));
            Destroy(gameObject);

        }
    }

    void track()
    {
        start = transform.position;
        end.x = start.x + player_dir.x * speed * Time.fixedDeltaTime;
        end.y = start.y + player_dir.y * Time.fixedDeltaTime * speed;
        transform.position = end;

        animator.SetFloat("x", player_dir.x);
        animator.SetFloat("y", player_dir.y);
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

        if (!isRomoteAttackable)
        {
            RomoteAttackableTimer -= Time.deltaTime;
            if (RomoteAttackableTimer < 0)
            {
                isRomoteAttackable = true;
            }
        }
    }

    void Launch()
    {

        if (!isRomoteAttackable)
        {
            return;
        }

        GameObject projectileObject = Instantiate(projectilePrefab0, transform.position + new Vector3(0, 0.65f, 0), Quaternion.identity);
        /*��Ҫ���ƵĶ�����Ԥ�Ƽ���Ȼ�󽫸ö�������ڸ����λ�ã�����������ƫ��һ�㣬�øö��󿿽� Ruby ��˫�֣�������˫�ţ�������ת Quaternion.identity*/
        /*��Ԫ�� (Quaternion) ��һ�ֿ��Ա����ת����ѧ�������������ֻ��Ҫ��ס���ǣ�Quaternion.identity ��ʾ������ת��*/

        CirnoAttackC projectile = projectileObject.GetComponent<CirnoAttackC>();//�Ӹ��¶����ȡ Projectile �ű�

        projectile.Launch(player_dir, 300);//��������ǰ��д�� Launch ���������еķ����ǽ�ɫĿ�ӵķ�����ֵ�ֶ�����Ϊ 300

        //animator.SetTrigger("Launch");//Ϊ��� Animator �����˴���������ˣ�Animator ���Բ��ŷ��䶯����
        isRomoteAttackable = false;
        RomoteAttackableTimer = timeRomoteAttackable;
    }
}