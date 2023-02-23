using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public int fps = 60;

    private void OnValidate()
    {
        Application.targetFrameRate= fps;
    }
}
