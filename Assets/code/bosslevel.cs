using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosslevel : MonoBehaviour
{
    public GameObject m_player;
    float player_X;
    public float speed;
    public float startpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player_X = m_player.transform.position.x;
        if (player_X > transform.position.x + startpoint && transform.position.x < 20f)
        {
            transform.position = new Vector3(player_X - startpoint, 0, -10);
            if (transform.position.x > 20f)
            {
                transform.position = new Vector3(20f, 0, -10);
            }
        }
        else if (player_X < transform.position.x - startpoint && transform.position.x > 0)
        {
            transform.position = new Vector3(player_X + startpoint, 0, -10);
            if (transform.position.x < 0)
            {
                transform.position = new Vector3(0, 0, -10);
            }
        }
    }
}
