using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float minSpeed = 50f;
    public float maxSpeed = 300f;

    Rigidbody2D rb;
  
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();

        Vector2 randomDirection = Random.insideUnitCircle;

        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        rb.AddForce(randomDirection * randomSpeed);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
