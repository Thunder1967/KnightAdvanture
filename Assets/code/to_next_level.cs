using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class to_next_level : MonoBehaviour
{
    static public int S_PlayerHP;
    static public int Level;
    static public int SavePoint;
    public bool IsSavePoint;
    public GameObject press;
    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            setting.UsedTime = 0;
            setting.DeadCount = 0;
            setting.SkipLevel = false;
        }
        if (IsSavePoint)
        {
            SavePoint = SceneManager.GetActiveScene().buildIndex;
            S_PlayerHP = 100;
        }
        Level = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        S_PlayerHP = player.PlayerHP;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 15f, 1 << 7);
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
                    SceneManager.LoadScene(2);
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
                    SceneManager.LoadScene(3);
                }
                break;
            case 4:
                if (hit.collider != null)
                {
                    S_PlayerHP = 50;
                    SceneManager.LoadScene(5);
                }
                break;
            case 61:
                break;

            case 62:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
                    SceneManager.LoadScene(63);
                }
                break;

            case 63:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
                    SceneManager.LoadScene(64);
                }
                break;

            case 64:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
                    SceneManager.LoadScene(3);
                }
                break;

            default:
                if (hit.collider != null)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                break;
        }
        if (Input.GetKeyDown(KeyCode.P) && SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 2)
        {
            Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex > 2)
        {
            Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
            if (S_PlayerHP > 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                S_PlayerHP = 100;
                SceneManager.LoadScene(SavePoint);
            }
        }


        if (Input.GetKeyDown(KeyCode.O) && SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().buildIndex < 62)
        {
            setting.SkipLevel = true;
            Instantiate(press, new Vector3(0, 0, 0), Quaternion.identity);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
