using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saying : MonoBehaviour
{
    public Text m_text;
    public GameObject Skip;
    public GameObject role1;
    public GameObject role2;
    public hugeslime_initial boss;

    public GameObject[] Des;
    public string[] English;
    public string[] Chinese;
    public bool[] Who;
    public float StartTime;
    public float SayingSpeed;
    string will_Say;
    string say;
    float Timer;
    float Timer2;
    int Sort;
    int Run;
    bool waiting;
    public GameObject press;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        Timer2 = StartTime;
        Sort = -1;
        say = "";
        will_Say = setting.language_mod ? Chinese[0] : English[0];
        role1.SetActive(Who[0]);
        role2.SetActive(!Who[0]);
        Skip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= Timer2 && Sort == -1)
        {
            Sort++;
            Run = 0;
        }
        else if (Timer >= Timer2 && Run != will_Say.Length)
        {
            say += will_Say[Run];
            Timer2 += SayingSpeed;
            Run++;
            if(Run == will_Say.Length)
            {
                Skip.SetActive(true);
            }
        }
        else if (!waiting)
        {
            Sort++;
            if(Sort == (setting.language_mod ? Chinese : English).Length)
            {
                foreach(GameObject D in Des)
                {
                    Destroy(D);
                }
                return;
            }
            say = "";
            will_Say = setting.language_mod ? Chinese[Sort] : English[Sort];
            Run = 0;
            waiting = true;
            Timer2 = Timer;
            role1.SetActive(Who[Sort]);
            role2.SetActive(!Who[Sort]);
            if(Sort + 1 == 8)
            {
                boss.enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.X) && waiting)
        {
            Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
            waiting = false;
            Skip.SetActive(false);
        }
        m_text.text = say;
    }
}
