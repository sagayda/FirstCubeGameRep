using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCurve : MonoBehaviour
{
    public AnimationCurve curve;
    public Rigidbody2D rb;

    private void Update()
    {
        Keyframe key = new Keyframe(Time.time, rb.velocity.x , 0, 0, 0, 0);
        curve.AddKey(key);
    }
}
