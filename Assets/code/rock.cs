using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    float aimposX;
    bool attack;
    float timer;
    public Animator m_ani;
    bool boom;
    public GameObject sound;
    // Start is called before the first frame update
    void Start()
    {
        attack = false;
        boom = false;
        transform.position = new Vector3(Random.Range(-8f, 27f), 10f, 0);
        aimposX = Random.Range(-10f, 30f) - transform.position.x;
        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-13f, aimposX) / Mathf.Deg2Rad);
        timer = Random.Range(2.5f, 11f);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if(timer < -90)
            {
                return;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(Mathf.Atan2(-13f, aimposX)) * 40, Mathf.Sin(Mathf.Atan2(-13f, aimposX)) * 40);
            timer = -100f;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if((other.gameObject.layer == 6 || other.gameObject.layer == 7) && other.tag != "Monster" && !boom)
        {
            m_ani.SetTrigger("Boom");
            Instantiate(sound, new Vector3(0, 0, 0), Quaternion.identity);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            transform.position += new Vector3(0, -1f, 0);
            boom = true;
            Destroy(this.gameObject, 0.4f);
        }
        if(other.name == "player" && !attack)
        {
            player.PlayerHP -= 20;
            attack = true;
        }
    }
}
