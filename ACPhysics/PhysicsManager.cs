using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ACPhysics {

    public class PhysicsManager : MonoBehaviour
    {
        public Action inGroundEnter = null;

        [Header("Check ground")]
        public LayerMask groundMask;
        public bool checkGround, groundedDebugCircle;
        [Header("Check ground circle")]
        public float radius;
        public Vector3 offset;
        public Color color = Color.red;
        [Header("Jump")]
        Rigidbody2D rb;
        public bool jump;
        public float gravity, jumpInverseDamping, fallingDumpingRage, fallingDamping, jumpIntensity, fallIntensity;

        [HideInInspector]
        public bool canMove = true;
        public Action moveCallBack = null, cantMoveCallBack = null, idleCallBack;


        Move move;
        float f, t;
        [Header("Move")]
        public Vector2 moveIntensity;
        [HideInInspector]
        public Vector2 moveDirection;

        bool inGroundBuffer = false;
        bool applyFallDumping = false;
        bool resetT = false;
        private void OnDrawGizmos()
        {
            if (checkGround && groundedDebugCircle)
            {
                Gizmos.color = color;
                Gizmos.DrawWireSphere(transform.position + offset, radius);
            }
        }

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            move = new Move(rb);
            rb.gravityScale = gravity;
            inGroundBuffer = IsGrounded();
        }

        void FixedUpdate()
        {
            bool isGround = IsGrounded();
            move.canMove = canMove;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(f, 0, t * jumpInverseDamping));
            if (rb.velocity.y < 0.1f && !isGround)
            {
                if (resetT) { resetT = false; t = 0; }
                f = 0;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(0, -fallIntensity, t * fallingDamping));
            }
            t += Time.deltaTime * .5f;
            move.Move_(moveDirection, moveIntensity, moveCallBack, cantMoveCallBack, idleCallBack);
            moveDirection.y = 0;
            if (jump && isGround && checkGround && canMove)
            {
                f = jumpIntensity;
                applyFallDumping = true;
                t = 0;
                resetT = true;
            }
            if (rb.velocity.y <= 0) rb.gravityScale = gravity;
            if (isGround && !inGroundBuffer)
            {
                if (inGroundEnter != null) inGroundEnter();
                applyFallDumping = false;
            }
            inGroundBuffer = isGround;
        }

        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(transform.position + offset, radius, groundMask);
        }
    }



}
