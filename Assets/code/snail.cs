using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snail : MonoBehaviour
{
    int mod = 0; //0:idle  1:move  2:defend
    Rigidbody2D rigid;
    SpriteRenderer m_SpriteRenderer;
    GameObject G_player;
    Animator m_Ani;
    public GameObject HP_bar;
    public GameObject dead;
    float D_player;
    float A_C_time; //attack cold time 
    float HP;
    float InjuryedTime;
    public GameObject sound;
    public GameObject sound2;
    float soundtime;
    public float speed;
    public float MaxHP;
    public int damage;
    public float DefendGravity;

    // Start is called before the first frame update
    void Start()
    {
        mod = 0;
        rigid = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Ani = GetComponent<Animator>();
        A_C_time = 0;
        HP = MaxHP;
        InjuryedTime = 0;
        G_player = GameObject.Find("player");
        soundtime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (InjuryedTime > 0)
        {
            InjuryedTime -= Time.deltaTime;
            if (InjuryedTime < 0)
            {
                InjuryedTime = 0;
            }
            m_SpriteRenderer.color = new Vector4(1f, 0.5f * Mathf.Cos(-InjuryedTime * 10 * Mathf.PI) + 0.5f, 0.5f * Mathf.Cos(-InjuryedTime * 10 * Mathf.PI) + 0.5f, 1f);
        }

        if (A_C_time > 0)
        {
            A_C_time -= Time.deltaTime;
        }
        HP_bar.transform.localScale = new Vector3((HP > 0 ? HP : 0) / MaxHP * 5f, 0.4f, 1);
        action();
        if (HP <= 0)
        {
            Instantiate(dead, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (transform.position.y < -10)
        {
            HP = 0;
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (A_C_time <= 0 && mod != 2)
        {
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.collider.name == "player")
                {
                    player.PlayerHP -= damage;
                    A_C_time = 1f;
                    break;
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Weapon" && player.attacking && InjuryedTime <= 0 && mod != 2)
        {
            HP -= 15;
            InjuryedTime = 0.2f;
        }
    }
    void action()
    {
        D_player = Mathf.Abs(G_player.transform.position.x - transform.position.x);
        if (rigid.gravityScale == DefendGravity && mod != 2)
        {
            Instantiate(sound2, new Vector3(0, 0, 0), Quaternion.identity);
            mod = 2;
            m_Ani.SetBool("defend", true);
            m_Ani.SetBool("move", false);
        }
        else  if (D_player <= 6 && rigid.gravityScale != DefendGravity)
        {
            mod = 1;
            m_Ani.SetBool("defend", false);
            m_Ani.SetBool("move", true);
        }
        else if (D_player > 6 && rigid.gravityScale != DefendGravity)
        {
            mod = 0;
            m_Ani.SetBool("defend", false);
            m_Ani.SetBool("move", false);
        }

        if (mod == 1)
        {
            soundtime += Time.deltaTime;
            if (soundtime >= 1f)
            {
                soundtime = 0;
                Instantiate(sound, new Vector3(0, 0, 0), Quaternion.identity);
            }
            if (G_player.transform.position.x > transform.position.x)
            {
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
                m_SpriteRenderer.flipX = true;
            }
            else if (G_player.transform.position.x < transform.position.x)
            {
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
                m_SpriteRenderer.flipX = false;
            }
        }

    }
}
