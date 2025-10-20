using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float minSize = 0.5f;

    public float maxSize = 2.7f;

    public float minSpeed = 100f;

    public float maxSpeed = 300f;

    public float maxSpinSpeed = 1f;

    public GameObject Bong;
   

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        float randomSize = Random.Range(minSize, maxSize);
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);


        Vector2 randomDirection = Random.insideUnitCircle;

        transform.localScale = new Vector3(randomSize, randomSize, 1);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomSpeed);
        rb.AddTorque(randomTorque);








    }

    // Update is called once per frame
    void Update()


    {
        rb.linearVelocity *= (1f + (Time.timeSinceLevelLoad / 35000));
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
        if (rb.linearVelocity.magnitude < minSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * minSpeed;
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).point;
        GameObject bounceEffect = Instantiate(Bong, contactPoint, Quaternion.identity);

        // Destroy the effect after 1 second
        Destroy(bounceEffect, 1f);
    }
    }


