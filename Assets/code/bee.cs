using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bee : MonoBehaviour
{
    bool move_mod; //0:idle  1:move
    int attack_mod;
    SpriteRenderer m_SpriteRenderer;
    Rigidbody2D rigid;
    GameObject G_player;
    public GameObject HP_bar;
    public GameObject dead;
    float Dx_player;
    float Dy_player;
    float Original_Y;
    float A_C_time; //attack cold time 
    float HP;
    float InjuryedTime;
    public GameObject bullet;
    bee_attack running_bullet;
    public GameObject sound2;
    public float speed;
    public float MaxHP;
    public float damage;
    public float bulletspeed;
    public float colding;

    // Start is called before the first frame update
    void Start()
    {
        attack_mod = 0;
        move_mod = false;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        A_C_time = 1;
        HP = MaxHP;
        InjuryedTime = 0;
        G_player = GameObject.Find("player");
        Dx_player = 10f;
        Dy_player = 10f;
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
            Instantiate(dead, transform.position, Quaternion.identity);
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
    void action()
    {
        if (attack_mod == 0)
        {
            Dx_player = G_player.transform.position.x - transform.position.x;
            Dy_player = G_player.transform.position.y - transform.position.y;

            if (Mathf.Abs(Dx_player) >= 4 && Mathf.Abs(Dx_player) <= 8 && !move_mod)
            {
                move_mod = true;
            }
            else if (Mathf.Abs(Dx_player) > 8 || Mathf.Abs(Dx_player) < 4)
            {
                move_mod = false;
            }

            if (Dx_player >= 0)
            {
                m_SpriteRenderer.flipX = true;
                RaycastHit2D hit_R = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, 1 << 6);
                if (move_mod && hit_R.collider == null)
                {
                    rigid.velocity = new Vector2(speed, rigid.velocity.y);
                }
                else
                {
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                }
            }
            else if (Dx_player < 0)
            {
                m_SpriteRenderer.flipX = false;
                RaycastHit2D hit_L = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, 1 << 6);
                if (move_mod && hit_L.collider == null)
                {
                    rigid.velocity = new Vector2(-speed, rigid.velocity.y);
                }
                else
                {
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                }
            }
            if(A_C_time <= 0 && Mathf.Abs(Dx_player) <= 4)
            {
                attack_mod = 1;
            }
        }
        else if (attack_mod == 1)
        {
            running_bullet = Instantiate(bullet, new Vector3(transform.position.x - 0.4f * (m_SpriteRenderer.flipX ? -1 : 1), transform.position.y - 0.28f, 0), Quaternion.identity).GetComponent<bee_attack>();
            Instantiate(sound2, new Vector3(0, 0, 0), Quaternion.identity);
            running_bullet.damage = damage;
            running_bullet.attackspeed = bulletspeed * bulletspeed;
            A_C_time = colding;
            attack_mod = 0;
        }
    }
}
