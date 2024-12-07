using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseBehaviourFSM : MonoBehaviour
{
    public enum MonsterState
    {
        Patrolling,
        Chasing,
        Idle
    }

    public Rigidbody2D rb;
    public float moveSpeed = 2f;

    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    public bool isFlying = false;
    public Transform player;
    public float detectionRadius = 5f;

    public float flyGravityScale = 0f;
    public float walkGravityScale = 1f;

    public Transform targetChase;

    public MonsterState currentState = MonsterState.Idle;

    // Start is called before the first frame update
    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (patrolPoints.Length > 0)
        {
            currentState = MonsterState.Patrolling;
            targetChase = patrolPoints[0];
        }
    }

    // Update is called once per frame
    private void Update()
    {
        switch (currentState)
        {
            case MonsterState.Patrolling:
                Patrol();
                if (IsPlayerDetected())
                {
                    TransitionToState(MonsterState.Chasing);
                }
                break;

            case MonsterState.Chasing:
                ChasePlayer();
                if (!IsPlayerDetected())
                {
                    TransitionToState(MonsterState.Patrolling);
                }
                break;

            case MonsterState.Idle:
                // Idle logic, if needed
                rb.velocity = Vector2.zero;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (currentState == MonsterState.Chasing)
        {
            rb.gravityScale = isFlying ? flyGravityScale : walkGravityScale;
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        targetChase = targetPoint;

        Vector2 direction = ((Vector2)targetPoint.position - rb.position).normalized;
        rb.velocity = direction * moveSpeed;

        if (Vector2.Distance(rb.position, targetPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    private void ChasePlayer()
    {
        if (player == null) return;

        targetChase = player;
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        if (isFlying)
        {
            rb.gravityScale = flyGravityScale;
        }
        else
        {
            direction.y = 0; // Walk mode only horizontal
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        }
    }

    private bool IsPlayerDetected()
    {
        if (player == null) return false;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRadius;
    }

    private void TransitionToState(MonsterState newState)
    {
        currentState = newState;

        // Reset velocities on state transitions
        rb.velocity = Vector2.zero;

        switch (newState)
        {
            case MonsterState.Patrolling:
                currentPatrolIndex = 0; // Optional reset
                break;

            case MonsterState.Chasing:
                targetChase = player;
                break;

            case MonsterState.Idle:
                targetChase = null;
                break;
        }
    }

    #region Gizmo

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    #endregion
}
