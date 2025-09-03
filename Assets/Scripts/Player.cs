using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
public class Player : MonoBehaviour
{
    public Text coinText; 
    public static int currentCoin = 0;
    public int maxHealth = 10; // Player's health
    public Text healthText; // Reference to the UI Text component to display health
    public Animator animator; // Reference to the Animator component for animations
    public float jumpForce = 6f; // Force applied when jumping
    public Rigidbody2D rb; // Reference to the Rigidbody2D component
    public float speed = 5f; // Speed of the player movement
    public bool isGrounded = true; // Track if the player is on the ground
    private float movemnet;
    private bool facingRight = true; // Track the facing direction of the player

    public Transform attack_point;
    public float attack_radious = 1f; // Range of the attack
    public LayerMask attack_layer; // Layer mask to specify which layers are considered enemies
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (maxHealth <= 0)
        {
            Die(); 

        }
        coinText.text = currentCoin.ToString(); 
        healthText.text = maxHealth.ToString();

        movemnet = Input.GetAxis("Horizontal"); 

        if (movemnet < 0 && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f); // Face right
            facingRight = false; // Update the facing direction
        }
        else if (movemnet > 0 && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f); // Face left
            facingRight = true; // Update the facing direction

        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            jump();
            isGrounded = false;
            animator.SetBool("Jump", true);// Call the jump method when the space key is pressed
        }


        if (Mathf.Abs(movemnet) != 0f)
        {
            animator.SetFloat("Run", 1f); 
        }
        else if (Mathf.Abs(movemnet) < 0.1f)
        {
            animator.SetFloat("Run", 0f); 
        }
        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is pressed
        {
            animator.SetTrigger("Attack"); // Trigger the attack animation
        }

    }
    private void FixedUpdate()
    {
        // Move the player based on the input
        transform.position += new Vector3(movemnet, 0f, 0f) * Time.fixedDeltaTime * speed; // Move at a speed of 5 units per second
    }

    void jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true; // Set isGrounded to true when colliding with the ground
            animator.SetBool("Jump", false); // Reset the jump animation when on the ground
        }
    }

    public void Attack()
    {
        Collider2D callInfo = Physics2D.OverlapCircle(attack_point.position, attack_radious, attack_layer);
        if (callInfo)
        {
            if (callInfo.gameObject.GetComponent<enemy>() != null)
            {
                callInfo.gameObject.GetComponent<enemy>().Take_damage(1);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (attack_point == null)
        {
            return; // Check if the attack point is assigned
        }
        Gizmos.color = Color.red; // Set the color of the gizmo to red
        Gizmos.DrawWireSphere(attack_point.position, attack_radious);
    }
    public void TakeDamage(int damage)
    {

        if (maxHealth <= 0)
        {
            return; // Call the Die method if health is zero or below
        }
        maxHealth -= damage; // Reduce the player's health by the damage amount
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "coin")
        {
            currentCoin++; // Increment the coin count when the player collects a coin
            other.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("collected");
            Destroy(other.gameObject,1f); 
        }
        if(other.gameObject.tag == "VP")
        {
         FindObjectOfType<SceneManagement>().LoadLevel(); 
            
        }
    }

        void Die()
        {
            Debug.Log("Player has died"); // Log a message when the player dies
            FindObjectOfType<GameManager>().isGameActive = false; // Call the EndGame method in the GameManager
            Destroy(this.gameObject); // Destroy the player game object 
            SceneManager.LoadScene("deathPanel");
    }

    
}
