using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    static public bool attacking;
    static public int PlayerHP;
    GameObject HP_bar;
    private Rigidbody2D rigid;
    private Animator m_Animator;
    public float speed;
    bool sit;
    public float jumpforce;
    static public bool onGround;
    float InjuryedTime;
    float DefendedTime;
    int PrivateHP;
    public SpriteRenderer[] m_SpriteRenderer = new SpriteRenderer[5];
    public GameObject Game_Over;
    public bool withshield;
    public bool Test;
    bool ForceSit;
    public GameObject[] sound_effect;
    GameObject Cam;
    public bool DontMove;

    // Start is called before the first frame update
    void Start()
    {
        HP_bar = GameObject.Find("Player_HP_bar").transform.GetChild(0).gameObject;
        Cam = GameObject.Find("Main Camera");
        PlayerHP = Test ? 100 : to_next_level.S_PlayerHP;
        PrivateHP = PlayerHP;
        rigid = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        attacking = false;
        InjuryedTime = 0;
        DefendedTime = 0;
        sit = false;
        ForceSit = false;
        m_Animator.SetBool("withshield", withshield);
        DontMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(1).IsName("attack"))
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }

        HP_Count();
        Action();
        RaycastDetect();
        setting.UsedTime += Time.deltaTime;
    }
 
    void Action()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RaycastHit2D hit_R = Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y), Vector2.right, 0.1f, 1 << 6);
            if (hit_R.collider == null && !DontMove)
            {
                transform.position += new Vector3(speed * (sit ? 0.5f : 1f) * Time.deltaTime, 0, 0);
            }
            m_Animator.SetBool("move", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RaycastHit2D hit_L = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y), Vector2.left, 0.1f, 1 << 6);
            if (hit_L.collider == null && !DontMove)
            {
                transform.position += new Vector3(-speed * (sit ? 0.5f : 1f) * Time.deltaTime, 0, 0);
            }
            m_Animator.SetBool("move", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            m_Animator.SetBool("move", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && onGround && !sit)
        {
            rigid.AddForce(new Vector2(0,jumpforce),ForceMode2D.Impulse);
            Instantiate(sound_effect[0], new Vector3(0, 0, 0), Quaternion.identity);
            m_Animator.SetTrigger("jump");
            m_Animator.SetBool("move", false);
        }
        if((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1)) && !sit)
        {
            Instantiate(sound_effect[1], new Vector3(0, 0, 0), Quaternion.identity);
            m_Animator.SetTrigger("attack");
        }
        if ((Input.GetKey(KeyCode.S) && !attacking) || ForceSit)
        {
            sit = true;
            m_Animator.SetBool("sit", true);
        }
        else
        {
            sit = false;
            m_Animator.SetBool("sit", false);
        }
    }

    void HP_Count()
    {
        if (PrivateHP > PlayerHP)
        {
            if (withshield && sit)
            {
                PlayerHP = PrivateHP - ((PrivateHP - PlayerHP) > 10 ? Mathf.FloorToInt((PrivateHP - PlayerHP - 10) * 0.1f) : 0);
                if (DefendedTime == 0)
                {
                    DefendedTime = 0.2f;
                    Instantiate(sound_effect[4], new Vector3(0, 0, 0), Quaternion.identity);
                }
            }
            else if(InjuryedTime == 0)
            {
                InjuryedTime = 0.2f;
                Instantiate(sound_effect[3], new Vector3(0, 0, 0), Quaternion.identity);
            }
            PrivateHP = PlayerHP;
        }
        else if (PrivateHP < PlayerHP)
        {
            if (InjuryedTime == 0)
            {
                InjuryedTime = -0.4f;
                Instantiate(sound_effect[6], new Vector3(0, 0, 0), Quaternion.identity);
            }
            PrivateHP = PlayerHP;
        }

        if (InjuryedTime > 0)
        {
            InjuryedTime -= Time.deltaTime;
            if (InjuryedTime < 0)
            {
                InjuryedTime = 0;
            }
            foreach (SpriteRenderer sprite in m_SpriteRenderer)
            {
                sprite.color = new Vector4(1f, 0.5f * Mathf.Cos(-InjuryedTime * 10 * Mathf.PI) + 0.5f, 0.5f * Mathf.Cos(-InjuryedTime * 10 * Mathf.PI) + 0.5f, 1f);
            }
        }
        else if (InjuryedTime < 0)
        {
            InjuryedTime += Time.deltaTime;
            if (InjuryedTime > 0)
            {
                InjuryedTime = 0;
            }
            foreach (SpriteRenderer sprite in m_SpriteRenderer)
            {
                sprite.color = new Vector4((105f / 255f) * Mathf.Cos(-InjuryedTime * 10 * Mathf.PI) + (45f / 255f + 0.5f), 1f, 0.5f * Mathf.Cos(InjuryedTime * 10 * Mathf.PI) + 0.5f, 1f);
            }
        }
        if (DefendedTime > 0)
        {
            DefendedTime -= Time.deltaTime;
            if (DefendedTime < 0)
            {
                DefendedTime = 0;
            }
            foreach (SpriteRenderer sprite in m_SpriteRenderer)
            {
                sprite.color = new Vector4(1f, 1f, 0.5f * Mathf.Cos(-DefendedTime * 10 * Mathf.PI) + 0.5f, 1f);
            }
        }

        HP_bar.transform.localScale = new Vector3((PlayerHP > 0 ? PlayerHP : 0) / 100f * 5, 0.4f, 1);

        if (PlayerHP <= 0)
        {
            foreach (SpriteRenderer sprite in m_SpriteRenderer)
            {
                sprite.color = new Vector4(1f, 1f, 1f, 1f);
            }
            m_Animator.SetBool("dead", true);
            Instantiate(Game_Over, Cam.transform);
            Instantiate(sound_effect[2], new Vector3(0, 0, 0), Quaternion.identity);
            setting.DeadCount++;
            Destroy(this);
        }
        if (transform.position.y <= -10)
        {
            PlayerHP -= 40;
            PrivateHP -= 40;
            InjuryedTime = 0.2f;
            Instantiate(sound_effect[3], new Vector3(0, 0, 0), Quaternion.identity);
            if (PlayerHP > 0)
            {
                transform.position = new Vector3(-9f, -3.379495f, 0);
            }
        }
    }
    void RaycastDetect()
    {
        RaycastHit2D hit_BL = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y - 0.5f), Vector2.down, 0.1f, 1 << 6);
        RaycastHit2D hit_BR = Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y - 0.5f), Vector2.down, 0.1f, 1 << 6);
        if (hit_BL.collider != null || hit_BR.collider != null)
        {
            onGround = true;
            m_Animator.SetBool("onGround", true);
        }
        else
        {
            onGround = false;
            m_Animator.SetBool("onGround", false);
        }

        RaycastHit2D hit_T = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.72f), Vector2.up, 0.1f, 1 << 6);
        if (hit_T.collider != null && onGround)
        {
            ForceSit = true;
        }
        else
        {
            ForceSit = false;
        }
    }
}
