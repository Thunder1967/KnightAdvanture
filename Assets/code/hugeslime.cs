using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hugeslime : MonoBehaviour
{
    int mod = 0; 
    Rigidbody2D rigid;
    public SpriteRenderer m_SpriteRenderer;
    Animator m_ani;
    GameObject G_player;
    player P_player;
    public GameObject HP_bar;
    public GameObject bullet1;
    public GameObject Summoned;
    public GameObject Summoned2;
    public GameObject Summoned3;
    public GameObject dead;
    public GameObject Land1;
    public GameObject Land2;
    public Animator BackGround;
    public gravity Gravity;
    public GameObject[] sound_effect;
    float D_player;
    float A_C_time; //attack cold time
    float StaticTime;
    int DoingMod;
    int idle;
    float FPT; //FreezePlayerTime
    float HP;
    float InjuryedTime;
    public float MaxHP;
    bool OneKill;
    bool HalfHP;

    // Start is called before the first frame update
    void Start()
    {
        mod = 0;
        rigid = GetComponent<Rigidbody2D>();
        m_ani = GetComponent<Animator>();
        A_C_time = 1f;
        StaticTime = 0.8f;
        FPT = 0;
        HP = MaxHP;
        InjuryedTime = 0;
        G_player = GameObject.Find("player");
        P_player = G_player.GetComponent<player>();
        OneKill = false;
        HalfHP = false;
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
            m_SpriteRenderer.color = new Vector4(1f, 0.125f * Mathf.Cos(InjuryedTime * 10 * Mathf.PI) + 0.875f, 0.125f * Mathf.Cos(InjuryedTime * 10 * Mathf.PI) + 0.875f, 1f);
        }

        HP_bar.transform.localScale = new Vector3((HP > 0 ? HP : 0) / MaxHP * 5f, 0.4f, 1);

        if(!HalfHP && HP < MaxHP * 0.5f)
        {
            HalfHP = true;
        }


        if (FPT > 0)
        {
            P_player.DontMove = true;
            FPT -= Time.deltaTime;
        }
        else
        {
            P_player.DontMove = false;
        }


        if (StaticTime > 0)
        {
            if (transform.position.x > 26.7f)
            {
                rigid.velocity = new Vector2(0, 0);
                transform.position = new Vector3(26.7f, transform.position.y, 0);
            }
            else if (transform.position.x < -7.75f)
            {
                rigid.velocity = new Vector2(0, 0);
                transform.position = new Vector3(-7.75f, transform.position.y, 0);
            }
            StaticTime -= Time.deltaTime;
        }
        else
        {
            action();
        }
        if (HP <= 0)
        {
            Instantiate(dead, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Weapon" && player.attacking && InjuryedTime <= 0)
        {
            HP -= 15;
            InjuryedTime = 0.2f;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "player")
        {

            if(other.gameObject.GetComponent<player>() && this.enabled)
            {
                player.PlayerHP -= OneKill ? 1010 : 10;
                if (Mathf.Abs(other.gameObject.transform.position.x - transform.position.x) > 1)
                {
                    other.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(10f * (other.gameObject.transform.position.x > transform.position.x ? 1f : -1f), 0);
                    FPT = 0.5f;
                }
            }

        }
    }

    void action()
    {
        if (A_C_time > 0)
        {
            A_C_time -= Time.deltaTime;
            if (idle == 0)
            {
                idle = transform.position.y > -2f ? 1 : 2;
            }
            if (idle == 1)
            {
                rigid.velocity = new Vector2(0, -4f);
                if (transform.position.y < -2f)
                {
                    transform.position = new Vector3(transform.position.x, -2f, 0);
                    rigid.velocity = new Vector2(0, 0);
                    m_ani.SetBool("DontAct", false);
                    idle = 3;
                }
            }
            else if(idle == 2)
            {
                rigid.velocity = new Vector2(0, 4f);
                if (transform.position.y > -2f)
                {
                    transform.position = new Vector3(transform.position.x, -2f, 0);
                    rigid.velocity = new Vector2(0, 0);
                    m_ani.SetBool("DontAct", false);
                    idle = 3;
                }
            }
            else
            {
                m_ani.SetBool("DontAct", false);
            }
        }
        else
        {
            switch (mod)
            {


                case 0:


                    mod = Mathf.CeilToInt(Random.Range(0f, 7f)); ////////////////////////////////////////////////////////////////////////////////////////////attack
                    DoingMod = 0;
                    if (mod != 1)
                    {
                        m_ani.SetBool("DontAct", true);
                    }
                    idle = 0;
                    break;



                case 1:


                        A_C_time = 1f + (HalfHP ? 0f : 2f);
                        mod = 0;
                    break;


                case 2:


                    if (DoingMod == 0)
                    {
                        m_ani.SetTrigger("Jump");
                        DoingMod++;
                    }
                    else if (DoingMod == 1 && m_ani.GetCurrentAnimatorStateInfo(0).IsName("Boss_jump"))
                    {
                        DoingMod++;
                    }
                    else if (DoingMod == 2 && m_ani.GetCurrentAnimatorStateInfo(0).IsName("null"))
                    {
                        rigid.velocity = new Vector2(0, 80f);
                        DoingMod++;
                    }
                    else if (DoingMod == 3 && transform.position.y > 100)
                    {
                        if (G_player.transform.position.x > 26.7f)
                        {
                            transform.position = new Vector3(26.7f, 100f, 0);
                        }
                        else if (G_player.transform.position.x < -7.75f)
                        {
                            transform.position = new Vector3(-7.75f, 100f, 0);
                        }
                        else
                        {
                            transform.position = new Vector3(G_player.transform.position.x, 100f, 0);
                        }

                        rigid.velocity = new Vector2(0, -150f);
                        DoingMod++;
                    }
                    else if (DoingMod == 4 && transform.position.y < -3.9f)
                    {
                        Instantiate(sound_effect[0], new Vector3(0, 0, 0), Quaternion.identity);
                        transform.position = new Vector3(transform.position.x, -3.9f, 0);
                        rigid.velocity = new Vector2(0, 0);
                        if (Mathf.Abs(G_player.transform.position.x - transform.position.x) < 2)
                        {
                            player.PlayerHP -= 1010;
                        }
                        StaticTime = 0.8f;
                        A_C_time = 1f + (HalfHP ? 0f : 1f);
                        mod = 0;
                    }
                    break;


                case 3:


                    if(DoingMod == 0)
                    {
                        rigid.velocity = new Vector2(0, -2f);
                        StaticTime = 0.3f;
                        DoingMod++;
                    }
                    else if(DoingMod == 1)
                    {
                        rigid.velocity = new Vector2(0, 0);
                        m_ani.SetTrigger("Shake");
                        DoingMod++;
                    }
                    else if (DoingMod == 2 && m_ani.GetCurrentAnimatorStateInfo(0).IsName("Boss_shake"))
                    {
                        DoingMod++;
                    }
                    else if (DoingMod == 3 && m_ani.GetCurrentAnimatorStateInfo(0).IsName("null"))
                    {
                        Instantiate(sound_effect[1], new Vector3(0, 0, 0), Quaternion.identity);
                        rigid.velocity = new Vector2(100f * (G_player.transform.position.x > transform.position.x ? 1 : -1), 0);
                        StaticTime = 0.2f;
                        DoingMod++;
                        OneKill = true;
                    }
                    else if (DoingMod == 4)
                    {
                        rigid.velocity = new Vector2(0, 0);
                        StaticTime = 0.4f;
                        A_C_time = 2f + (HalfHP ? 0f : 1f);
                        mod = 0;
                        OneKill = false;
                    }
                    break;

                case 4:


                    if (DoingMod == 0)
                    {
                        Instantiate(bullet1, new Vector3(transform.position.x + (G_player.transform.position.x > transform.position.x ? 2f : -2f), transform.position.y + 5f, 0), Quaternion.identity).GetComponent<Bossbullet1>().face = G_player.transform.position.x > transform.position.x;
                        StaticTime = 0.2f;
                        DoingMod++;
                    }
                    else if(DoingMod == 1)
                    {
                        Instantiate(bullet1, new Vector3(transform.position.x + (G_player.transform.position.x > transform.position.x ? 2f : -2f), transform.position.y + 6f, 0), Quaternion.identity).GetComponent<Bossbullet1>().face = G_player.transform.position.x > transform.position.x;
                        StaticTime = 0.2f;
                        DoingMod++;
                    }
                    else if (DoingMod == 2)
                    {
                        Instantiate(bullet1, new Vector3(transform.position.x + (G_player.transform.position.x > transform.position.x ? 2f : -2f), transform.position.y + 7f, 0), Quaternion.identity).GetComponent<Bossbullet1>().face = G_player.transform.position.x > transform.position.x;
                        StaticTime = 0.2f;
                        DoingMod++;
                    }
                    else if(DoingMod == 3)
                    {
                        StaticTime = 1.5f;
                        A_C_time = 2f + (HalfHP ? 0f : 1f);
                        mod = 0;
                    }
                    break;


                case 5:


                    if(DoingMod == 0)
                    {
                        m_ani.SetTrigger("Shake_up");
                        Instantiate(sound_effect[2], new Vector3(0, 0, 0), Quaternion.identity);
                        StaticTime = 0.5f;
                        DoingMod++;
                    }
                    else if (DoingMod == 1)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Instantiate(Summoned, transform.position + new Vector3(0, 2f, 0), Quaternion.identity).GetComponent<slime_enable>().enabled = true;
                        }
                        StaticTime = 0.8f;
                        A_C_time = 3.5f + (HalfHP ? 0f : 1f);
                        mod = 0;
                    }
                    break;


                case 6:


                    if (DoingMod == 0)
                    {
                        Land1.GetComponent<Animator>().SetTrigger("Shake");
                        Land2.GetComponent<Animator>().SetTrigger("Shake");
                        BackGround.SetTrigger("Shake");
                        Instantiate(sound_effect[3], new Vector3(0, 0, 0), Quaternion.identity);
                        Gravity.ForceChange(Mathf.CeilToInt(Random.Range(0, 2f)), 10f);
                        StaticTime = 2.5f;
                        DoingMod++;
                    }
                    else if (DoingMod == 1)
                    {
                        Land1.transform.position = new Vector3(G_player.transform.position.x + 34.5f, -4.5f, 0);
                        Land2.transform.position = new Vector3(G_player.transform.position.x - 34.5f, -4.5f, 0);
                        StaticTime = 1f;
                        DoingMod++;
                    }
                    else if (DoingMod == 2)
                    {
                        Land1.transform.position = new Vector3(G_player.transform.position.x + 32, -4.5f, 0);
                        Land2.transform.position = new Vector3(G_player.transform.position.x - 32, -4.5f, 0);
                        A_C_time = 3f + (HalfHP ? 0f : 1.5f);
                        mod = 0;
                    }
                    break;


                case 7:


                    if(DoingMod == 0)
                    {
                        Destroy(Instantiate(Summoned3, transform.position + new Vector3(0, 2f, 0), Quaternion.identity), 1.2f);
                        Instantiate(sound_effect[4], new Vector3(0, 0, 0), Quaternion.identity);
                        for (int i = 0; i < 100; i++)
                        {
                            Instantiate(Summoned2, new Vector3(0, -10f, 0), Quaternion.identity);
                        }
                        StaticTime = 12f;
                        A_C_time = 2f + (HalfHP ? 0f : 2f);
                        mod = 0;
                    }
                    break;


                default:
                    break;


            }
        }
        
    }
}
