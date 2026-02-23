using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hugeslime_initial : MonoBehaviour
{
    GameObject m_player;
    public hugeslime main;
    public Rigidbody2D rigid;
    public GameObject sound;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("player");
        if(m_player.transform.position.x > 26.7f)
        {
            transform.position = new Vector3(26.7f, 100f, 0);
        }
        else if(m_player.transform.position.x < -7.75f)
        {
            transform.position = new Vector3(-7.75f, 100f, 0);
        }
        else
        {
            transform.position = new Vector3(m_player.transform.position.x, 100f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -3.9f)
        {
            Instantiate(sound, new Vector3(0, 0, 0), Quaternion.identity);
            transform.position = new Vector3(transform.position.x, -3.9f, 0);
            rigid.velocity = new Vector2(0, 0f);
            if(Mathf.Abs(m_player.transform.position.x - transform.position.x) < 2)
            {
                player.PlayerHP -= 1010;
            }
            else
            {
                main.enabled = true;
            }
            Destroy(this);
        }
        else
        {
            rigid.velocity = new Vector2(0, -100f);
        }
    }
}
