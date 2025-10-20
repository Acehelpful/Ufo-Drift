using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour

{
    private float zeit = 0f;
    public float thrustforce = 1f;
    Rigidbody2D rb;
    float maxSpeed = 10f;

    private float punkte = 0f;
    public float punktemultiplizierer = 2f;

    public GameObject flamme;

    public UIDocument uiDocument;

    private Label scoreLabel;
    private Label highscorelabel;


    private Button Restart;

    public GameObject Explosion;

    private float highscore;

    //für mobile

    public InputAction moveForward;
    public InputAction lookPosition;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreLabel = uiDocument.rootVisualElement.Q<Label>("scoreLabel");
        rb = GetComponent<Rigidbody2D>();
        Restart = uiDocument.rootVisualElement.Q<Button>("Restart");
        Restart.style.display = DisplayStyle.None;
        Restart.clicked += ReloadScene;
        highscorelabel = uiDocument.rootVisualElement.Q<Label>("highscorelabel");
        highscorelabel.style.display = DisplayStyle.None;

        moveForward.Enable();
        lookPosition.Enable();

    }

    // Update is called once per frame
    void Update()
    {
        zeit += Time.deltaTime;
        punkte = Mathf.FloorToInt(zeit * punktemultiplizierer);


        scoreLabel.text = "Punkte: " + punkte;

        if (moveForward.IsPressed())
        {
            flamme.SetActive(true);
        }
        else if (moveForward.WasReleasedThisFrame())
        {
            flamme.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(Explosion, transform.position, transform.rotation);
        Restart.style.display = DisplayStyle.Flex;
        highscore = Mathf.FloorToInt(highscore); 
        if (punkte > highscore)
        {

            highscore = punkte;
            PlayerPrefs.SetFloat("Highscore", highscore);
            PlayerPrefs.Save();

        }
        highscore = PlayerPrefs.GetFloat("Highscore", 0);
        highscorelabel.text = "Maxwin: " + highscore;
        highscorelabel.style.display = DisplayStyle.Flex;

    }


    void ReloadScene() { 

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    void FixedUpdate()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (MousePos - transform.position).normalized;

            transform.up = direction;
            rb.AddForce(direction * thrustforce, ForceMode2D.Impulse);
        }

        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
    }

    }
