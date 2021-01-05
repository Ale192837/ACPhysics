using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ACPhysics
{

    public class Move
    {
        Rigidbody2D rb2d;
        Rigidbody rb;
        public bool canMove = true;
        public Move(Rigidbody2D rb_)
        {
            rb2d = rb_;
        }

        public Move(Rigidbody rb_)
        {
            rb = rb_;
        }

        public void Move_(Vector2 dir, Vector2 intensity, Action moveCallBack = null, Action cantMoveCallback = null, Action idleCallback = null)
        {
            dir = dir.normalized;
            if (canMove)
            {
                rb2d.velocity = new Vector2(dir.x * intensity.x, rb2d.velocity.y + dir.y * intensity.y);
                if ((dir.x != 0 || dir.y != 0))
                {
                    if (moveCallBack != null) moveCallBack();
                }
                else if (idleCallback != null) idleCallback();

            }
            else if (cantMoveCallback != null) cantMoveCallback();
        }

        public void Move_(Vector3 dir, Vector3 intensity, Action moveCallBack = null, Action cantMoveCallback = null, Action idleCallback = null)
        {
            dir = dir.normalized;
            if (canMove)
            {
                rb.velocity = new Vector3(dir.x * intensity.x, rb.velocity.y + dir.y * intensity.y, dir.z * intensity.z);
                if ((dir.x != 0 || dir.y != 0 || dir.z != 0))
                {
                    if (moveCallBack != null) moveCallBack();
                }
                else if (idleCallback != null) idleCallback();
            }
            else if (cantMoveCallback != null) cantMoveCallback();
        }
    }

}