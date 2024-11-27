using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is the Script that will Allow one Game Object to Chase another Game Object, that is, to make Game Object A follow Game Object B.
 * 
 * That is, this is the script that will make the Enemy to follow or chase the Player.
 * 
 * This script lets the enemy follow the player horizontally, emulating the behavior of a Goomba-like enemy from the Super Mario Bros games. 
 * The enemy will not jump or respond to vertical positioning.
 * 
 *   ### How to Use:
 *
 *   1. **Attach Script**: Add this script to your enemy GameObject (Game Object A).
 *   2. **Assign Player**: Drag the player GameObject into the `Player` field in the Inspector.
 *   3. **Adjust Speed**: Modify the `Speed` variable in the Inspector to set how fast the enemy moves.
 *   4. **Scene Setup**: Ensure the player and enemy GameObjects are at the correct height so the enemy moves only horizontally, ignoring 
 *     vertical positioning.
 *
 *   ### Key Features:
 *   - **Horizontal Movement**: The script ensures the enemy only chases the player along the X-axis.
 *   - **Customizable Speed**: Easily tweakable for fine-tuning enemy behavior.
 *   - **Lightweight**: Optimized for simple 2D AI in platformer games.
 *
 * You can adjust the enemy's speed manually in the Unity Inspector, as if it were a Serialized Field.
 *
 * I also added a Gizmo so that, if you click on the enemy Game Object (Game Object A) while playing the game, and if you select the 
 * Scene View, you will see a red line between the enemy and the player, which indicates that the enemy is chasing the player.
 */

public class ChaseObject : MonoBehaviour
{

    public Transform player;  // Reference to the player GameObject
    public float speed = 2f;  // Movement speed of the enemy

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is assigned
        if (player != null)
        {
            // Calculate the direction towards the player on the X-axis
            Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized;

            // Move the enemy horizontally towards the player
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    // Optional: Draw a debug line in the Scene view to visualize chasing
    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, new Vector2(player.position.x, transform.position.y));
        }
    }
}
