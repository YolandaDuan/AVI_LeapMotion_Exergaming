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
    private float startY;
    private float startX;
    private bool grounded;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startY = transform.position.y;
        startX = transform.position.x;
    }

    private void Update()
    {
        //Check if below bounds, aka dead
        if (transform.position.y < startY - 3.0f)
        {
            //Reset to start
            trail.enabled = false;
            transform.position = new Vector3(startX, startY, 0f);
        }
    }

    public void Move(float duration)
    {
        trail.enabled = true;
        CheckIfGrounded();
        if (grounded) {
            Debug.Log("Jumped for " + duration + " seconds of force");
            m_Rigidbody2D.AddForce(new Vector2(duration * m_HorizontalForce, m_JumpForce));
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
}


