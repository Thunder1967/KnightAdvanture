using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class setting : MonoBehaviour
{
    public Text S_FPS;
    public Text Level;
    static public bool language_mod;
    static public int FPS;
    static public float Music;
    static public float Sound;
    public Slider Music_slider;
    public Slider Sound_slider;
    public GameObject press;
    public static float UsedTime;
    public static int DeadCount;
    public static bool SkipLevel;

    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            language_mod = true;
            FPS = 120;
            Music = 0.5f;
            Sound = 1f;
            Application.targetFrameRate = FPS;
            Destroy(this);
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Music_slider.value = Music;
            Sound_slider.value = Sound;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        S_FPS.text = FPS.ToString();
        Level.text = "LEVEL " + (to_next_level.Level - 2).ToString();

        if (Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
            Application.targetFrameRate = FPS;
            SceneManager.LoadScene(to_next_level.Level);
        }
        Music = Music_slider.value;
        Sound = Sound_slider.value;
    }
}
