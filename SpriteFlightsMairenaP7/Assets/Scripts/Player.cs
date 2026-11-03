using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Score variables
    private float elapsedTime = 0f;

    private float score = 0f;

    public float scoreMultiplier = 10f;

    private Label scoreText;

    //Speed variables
    public float maxSpeed = 5f;

    public float thrustForce = 1f;

    //Restart variables
    public GameObject explosionEffect;

    private Button restartButton;

    //Randpm Variables
    Rigidbody2D rb;

    public UIDocument uiDocument;

    //STUPID BOOSTER FLAME
    public GameObject boosterFlame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");

        //Restart Button
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = "Score: " + score;

        if (Mouse.current.leftButton.isPressed)
        {
            //Mouse direction calculation
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePos - transform.position).normalized;

            //Move player in mouse direction
            transform.up = direction;
            rb.AddForce(direction * thrustForce);


            //Max velocity
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }

            //Booster flame
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                boosterFlame.SetActive(true);
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                boosterFlame.SetActive(false);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
    }

    void ReloadScene()
        {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
}
