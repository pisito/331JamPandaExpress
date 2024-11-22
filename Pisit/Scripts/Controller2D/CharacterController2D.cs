using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pisit.Controllers
{
    public class CharacterController2D : MonoBehaviour
    {
        Rigidbody2D rb;

        public Vector2 moveInput { get; private set; }

        private float horizontalInput;
        private float verticalInput;
        private bool isJumpPressed;
        private bool isGrounded;

        private bool isFlipX = false;
        private Vector3 originalTransformScale;
        private Vector2 previousMoveInput;

        #region Parameters
        public float moveSpeed = 4f;
        public float sprintSpeedMod = 2.0f;
        public float moveSpeedMod = 1.0f;
        public float jumpPower = 2f;
        public float groundCheckRadius = 0.4f;
        public LayerMask groundLayerMask;
        #endregion


        // Start is called before the first frame update
        void Start()
        {
            originalTransformScale = transform.localScale;
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if(moveInput != Vector2.zero)
            {
                previousMoveInput = moveInput;
            }
            handleMoveInput();
            handleJumpInput();
            IsGroundCheck();
        }

        private void FixedUpdate()
        {
            Vector2 newVelocity = new Vector2(moveInput.x, 0f);
            newVelocity *= moveSpeed * moveSpeedMod;

            newVelocity.y = rb.velocity.y;

            if(isJumpPressed & isGrounded )
            {
                newVelocity.y = -2 * Physics2D.gravity.y * jumpPower;
                isJumpPressed = false;
                isGrounded = false;
            }

            rb.velocity = newVelocity;
            HandleFlipX();
        }

        void handleMoveInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            moveInput = new Vector2(horizontalInput, verticalInput);
        }

        void handleJumpInput()
        {
            if(Input.GetButtonDown("Jump"))
            {
                isJumpPressed = true;
            }
            
        }

        void IsGroundCheck()
        {
            isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayerMask) != null;
        }

        void HandleFlipX()
        {
            if(moveInput != Vector2.zero)
            {
                if (moveInput.x < 0f)
                {
                    transform.localScale = new Vector3(-1 * originalTransformScale.x, originalTransformScale.y, originalTransformScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(originalTransformScale.x, originalTransformScale.y, originalTransformScale.z);
                }
            }
            else
            {
                if (previousMoveInput.x < 0f)
                {
                    transform.localScale = new Vector3(-1 * originalTransformScale.x, originalTransformScale.y, originalTransformScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(originalTransformScale.x, originalTransformScale.y, originalTransformScale.z);
                }
            }
            
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
        }
    }
}
