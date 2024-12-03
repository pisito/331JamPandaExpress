using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 *  a class MonsterChaseBehaviour that walk by using rigidbody2d with stats of movespeed. 
 *  Monster without seeing player will patrol with in the set of transform list register to them. 
 *  We design a bool to trigger using walk or fly mode. 
 *  Fly mode monster simply move toward the player regardless of the physics
 */
public class MonsterChaseBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 2f;

    public Transform[] patrolPoints; // Array of waypoints for patrolling
    private int currentPatrolIndex = 0;

    public bool isFlying = false; // Bool to determine if the monster is in fly mode
    public Transform player; // Reference to the player's transform
    public float detectionRadius = 5f; // Radius for detecting the player

    public float flyGravityScale = 0f;
    public float walkGravityScale = 1f;

    private bool isChasing = false;

    public bool useTagPlayer = true;
    public string playerTag = "Player";

    // Start is called before the first frame update
    private void Start()
    {
        

        if (useTagPlayer)
        {
            GameObject PlayerObject = GameObject.FindGameObjectWithTag(playerTag);
            if (PlayerObject == null)
            {
                Debug.LogWarning("Player object with tag '" + playerTag + "' not found!");
                return;
            }
            else
            {
                player = PlayerObject.transform;
            }
            
        }

        if(rb == null)
        {
           rb = GetComponent<Rigidbody2D>();
        }

        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (player != null)
        {
            DetectPlayer();
        }

        
    }

    private void FixedUpdate()
    {
        if (isChasing)
        {
            // reset to remove residue velocity
            rb.velocity = Vector2.zero;

            rb.gravityScale = isFlying ? flyGravityScale : walkGravityScale;

            if (isFlying)
            {
                FlyTowardPlayer();
            }
            else
            {
                WalkTowardPlayer();
            }
            
        }
        else
        {
            Patrol();
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isChasing = distanceToPlayer <= detectionRadius;
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector2 direction = ((Vector2)targetPoint.position - rb.position).normalized;
        rb.velocity = Vector2.Lerp(rb.velocity, direction * moveSpeed, Time.deltaTime * 5f);

        if (Vector2.Distance(rb.position, targetPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void WalkTowardPlayer()
    {
        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        direction.y = 0;
        float yRB = rb.velocity.y;
        direction *= moveSpeed;
        direction.y = yRB;
        rb.velocity = direction;
    }

    void FlyTowardPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualization for detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
