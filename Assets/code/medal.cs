using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class medal : MonoBehaviour
{
    public int mod;
    public Sprite[] sp;
    public SpriteRenderer m_sr;
    public Text m_text1;
    public Text m_text2;
    bool m_language;
    // Start is called before the first frame update
    void Start()
    {
        m_language = setting.language_mod;
        if (mod == 1)
        {
            switch (setting.DeadCount)
            {
                case 0:
                    m_sr.sprite = sp[4];
                    m_text2.text = "Rank S";
                    m_text2.color = new Vector4(1f, 0.6129987f, 0, 1f);
                    break;
                case <= 5:
                    m_sr.sprite = sp[3];
                    m_text2.text = "Rank A";
                    m_text2.color = new Vector4(1f, 0, 0.6658416f, 1f);
                    break;
                case <= 15:
                    m_sr.sprite = sp[2];
                    m_text2.text = "Rank B";
                    m_text2.color = new Vector4(0.009664536f, 0, 1f, 1f);
                    break;
                case <= 30:
                    m_sr.sprite = sp[1];
                    m_text2.text = "Rank C";
                    m_text2.color = new Vector4(0, 1f, 0.8969061f, 1f);
                    break;
                default:
                    m_sr.sprite = sp[0];
                    m_text2.text = "Rank D";
                    m_text2.color = new Vector4(0.1286893f, 0.8490566f, 0, 1f);
                    break;
            }

            if (m_language)
            {
                m_text1.text = "死亡次數\n" + setting.DeadCount.ToString();
            }
            else
            {
                m_text1.text = "Number Of Deaths:\n" + setting.DeadCount.ToString();
            }
        }

        else if(mod == 2)
        {
            if (setting.SkipLevel)
            {
                m_sr.sprite = sp[0];
                if (m_language)
                {
                    m_text1.text = "通關遊戲但有跳關";
                }
                else
                {
                    m_text1.text = "Pass the game with skipping level";
                }
                m_text2.text = "Rank D";
                m_text2.color = new Vector4(0.1286893f, 0.8490566f, 0, 1f);
            }
            else
            {
                m_sr.sprite = sp[4];
                if (m_language)
                {
                    m_text1.text = "沒有使用跳關且通關遊戲";
                }
                else
                {
                    m_text1.text = "Pass the game without skipping level";
                }
                m_text2.text = "Rank S";
                m_text2.color = new Vector4(1f, 0.6129987f, 0, 1f);
            }
        }

        else if (mod == 3)
        {
            switch (setting.UsedTime)
            {
                case < 2400:
                    m_sr.sprite = sp[4];
                    m_text2.text = "Rank S";
                    m_text2.color = new Vector4(1f, 0.6129987f, 0, 1f);
                    break;
                case < 5400:
                    m_sr.sprite = sp[3];
                    m_text2.text = "Rank A";
                    m_text2.color = new Vector4(1f, 0, 0.6658416f, 1f);
                    break;
                case < 7200:
                    m_sr.sprite = sp[2];
                    m_text2.text = "Rank B";
                    m_text2.color = new Vector4(0.009664536f, 0, 1f, 1f);
                    break;
                case < 10800:
                    m_sr.sprite = sp[1];
                    m_text2.text = "Rank C";
                    m_text2.color = new Vector4(0, 1f, 0.8969061f, 1f);
                    break;
                default:
                    m_sr.sprite = sp[0];
                    m_text2.text = "Rank D";
                    m_text2.color = new Vector4(0.1286893f, 0.8490566f, 0, 1f);
                    break;
            }

            int UT = Mathf.CeilToInt(setting.UsedTime);
            if (m_language)
            {
                m_text1.text = "過關時間\n" + (UT / 3600).ToString() + " h " + (UT / 60 % 60).ToString() + " m " + (UT % 60).ToString() + " s";
            }
            else
            {
                m_text1.text = "Pass Time:\n" + (UT / 3600).ToString() + " h " + (UT / 60 % 60).ToString() + " m " + (UT % 60).ToString() + " s";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    } 
}
