using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill_monster : MonoBehaviour
{
    public bool BossLevel;
    GameObject Cam;
    public GameObject End;
    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Monster") == null)
        {
            if (!BossLevel)
            {
                Destroy(this.gameObject, 0.5f);
            }
            else if(BossLevel && player.PlayerHP > 0)
            {
                Instantiate(End, Cam.transform);
                Destroy(this);
            }
        }
    }
}
