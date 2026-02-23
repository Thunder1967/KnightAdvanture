using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion_r : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "player")
        {
            if (player.PlayerHP >= 50)
            {
                player.PlayerHP = 100;
            }
            else
            {
                player.PlayerHP += 50;
            }
            Destroy(this.gameObject);
        }
    }
}
