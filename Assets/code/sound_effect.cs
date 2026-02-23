using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_effect : MonoBehaviour
{
    public AudioSource m_audio;
    public int mod;
    // Start is called before the first frame update
    void Start()
    {
        m_audio.volume = setting.Sound;
        if(mod == 1)
        {
            DontDestroyOnLoad(this.gameObject);
            Destroy(this.gameObject, 0.5f);
        }
        else if(mod == 2)
        {
            Destroy(this.gameObject, 0.5f);
        }
        else if(mod == 4)
        {
            Destroy(this.gameObject, 2f);
        }
        else if (mod == 5)
        {
            Destroy(this.gameObject, 3.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
