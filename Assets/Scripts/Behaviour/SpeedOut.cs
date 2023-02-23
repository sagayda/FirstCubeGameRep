using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedOut : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public TextMeshProUGUI text;
    int num = 1;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            num = 1;
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            num = 2;
        }

        switch (num)
        {
            case 1: 
                text.text = MathF.Round(rigidbody2d.angularVelocity, 1).ToString();
                break;
            case 2:
                text.text = MathF.Round(rigidbody2d.velocity.x, 1).ToString();
                break;
            default:
                break;
        }
    }

}
