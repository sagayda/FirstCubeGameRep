using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float lerpRate = 5.0f;

    private void LateUpdate()
    {

        Vector3 pos = Vector3.Lerp(transform.position, playerTransform.position, lerpRate);
        pos.z = -10f;
        pos = Vector3.Lerp(pos, pos += new Vector3(8 * Input.GetAxisRaw("HorizontalArrow"), 4 * Input.GetAxisRaw("VerticalArrow"), 0f), lerpRate);

        transform.position = pos;
    }
}
