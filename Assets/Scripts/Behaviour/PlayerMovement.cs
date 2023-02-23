
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Transform playerTransform;
    private Collider2D playerCollider;
    public GameObject player;
    private List<Collision2D> playerCollisions = new List<Collision2D>();

    public float moveForce;
    public float jumpForce;
    public float inAirForceReduction = 1;
    public float scaleForceMultiplier = 1;
    public float playerShrinkStep = 0.25f;
    public float jumpPreparationSpeed = 1;
    public float forceOnSpeedDependencyMultp = 1;

    private float playerMass;
    private float scaleForce;
    private bool isGrounded;
    private float startPlayerScale;
    private float forceOnSpeedDependency = 1;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerCollider = GetComponent<Collider2D>();
        Physics2D.maxRotationSpeed = 15;
        startPlayerScale = playerTransform.localScale.x;
    }
    private void Update()
    {
        playerMass = playerRb.mass;
        scaleForce = Mathf.Pow(playerMass, scaleForceMultiplier);
    }

    private void FixedUpdate()
    {
        Vector2 addForce = Vector2.zero;

        forceOnSpeedDependency = Mathf.Clamp(Mathf.Pow(Mathf.Abs(playerRb.angularVelocity / 200), 1 / forceOnSpeedDependencyMultp), 1f, 2f);

        if (Input.GetKey(KeyCode.Space))
        {
            TryChangeScale(playerTransform, playerTransform.localScale.x / 50 * -jumpPreparationSpeed, startPlayerScale, startPlayerScale - playerShrinkStep);
        }
        else if (playerTransform.localScale.x < startPlayerScale)
        {
            TryChangeScale(playerTransform, playerTransform.localScale.x / 5, startPlayerScale, startPlayerScale - playerShrinkStep);
            foreach (var collision in playerCollisions)
                foreach (var contact in collision.contacts)
                    addForce += new Vector2(contact.normal.x, contact.normal.y);
            playerRb.AddForce(addForce.normalized * jumpForce);
        }


        if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                float v = -1 * Input.GetAxis("Vertical") * Input.GetAxis("Horizontal") * moveForce;
                if (!isGrounded)
                {
                    playerRb.AddForce(new Vector2(playerRb.angularVelocity / -inAirForceReduction * scaleForce / forceOnSpeedDependency, 0f));
                }
                playerRb.AddTorque(v * scaleForce / forceOnSpeedDependency);
            }
        }

        if (Input.GetKey(KeyCode.U))
        {
            playerRb.MovePosition(new Vector3(0, 5, 0));
            playerRb.SetRotation(70);
        }

        if (Input.GetKey(KeyCode.T))
        {
            playerRb.AddForce(new Vector2(0f, 10f));
            playerRb.SetRotation(70);
        }
    }

    private void TryChangeScale(Transform objectTransform, float step, float maxValue, float minValue)
    {

        if (objectTransform.localScale.x + step <= maxValue && objectTransform.localScale.x + step >= minValue)
        {
            objectTransform.localScale = new Vector2(objectTransform.localScale.x + step, objectTransform.localScale.x + step);
        }
        else
        {
            if (objectTransform.localScale.x + step > maxValue && objectTransform.localScale.x != maxValue)
                objectTransform.localScale = new Vector2(maxValue, maxValue);

            if (objectTransform.localScale.x + step < minValue && objectTransform.localScale.x != minValue)
                objectTransform.localScale = new Vector2(minValue, minValue);

        }
    }

    private bool IsCollisionLower(Collision2D collision)
    {
        foreach (var item in collision.contacts)
        {
            if (item.normal.y >= 0.5f)
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerCollisions.Add(collision);
        if (IsCollisionLower(collision))
            isGrounded = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (IsCollisionLower(collision))
            isGrounded = true;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        playerCollisions.Remove(collision);
        isGrounded = IsCollisionLower(collision);
    }

}
