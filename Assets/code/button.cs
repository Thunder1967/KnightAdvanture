using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    public Text m_text;
    public GameObject press;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void changeLanguage()
    {
        setting.language_mod = !setting.language_mod;
        Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void riseFPS()
    {
        switch (setting.FPS)
        {
            case 10:
                setting.FPS = 30;
                break;
            case 30:
                setting.FPS = 60;
                break;
            case 60:
                setting.FPS = 120;
                break;
            case 120:
                setting.FPS = 240;
                break;
            default:
                setting.FPS = 30;
                break;
        }
    }

    public void dropFPS()
    {
        switch (setting.FPS)
        {
            case 10:
                setting.FPS = 240;
                break;
            case 30:
                setting.FPS = 10;
                break;
            case 60:
                setting.FPS = 30;
                break;
            case 120:
                setting.FPS = 60;
                break;
            default:
                setting.FPS = 120;
                break;
        }
    }
    public void back()
    {
        Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
        Application.targetFrameRate = setting.FPS;
        SceneManager.LoadScene(to_next_level.Level);
    }
}
