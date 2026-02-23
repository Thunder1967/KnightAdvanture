using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class music_ctrl : MonoBehaviour
{
    public AudioSource m_audio;
    public AudioClip[] BGM;
    int Done;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        m_audio.volume = setting.Music;
        Done = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            m_audio.volume = setting.Music;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 46 && Done == 0)
        {
            m_audio.clip = BGM[1];
            m_audio.Play();
            Done = 1;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 61 && Done == 1)
        {
            m_audio.clip = BGM[2];
            m_audio.Play();
            Done = 2;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 62 && Done == 2)
        {
            m_audio.clip = BGM[0];
            m_audio.Play();
            Done = 3;
        }
    }
}
