using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    public Rigidbody2D[] change;
    public Sprite[] display;
    SpriteRenderer m_S;
    float timer;
    int timer_compare;
    public int T;
    public int initialTime;
    public bool BossLevel;
    // Start is called before the first frame update
    void Start()
    {
        m_S = GetComponent<SpriteRenderer>();
        timer = initialTime;
        timer_compare = initialTime / T * T;
    }

    // Update is called once per frame
    void Update()
    {
        if (BossLevel)
        {
            if (timer < 0)
            {
                ForceChange(Mathf.CeilToInt(Random.Range(0f, 3f)), Random.Range(6f, 15f));
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            if (timer >= timer_compare)
            {
                foreach (Rigidbody2D rid in change)
                {
                    if (rid != null)
                    {
                        rid.gravityScale = Mathf.Pow(2, (timer_compare / T % 3) + 1);
                    }
                }
                m_S.sprite = display[timer_compare / T % 3];
                timer_compare += T;
            }
            timer += Time.deltaTime;
        }
    }
    public void ForceChange(int mod, float Timer)
    {
        change[0].gravityScale = Mathf.Pow(2, mod);
        m_S.sprite = display[mod - 1];
        timer = Timer;
    }
}
