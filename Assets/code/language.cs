using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class language : MonoBehaviour
{
    public Text m_text;
    public string English;
    public string Chinese;
    bool m_language;
    // Start is called before the first frame update
    void Start()
    {
        m_language = setting.language_mod;
        if (m_language)
        {
            m_text.text = Chinese;
        }
        else
        {
            m_text.text = English;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_language != setting.language_mod)
        {
            m_language = setting.language_mod;
            if (m_language)
            {
                m_text.text = Chinese;
            }
            else
            {
                m_text.text = English;
            }
        }
    }
}
