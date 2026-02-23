using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_dead : MonoBehaviour
{
    GameObject G_player;
    SpriteRenderer m_SpriteRenderer;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        G_player = GameObject.Find("player");
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        if(G_player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
        }
        timer = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            m_SpriteRenderer.color = new Vector4(1f, 1f, 1f, timer / 1.2f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
