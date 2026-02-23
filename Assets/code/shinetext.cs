using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shinetext : MonoBehaviour
{
    public float speed;
    float timer;
    Text m_Text;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<Text>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_Text.color = new Vector4(m_Text.color.r, m_Text.color.g, m_Text.color.b, 0.5f*Mathf.Cos(timer)+0.5f);
        timer += Time.deltaTime * speed;
    }
}
