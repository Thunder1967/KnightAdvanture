using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_enable : MonoBehaviour
{
    public GameObject m_slime;
    // Start is called before the first frame update
    void Start()
    {
        m_slime.GetComponent<slime>().enabled = false;
        m_slime.GetComponent<slime>().speed = Random.Range(1f, 1.5f);
        m_slime.GetComponent<slime>().damage = 30;
        m_slime.GetComponent<slime>().MaxHP = 40f;
        m_slime.GetComponent<slime>().HP = 40f;
        m_slime.GetComponent<Collider2D>().isTrigger = true;
        m_slime.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(2f, 6f) * (Random.Range(0f, 1f) > 0.5f ? 1 : -1), Random.Range(6f, 12f));
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 6 && other.tag != "Monster")
        {
            m_slime.GetComponent<slime>().enabled = true;
            m_slime.GetComponent<Collider2D>().isTrigger = false;
            m_slime.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Destroy(this);
        }
    }
}
