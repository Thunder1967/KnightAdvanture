using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandbug : MonoBehaviour
{
    int mod;// 0:idle  1:attack_up  2:bite  3:attack_down  4:colding  5:attackhit  6:attack_prepare
    public SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer[] HP_SpriteRenderer;
    GameObject G_player;
    public GameObject HP_bar;
    public GameObject dead;
    float A_time; //attack time 
    float A_C_time;//attack cold time
    float HP;
    float InjuryedTime;
    public Sprite[] sp;
    float originalPos;
    bool nohit;
    public GameObject sound;
    public float MaxHP;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        A_time = 0;
        A_C_time = 0;
        HP = MaxHP;
        InjuryedTime = 0;
        G_player = GameObject.Find("player");
        mod = 0;
        nohit = true;
        HP_SpriteRenderer[0].color = new Vector4(0.1529412f, 0.9176471f, 0f, 0f);
        HP_SpriteRenderer[1].color = new Vector4(1f, 0f, 0.009400845f, 0f);
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

        HP_bar.transform.localScale = new Vector3((HP > 0 ? HP : 0) / MaxHP * 5f, 0.4f, 1);
        if (HP <= 0)
        {
            Instantiate(dead, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }

        attack();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Weapon" && player.attacking && !nohit && InjuryedTime <= 0)
        {
            HP -= 15;
            InjuryedTime = 0.2f;
        }
    }
    
    void attack()
    {
        if (mod == 0 && Mathf.Abs(G_player.transform.position.x - this.transform.position.x) < 1.5f && G_player.transform.position.y - transform.position.y <= 4f && G_player.transform.position.y - transform.position.y > 0)
        {
            mod = 6;
            A_time = 0.2f;
            originalPos = transform.position.y;
        }
        else if (mod == 6)
        {
            if (A_time > 0)
            {
                A_time -= Time.deltaTime;
            }
            else
            {
                nohit = false;
                HP_SpriteRenderer[0].color = new Vector4(0.1529412f, 0.9176471f, 0f, 1f);
                HP_SpriteRenderer[1].color = new Vector4(1f, 0f, 0.009400845f, 1f);
                mod = 1;
                A_time = 0.2f;
                Instantiate(sound, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
        else if (mod == 1)
        {
            if (A_time > 0)
            {
                A_time -= Time.deltaTime;
                transform.position += new Vector3(0, 5 * Time.deltaTime, 0);
            }
            else{
                mod = 2;
                A_time = 0.4f;
                m_SpriteRenderer.sprite = sp[1];
                transform.position = new Vector3(transform.position.x, originalPos + 1f, 0); 
            }
        }
        else if (mod == 2)
        {
            if (A_time > 0)
            {
                A_time -= Time.deltaTime;
            }
            else
            {
                mod = 3;
                A_time = 0.5f;
            }
        }
        else if (mod == 3)
        {
            if (A_time > 0)
            {
                A_time -= Time.deltaTime;
                transform.position -= new Vector3(0, 2 * Time.deltaTime, 0);
            }
            else
            {
                m_SpriteRenderer.sprite = sp[0];
                transform.position = new Vector3(transform.position.x, originalPos, 0);
                mod = 4;
                A_time = 0.5f;
                nohit = true;
                HP_SpriteRenderer[0].color = new Vector4(0.1529412f, 0.9176471f, 0f, 0f);
                HP_SpriteRenderer[1].color = new Vector4(1f, 0f, 0.009400845f, 0f);
            }
        }
        else if (mod == 4)
        {
            if (A_time > 0)
            {
                A_time -= Time.deltaTime;
            }
            else
            {
                mod = 0;
            }
        }
        else if (mod == 5)
        {
            if (A_time > 0)
            {
                A_time -= Time.deltaTime;
                transform.position -= new Vector3(0, 2 * Time.deltaTime, 0);
                G_player.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, 0);
                if (A_C_time <= 0)
                {
                    player.PlayerHP -= 2;
                    A_C_time = 1f / damage;
                }
                A_C_time -= Time.deltaTime;
            }
            else
            {
                m_SpriteRenderer.sprite = sp[0];
                transform.position = new Vector3(transform.position.x, originalPos, 0);
                mod = 4;
                A_time = 1f;
                nohit = true;
                HP_SpriteRenderer[0].color = new Vector4(0.1529412f, 0.9176471f, 0f, 0f);
                HP_SpriteRenderer[1].color = new Vector4(1f, 0f, 0.009400845f, 0f);
            }
        }

        if (Vector2.Distance(G_player.transform.position, new Vector3(transform.position.x, transform.position.y + 1f, 0)) < 1.3 && mod>=1 && mod<=3)
        {
            mod = 5;
            A_time = 0.5f;
            m_SpriteRenderer.sprite = sp[1];
            transform.position = new Vector3(transform.position.x, originalPos + 1, 0);
        }
    }
}
