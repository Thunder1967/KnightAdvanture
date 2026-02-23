using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demo : MonoBehaviour
{
    public SpriteRenderer Key_A;
    public SpriteRenderer Key_S;
    public SpriteRenderer Key_D;
    public SpriteRenderer Key_Space;
    public SpriteRenderer Key_mouse0;
    public SpriteRenderer Key_mouse1;
    float value;
    // Start is called before the first frame update
    void Start()
    {
        value = 200f / 255f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Key_A.color = new Vector4(value, value, value, 1f);
        }
        else
        {
            Key_A.color = new Vector4(1f, 1f, 1f, 1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Key_S.color = new Vector4(value, value, value, 1f);
        }
        else
        {
            Key_S.color = new Vector4(1f, 1f, 1f, 1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Key_D.color = new Vector4(value, value, value, 1f);
        }
        else
        {
            Key_D.color = new Vector4(1f, 1f, 1f, 1f);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Key_Space.color = new Vector4(value, value, value, 1f);
        }
        else
        {
            Key_Space.color = new Vector4(1f, 1f, 1f, 1f);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Key_mouse0.color = new Vector4(value, value, value, 1f);
        }
        else
        {
            Key_mouse0.color = new Vector4(1f, 1f, 1f, 1f);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Key_mouse1.color = new Vector4(value, value, value, 1f);
        }
        else
        {
            Key_mouse1.color = new Vector4(1f, 1f, 1f, 1f);
        }
    }
}
