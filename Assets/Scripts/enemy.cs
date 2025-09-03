using UnityEditor;
using UnityEngine;

public class enemy : MonoBehaviour
{   public int maxHealth = 5; // Enemy's health
    public bool facingLeft = true; // Track the facing direction of the enemy
    public float moveSpeed = 2f; // Speed of the enemy movement
    public Transform groundCheck; 
    public float groundCheckDistance = 1f; 
    public LayerMask groundLayer;

    public bool inrage = false; 
    public Transform player;
    public float attack_range =8f;
    public float chaseSpeed = 5f; 

    public float rageDistance = 2f; 
    public Animator animator; 
    public Transform attackPoint; 
    public float attackRadious= 2f;
    public LayerMask attacjLayer; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   if(FindObjectOfType<GameManager>().isGameActive == false) return; // Check if the game is active, if not, skip the update logic

        if (maxHealth <= 0)
        {
            die();
        }
        if (Vector2.Distance(transform.position, player.position) < attack_range && !inrage) // Check if the player is within rage distance
        {
            inrage = true; // Set the enemy to rage mode
        }
        else
        {
            inrage = false; // Reset the rage mode if the player is not within range
        }
        if (inrage)
        {   
            if(player.position.x > transform.position.x && facingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0); // Flip the enemy to face the player
                facingLeft = false; // Update the facing direction
            }
            else if(player.position.x < transform.position.x && facingLeft == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0); // Flip the enemy to face the player
                facingLeft = true; // Update the facing direction

            }
            if(Vector2.Distance(transform.position, player.position) > rageDistance) // Check if the player is within rage distance
            {
                animator.SetBool("Atk1", false);
                transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime); // Move towards the player
            }
            else
            {
                animator.SetBool("Atk1", true); // Set the attacking animation
            }
        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer); // Cast a ray downwards 
            if (hit == false && facingLeft) // If the ray does not hit anything
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facingLeft = false;// Flip the enemy direction
            }
            else if (hit == false && facingLeft == false) // If the ray hits something
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingLeft = true; // Flip the enemy direction back
            }
        }
      
    }
    public void onAttack()
    {
        Collider2D callinfo= Physics2D.OverlapCircle(attackPoint.position, attackRadious, attacjLayer);
        if(callinfo == true)
        {
            if(callinfo.gameObject.GetComponent<Player>() != null)
            {
                callinfo.gameObject.GetComponent<Player>().TakeDamage(1); // Call the TakeDamage method on the player
            }
            
        }
    }

    public void Take_damage(int damage)
    {
        if (maxHealth <= 0) return;
        maxHealth-= damage;
    }

    


    private void OnDrawGizmosSelected()
    {   if (groundCheck == null) { return; }
        Gizmos.color = Color.yellow;// Check if groundCheck is assigned
        Gizmos.DrawRay(groundCheck.position, Vector2.down * groundCheckDistance); // Draw a ray in the editor to visualize the ground check
        Gizmos.color = Color.red; // Change the color to red for the raycast hit
        Gizmos.DrawWireSphere(transform.position, attack_range); // Draw a wire sphere around the ground check position
        if(attackPoint == null)  return;  // Check if attackPoint is assigned
        Gizmos.color = Color.red;  // Change the color to blue for the attack range
        Gizmos.DrawWireSphere(attackPoint.position, attackRadious);
    }
    void die()
    {
        Debug.Log(this.transform.name + "Died");
        Destroy(this.gameObject);
    }
}
