using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReimuController : MonoBehaviour
{

    public int currentHealth;
    public int currentMagic;
    public int maxHealth = 40;
    public int maxMagic = 20;

    float speed = 3.0f;

    public float timeInvincible = 1.0f;
    bool isInvincible = false;
    float invincibleTimer;

    public float timeMagic = 2.5f;
    bool isMagic = true;
    float MagicTimer = 0;

    public float timeHealth = 10.0f;
    bool isHealth = true;
    float HealthTimer = 0;

    public float timeRomoteAttackable = 1.0f;
    bool isRomoteAttackable = true;
    float RomoteAttackableTimer;

    Animator animator;
    Vector2 walkDirection = new Vector2(1, 0);

    Vector2 look_dir = new Vector2(1, 0);

    public GameObject projectilePrefab0;
    public GameObject projectilePrefab1;

    float horizontal = 0;
    float vertical = 0;
    Vector2 pos;
    Rigidbody2D ReimuRb;

    public int kill_num;

    void Start()
    {
        ReimuRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = 10;
        currentMagic = 10;
        kill_num = 0;

        if (PlayerPrefs.GetInt("portal_flag") == 1)
        {

            Vector2 tp = new Vector2(PlayerPrefs.GetFloat("pos_x"), PlayerPrefs.GetFloat("pos_y"));
            Debug.Log("Portal" + tp.x + "  " + tp.y);
            transform.position = tp;
            my_Read();
            PlayerPrefs.SetInt("portal_flag", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        move();

        RefreshAnimation();

        Launch();

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        UIMagicBar.instance.SetValue(currentMagic / (float)maxMagic);
    }

    void FixedUpdate()
    {
        Timer();
    }

    void Timer()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
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

        if (isMagic)
        {
            currentMagic = Mathf.Clamp(currentMagic + 1, 0, maxMagic);
            isMagic = false;
            MagicTimer = timeMagic;
        }
        else
        {
            MagicTimer -= Time.deltaTime;
            if (MagicTimer < 0)
            {
                isMagic = true;
            }
        }

        if (isHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + 1, 0, maxHealth);
            isHealth = false;
            HealthTimer = timeHealth;
        }
        else
        {
            HealthTimer -= Time.deltaTime;
            if (HealthTimer < 0)
            {
                isHealth = true;
            }
        }

    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth += amount;
        if(currentHealth<=0) SceneManager.LoadScene("Die");

    }

    void RefreshAnimation()
    {

        if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f))
        {
            walkDirection.Set(horizontal, vertical);
            walkDirection.Normalize();//��������ģ��Ϊ1
            animator.SetFloat("Walk X", horizontal);
            animator.SetFloat("Walk Y", vertical);
        }

        if (Mathf.Approximately(horizontal, 0) && Mathf.Approximately(vertical, 0))
        {
            animator.SetFloat("Speed", 0);
            //Debug.Log("IDEL");
        }
        else
        {
            animator.SetFloat("Speed", speed);
            //Debug.Log("WALK");
        }
    }

    void Launch()
    {
        float alpha = 20 * Mathf.PI / 180;
        if (Input.GetKeyDown(KeyCode.Z))//�������Ƿ���ĳ������������Ұ���ĳ����ʱ���� Launch
        {
            //Debug.Log("GetKeyZDown");
            if (!isRomoteAttackable)
            {
                return;
            }
            if (currentMagic < 3)
            {
                Debug.Log("No Magic");
                return;
            }

            currentMagic -= 3;

            Vector2 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            look_dir.x = mousePositionInWorld.x - transform.position.x;
            look_dir.y = mousePositionInWorld.y - transform.position.y;
            
            Vector2 shoot_dir = new Vector2(look_dir.x * Mathf.Cos(2 * alpha) - look_dir.y * Mathf.Sin(2 * alpha), look_dir.y * Mathf.Cos(2 * alpha) + look_dir.x * Mathf.Sin(2 * alpha));
            shoot_dir.Normalize();

            for (int i = 0; i < 5; i++)
            {
                GameObject projectileObject = Instantiate(projectilePrefab0, ReimuRb.position + Vector2.up * 0.5f, Quaternion.identity);
                /*��Ҫ���ƵĶ�����Ԥ�Ƽ���Ȼ�󽫸ö�������ڸ����λ�ã�����������ƫ��һ�㣬�øö��󿿽� Ruby ��˫�֣�������˫�ţ�������ת Quaternion.identity*/
                /*��Ԫ�� (Quaternion) ��һ�ֿ��Ա����ת����ѧ�������������ֻ��Ҫ��ס���ǣ�Quaternion.identity ��ʾ������ת��*/
                RemoteAttack1 projectile = projectileObject.GetComponent<RemoteAttack1>();//�Ӹ��¶����ȡ Projectile �ű�
                projectile.Launch(shoot_dir, 180);//��������ǰ��д�� Launch ���������еķ����ǽ�ɫĿ�ӵķ�����ֵ�ֶ�����Ϊ 300
                float temp_x = shoot_dir.x * Mathf.Cos(alpha) + shoot_dir.y * Mathf.Sin(alpha);
                float temp_y = shoot_dir.y * Mathf.Cos(alpha) - shoot_dir.x * Mathf.Sin(alpha);
                shoot_dir.x = temp_x;
                shoot_dir.y = temp_y;
                shoot_dir.Normalize();
            }

            isRomoteAttackable = false;
            RomoteAttackableTimer = timeRomoteAttackable;
        }
        else if (Input.GetKeyDown(KeyCode.X))//�������Ƿ���ĳ������������Ұ���ĳ����ʱ���� Launch
        {
            if (!isRomoteAttackable)
            {
                return;
            }
            if (currentMagic < 3)
            {
                Debug.Log("No Magic");
                return;
            }

            currentMagic -= 3;
            GameObject projectileObject = Instantiate(projectilePrefab1, ReimuRb.position + Vector2.up * 0.5f, Quaternion.identity);
            /*��Ҫ���ƵĶ�����Ԥ�Ƽ���Ȼ�󽫸ö�������ڸ����λ�ã�����������ƫ��һ�㣬�øö��󿿽� Ruby ��˫�֣�������˫�ţ�������ת Quaternion.identity*/
            /*��Ԫ�� (Quaternion) ��һ�ֿ��Ա����ת����ѧ�������������ֻ��Ҫ��ס���ǣ�Quaternion.identity ��ʾ������ת��*/

            RemoteAttack2 projectile = projectileObject.GetComponent<RemoteAttack2>();//�Ӹ��¶����ȡ Projectile �ű�

            Vector2 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            look_dir.x = mousePositionInWorld.x - transform.position.x;
            look_dir.y = mousePositionInWorld.y - transform.position.y;
            look_dir.Normalize();

            projectile.Launch(look_dir, 300);//��������ǰ��д�� Launch ���������еķ����ǽ�ɫĿ�ӵķ�����ֵ�ֶ�����Ϊ 300

            //animator.SetTrigger("Launch");//Ϊ��� Animator �����˴���������ˣ�Animator ���Բ��ŷ��䶯����
            isRomoteAttackable = false;
            RomoteAttackableTimer = timeRomoteAttackable;
        }
    }


    void move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        pos = transform.position;
        Vector2 dir = new Vector2(horizontal, vertical);
        dir.Normalize();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 5.0f;
            Debug.Log("shift");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 3.0f;
            Debug.Log("shiftup");
        }

        Vector2 position = transform.position;
        position.x = position.x + dir.x * Time.deltaTime * speed;
        position.y = position.y + dir.y * Time.deltaTime * speed;
        transform.position = position;

        ReimuRb.MovePosition(position);
    }

    public void my_Save()
    {
        PlayerPrefs.SetInt("currentHealth", currentHealth);
        PlayerPrefs.SetInt("currentMagic", currentMagic);
        PlayerPrefs.SetInt("maxHealth", maxHealth);
        PlayerPrefs.SetInt("maxMagic", maxMagic);
    }

    public void my_Read()
    {
        currentHealth = PlayerPrefs.GetInt("currentHealth");
        currentMagic = PlayerPrefs.GetInt("currentMagic");
        maxHealth = PlayerPrefs.GetInt("maxHealth");
        maxMagic = PlayerPrefs.GetInt("maxMagic");
    }

}