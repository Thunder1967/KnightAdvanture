using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hugeslime_dead : MonoBehaviour
{
    public GameObject Summoned;
    float timer;
    bool isSummon;
    public GameObject sound;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        isSummon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 4.2 && !isSummon)
        {
            Instantiate(sound, new Vector3(0, 0, 0), Quaternion.identity);
            for (int i = 0; i < 10; i++)
            {
                Instantiate(Summoned, transform.position + new Vector3(0, 4f, 0), Quaternion.identity).GetComponent<slime_enable>().enabled = true;
            }
            isSummon = true;
        }
        else if(timer > 6)
        {
            Destroy(this.gameObject);
            return;
        }
        timer += Time.deltaTime;
    }
}
