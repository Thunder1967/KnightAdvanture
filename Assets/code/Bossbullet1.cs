using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossbullet1 : MonoBehaviour
{
    Rigidbody2D ri;
    GameObject m_player;
    float Dx_player;
    float Dy_player;
    public SpriteRenderer m_sr;
    float timer;
    float Rad;
    public bool face;
    public GameObject sound;
    // Start is called before the first frame update
    void Start()
    {
        ri = GetComponent<Rigidbody2D>();
        m_player = GameObject.Find("player");
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0)
        {
            return;
        }


        timer += Time.deltaTime;
        Dx_player = m_player.transform.position.x - transform.position.x;
        Dy_player = m_player.transform.position.y - transform.position.y;
        Rad = Mathf.Atan2(Dy_player, Dx_player);
        Rad = (face ? (Rad < -80 * Mathf.Deg2Rad ? -80f * Mathf.Deg2Rad : (Rad > 90 * Mathf.Deg2Rad ? 90f * Mathf.Deg2Rad : Rad)) : (Rad > -100 * Mathf.Deg2Rad ? -100f * Mathf.Deg2Rad : (Rad < -270 * Mathf.Deg2Rad ? -270f * Mathf.Deg2Rad : Rad)));
        transform.eulerAngles = new Vector3(0f, 0f, (Rad / Mathf.Deg2Rad) + 90);
        if (timer <= 1)
        {
            m_sr.color = new Vector4(1f, 1f, 1f, timer);
        }
        else if (timer > 1.5f)
        {
            Instantiate(sound, new Vector3(0, 0, 0), Quaternion.identity);
            ri.velocity = new Vector2(Mathf.Cos(Rad) * 60, Mathf.Sin(Rad) * 60);
            timer = -1f;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 6 && other.tag != "Monster")
        {
            Destroy(this.gameObject);
        }
        else if (other.name == "player" && timer < 0)
        {
            player.PlayerHP -= 50;
            Destroy(this.gameObject);
        }
    }
}
