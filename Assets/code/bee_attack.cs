using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bee_attack : MonoBehaviour
{
    Rigidbody2D ri;
    float Dx_player;
    float Dy_player;
    public float damage = 0;
    public float attackspeed = 0;
    int aimlayer;
    // Start is called before the first frame update
    void Start()
    {
        aimlayer = LayerMask.NameToLayer("Ground");
        ri = GetComponent<Rigidbody2D>();
        Dx_player = GameObject.Find("player").transform.position.x - transform.position.x;
        Dy_player = GameObject.Find("player").transform.position.y - transform.position.y;
        transform.Rotate(0f, 0f, Mathf.Atan2(Dy_player, Dx_player) / Mathf.Deg2Rad);
        ri.velocity = new Vector2(Dx_player * Mathf.Sqrt(attackspeed / (Dx_player * Dx_player + Dy_player * Dy_player)), Dy_player * Mathf.Sqrt(attackspeed / (Dx_player * Dx_player + Dy_player * Dy_player)));
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == aimlayer)
        {
            Destroy(this.gameObject);
        }
        else if(other.name == "player")
        {
            player.PlayerHP -= (int)damage;
            Destroy(this.gameObject);
        }
    }

}
