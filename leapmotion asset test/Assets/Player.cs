using System;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 10f; // Amount of force added when the player jumps  
    [SerializeField] private float m_HorizontalForce = 10f;  // Amount of force added to the horizontal direction when the player jumps  
    [SerializeField] private float k_GroundedRadius = .15f; // Radius of the overlap circle to determine if grounded
    private Camera cam;
    private TrailRenderer trail;
    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private SphereCollider landCheck;
    private float startY;
    private float startX;
    private bool grounded;
    private bool resetting;
    private GameObject UI;
    public Sprite SadPlayer;
    public Sprite HappyPlayer;
    public Sprite HighScorePlayer;

    public GameObject platform;
    public GameObject landingParticle;

    private void Awake()
    {
        landCheck = GetComponent<SphereCollider>();
        UI = GameObject.FindGameObjectWithTag("UItext");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startY = transform.position.y;
        startX = transform.position.x;
    }

    private void Update()
    {
        //Check if below bounds, aka dead
        if (transform.position.y < startY - 5.0f)
        {
            //Reset to start
            fallOffScreen();
        }
        if (resetting && m_Rigidbody2D.velocity.magnitude < 0.3f)
        {
            Instantiate(platform, new Vector3(transform.position.x, transform.position.y-1f, transform.position.z), Quaternion.identity);
            resetting = false;
        }
        if (false)
        {
            Instantiate(landingParticle, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
        }
    }

    private void fallOffScreen()
    {
        //trail.enabled = false;
        m_Rigidbody2D.velocity = Vector3.up * 9;
        resetting = true;
        UI.GetComponent<Score>().resetScore();
        ChangeFace(SadPlayer);
        //transform.position = new Vector3(startX, startY, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            var score = UI.GetComponent<Score>().CurrentScore;
            var highScore = UI.GetComponent<Score>().HighScore;
            if (score > 10 && score < highScore)
            {
                ChangeFace(HappyPlayer);
            }
            if (score >= highScore)
            {
                ChangeFace(HighScorePlayer);
            }
            Instantiate(landingParticle, new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z), Quaternion.identity);
        }
    }

    public void Move(float duration)
    {
        trail.enabled = true;
        CheckIfGrounded();
        if (grounded) {
            //Debug.Log("Jumped for " + duration + " seconds of force");
            m_Rigidbody2D.AddForce(new Vector2(duration * m_HorizontalForce, m_JumpForce));
            //Instantiate(landingParticle, new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z), Quaternion.identity);
        }
    }

    public bool CheckIfGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, k_GroundedRadius);
        grounded = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
        return grounded;
    }

    public void ChangeColor(float r, float g, float b)
    {
        spriteRenderer.color = new Color(r, g, b);
    }

    public void ChangeFace(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}


