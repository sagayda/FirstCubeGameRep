using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    public float waterForce;
    private float divePersent;
    private float waterTop;

    private void Awake()
    {
        waterTop = GetComponent<Transform>().position.y + GetComponent<Transform>().localScale.y / 2f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        divePersent = waterTop - collision.transform.position.y;
        divePersent = Mathf.Clamp(divePersent, 0f, 1f);
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, waterForce * divePersent));

        collision.attachedRigidbody.AddForce(new Vector2(collision.attachedRigidbody.angularVelocity / -600 * collision.attachedRigidbody.mass, Mathf.Abs(collision.attachedRigidbody.angularVelocity / -600 * collision.attachedRigidbody.mass)));

        collision.attachedRigidbody.AddTorque(-1f * Mathf.Clamp(collision.attachedRigidbody.angularVelocity, -0.2f, 0.2f));
        collision.attachedRigidbody.AddForce(new Vector2(-collision.attachedRigidbody.velocity.x / 2, -collision.attachedRigidbody.velocity.y / 2));
    }
}

