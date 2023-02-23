using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunActivation : MonoBehaviour
{
    public GameObject gun;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.SetActive(!gun.activeSelf);
        }
    }
}
