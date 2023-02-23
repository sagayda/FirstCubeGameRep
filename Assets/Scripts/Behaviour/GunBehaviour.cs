using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    public Transform point;
    public GameObject bullet;
    public AudioSource shotSound;
    public AudioClip shotClip;

    private int fireCooldown;
    private void Awake()
    {

    }

    private void FixedUpdate()
    {
        if (fireCooldown > 0)
        {
            fireCooldown--;
        }
    }

    private void Update()
    {
        transform.position = new Vector2(playerTransform.position.x, playerTransform.position.y + 0.15f);

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float rotateY = 0;

        if (rotateZ > 90)
        {
            rotateY = 180;
            rotateZ = 180 - rotateZ;
        }
        else if (rotateZ < -90)
        {
            rotateY = 180;
            rotateZ = -180 - rotateZ;
        }

        rotateY = Mathf.MoveTowards(transform.eulerAngles.y, rotateY, 800f * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, rotateY, rotateZ);

        if (Input.GetMouseButton(0))
        {
            if (fireCooldown <= 0)
            {
                Quaternion rotat = new Quaternion(0f, 0f, transform.rotation.z, transform.rotation.w);
                rotat = Quaternion.Euler(0f, 0f, rotat.eulerAngles.z - 90);
                Instantiate(bullet, point.position, rotat);
                fireCooldown = 4;
                shotSound.PlayOneShot(shotClip);
            }
        }

    }
}
