using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : MonoBehaviour
{
    int mod = 0; //0:idle  1:move_start 2:move_aboveGround  3:moving   4:move_end   
    Rigidbody2D rigid;
    public SpriteRenderer m_SpriteRenderer;
    public Sprite idle;
    public Sprite jump;
    GameObject G_player;
    public GameObject HP_bar;
    public GameObject dead;
    float D_player;
    float A_C_time; //attack cold time 
    float movecolding;
    float HP;
    float InjuryedTime;
    bool onGround;
    float X_speed;
    public GameObject sound;
    public float max_Displacement;
    public float max_seeing;
    public float MaxHP;
    public int damage;
    public float JumpSpeed;
    public float move_cold;

    // Start is called before the first frame update
    void Start()
    {
        mod = 0;
        rigid = GetComponent<Rigidbody2D>();
        A_C_time = 0;
        movecolding = 0;
        HP = MaxHP;
        InjuryedTime = 0;
        G_player = GameObject.Find("player");
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
        if (A_C_time <= 0)
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
        if (other.gameObject.name == "Weapon" && player.attacking && InjuryedTime <= 0)
        {
            HP -= 15;
            InjuryedTime = 0.2f;
        }
    }
    void action()
    {
        D_player = Mathf.Abs(G_player.transform.position.x - transform.position.x);
        if (mod == 0 || mod == 4)
        {
            if (G_player.transform.position.x > transform.position.x)
            {
                m_SpriteRenderer.flipX = true;
            }
            else if (G_player.transform.position.x < transform.position.x)
            {
                m_SpriteRenderer.flipX = false;
            }
        }

        if (D_player <= max_seeing && D_player > 0.5f && mod == 0 && onGround)
        {
            mod = 1;
            X_speed = (rigid.gravityScale * 9.81f * (D_player > max_Displacement ? max_Displacement : D_player)) / (2 * JumpSpeed);
        }
        else if (mod == 1)
        {
            if (m_SpriteRenderer.flipX)
            {
                rigid.velocity = new Vector2(X_speed, JumpSpeed);
            }
            else
            {
                rigid.velocity = new Vector2(-X_speed, JumpSpeed);
            }
            Instantiate(sound, new Vector3(0, 0, 0), Quaternion.identity);
            mod = 2;
        }
        else if(mod == 2 && !onGround)
        {
            mod = 3;
            m_SpriteRenderer.sprite = jump;
        }
        else if(mod == 2 && rigid.velocity.y == 0)
        {
            mod = 0;
        }
        else if(mod == 3 && onGround)
        {
            rigid.velocity = new Vector2(0, 0);
            movecolding = move_cold;
            m_SpriteRenderer.sprite = idle;
            mod = 4;
        }
        else if(mod == 4)
        {
            movecolding -= Time.deltaTime;
            if (movecolding <= 0)
            {
                mod = 0;
            }
        }

        RaycastHit2D hit_BL = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.65f), Vector2.down, 0.1f, 192);
        RaycastHit2D hit_BR = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y - 0.65f), Vector2.down, 0.1f, 192);
        if (hit_BL.collider != null || hit_BR.collider != null)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }
}
