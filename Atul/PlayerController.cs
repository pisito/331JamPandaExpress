using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script will handle everything about the player, including allowing me to move the player using the WASD keys. 
 * 
 * Source of some part of this script: Alex Dev from Udemy: 
 * https://www.udemy.com/course/free-part1-alexdev/learn/lecture/37545226#overview .
 * 
 * I also took code from this snippet from Alex Dev:
 * https://www.udemy.com/course/free-part1-alexdev/learn/lecture/37545214#overview
 * 
 * I took part of the snippet that makes the player jump from Alex Dev:
 * https://www.udemy.com/course/free-part1-alexdev/learn/lecture/37545230#overview . This will make the player jump.
 * 
 * It's in the update() of this script where I need to add the snippet to make the player jump. 
 * 
 * About jumping: remember also that I want the player to jump only once. For that, I have to add a lot of code. If I don't add the extra 
 * code to make the player jump only once, the player will jump infinitely.
 * 
 * I added a functionality which lets me pick up power ups. There will be 2 power ups: one that makes the player move faster, and another 
 * that makes the player move slower (it would be better to call it something like a "power down").  I would also 
 * need to create 2 new sprites for the power ups. 
 */

public class PlayerController : MonoBehaviour
{
    // This should detect the player's Rigid Body for collision detection purposes
    private Rigidbody2D rb;

    // This lets me adjust the running speed of the player in the Unity Editor
    [SerializeField] private float moveSpeed;

    // This lets me adjust the jumping height of the player in the Unity Editor
    [SerializeField] private float jumpForce;

    public float xInput;

    // I need these ground checkig variables to make the player jump only once, and only if they are in contact with the ground.
    public float groundCheckRadius;
    public Transform groundCheck;

    public bool groundDetected;

    // This will let the player assign the layer of the ground in the Unity Editor
    public LayerMask whatIsGround;

    [SerializeField] private float powerEffectDuration = 5f; // How long the power-up or power-down lasts



    // Start is called before the first frame update.
    /* I need this to make the player jump.
    */
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame.
    /* Update() function.
     * 
     * This will make the player run and jump.
     * 
     */
    void Update()
    {
        CollisionChecks();

        // This detects if the player is pressing the left and right arrow keys.
        xInput = Input.GetAxisRaw("Horizontal");

        Movement();

        // This makes the player jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }


    /* Function that detects the collision between the Player and the Floor, which prevents the bug that made the player jump 
     * indefinitely.
     */
    private void CollisionChecks()
    {
        groundDetected = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    /* Function to make the player jump.
     
     */
    private void Jump()
    {
        if (groundDetected)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // This makes the player run and walk
    private void Movement()
    {

        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    /* I need this to make the collision detection between the player and the ground so that the player can jump only once. 
     * 
     * That is, this function will fix the bug that makes the player jump indefinitely.
     */
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    /* Function that handles the power-up and power-down functionalities.

        This modify the player’s movement speed temporarily when interacting with power-ups or power-downs.

        ### Instructions to Set Up in Unity:
        1. **Player Setup**:
           - Ensure the player GameObject has a `Rigidbody2D`, `Collider2D`, and is tagged as "Player".

        2. **Power-Up and Power-Down Prefabs**:
           - Create two new prefabs: one for the power-up to move faster and another for the power-down that makes you move slower.
           - Add the appropriate sprite for each prefab.
           - Assign a `Collider2D` (e.g., `CircleCollider2D`) to the prefabs, set it as a trigger.

        3. **Tags**:
           - Create tags `PowerUpMoveFaster` and `PowerDownMoveSlower` in the Unity Editor, and assign them to their respective prefabs.

        4. **Scene Placement**:
           - Place the prefabs in the scene, or spawn them using your existing spawner system.

        5. **Testing**:
           - Ensure the player can collect the power-up or power-down and that the speed changes accordingly for the set duration.

        This setup allows the player to interact with the power-upsthat make them move faster or slower seamlessly.
     * 
     */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUpMoveFaster"))
        {
            Destroy(other.gameObject);  // Make the power-up disappear
            StartCoroutine(AdjustSpeed(3f)); // Increase speed
        }
        else if (other.CompareTag("PowerDownMoveSlower"))
        {
            Destroy(other.gameObject);  // Make the power-up disappear
            StartCoroutine(AdjustSpeed(0.2f)); // Decrease speed
        }
    }

    /* Coroutine which modifies the player's speed temporarily when the player picks up the power-ups.
     * 
     */
    private IEnumerator AdjustSpeed(float speedMultiplier)
    {
        moveSpeed *= speedMultiplier;
        yield return new WaitForSeconds(powerEffectDuration);
        moveSpeed /= speedMultiplier;
    }
}
