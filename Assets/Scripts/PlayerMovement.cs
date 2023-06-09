using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Assets
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float xSpeed = 0f;

        [SerializeField]
        private float ySpeed = 0f;

        [SerializeField]
        private LayerMask jumpableGround;

        // AudioSource组件游戏播放音效。
        [SerializeField]
        private AudioSource jumpSoundEffect;

        // RigidBody2D 组件
        // RigidBody component
        public Rigidbody2D rb { get; set; }

        public Animator anim { get; set; }
        //private Animator anim;

        public SpriteRenderer sprite { get; set; }

        public enum MovementState { idle, running, jumping, falling }

        public BoxCollider2D boxCollider { get; set; }

        // 在第一框更新之前调用开始函数
        // Start is called before the first frame update
        public void Start()
        {
            // 获取脚本所附件的RigidBody2D组件
            // Get the RigidBody2D component that the script is attached to
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        public void UpdateAnimationState(float xDirection, float yVelocity)
        {
            MovementState state = MovementState.idle;
            if (xDirection > 0f)
            {
                // 如果xDirection大于零，用户按了右箭头键。这时候，Unity应该放running的动画。
                state = MovementState.running;
                sprite.flipX = false;
            }
            else if (xDirection < 0)
            {
                state = MovementState.running;
                // 用户摁了左箭头键，所以我们应该在X轴上反转精灵。
                sprite.flipX = true;
            }

            if (yVelocity > 0.1f)
            {
                state = MovementState.jumping;
            }
            else if (yVelocity < -0.1f)
            {
                state = MovementState.falling;
            }

            anim.SetInteger("state", (int)state);
        }


        // 没框更新时调用更新函数
        // Update is called once per frame
        public void Update()
        {
            // 如果用户按左箭头键，则Input.GetAxisRaw("Horizontal")返回-1。
            // If the user presses the left arrow key, Input.GetAxisRaw("Horizontal") returns -1.
            // 如果用户按右箭头键，则Input.GetAxisRaw("Horizontal")返回1。
            // If the user presses the right arrow key, Input.GetAxisRaw("Horizontal") returns 1.

            //用户释放了键以后，Input.GetAxis("Horizontal")不会马上返回0。反而，它的返回值会渐渐地减少至零。
            // When the user releases a key, Input.GetAxis("Horizontal") won't immediately return 0. Instead, the return value will gradually decrease to 0.
            float xDirection = Input.GetAxisRaw("Horizontal");

            // 改变RigidBody2D组件的水平速度，但不改变垂直的速度。
            // Change the horizontal velocity of the RigidBody2D object without changing the vertical velocity
            rb.velocity = new Vector2(xDirection * xSpeed, rb.velocity.y);


            // 当用户按下空格键时，Input.GetButtonDown()返回true
            // When the user presses the space button, Input.GetButtonDown() returns true.
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                // 改变RigidBody2D组件的垂直速度
                // Change the vertical velocity of the RigidBody2D object
                rb.velocity = new Vector2(rb.velocity.x, ySpeed);
                jumpSoundEffect.Play();
            }

            UpdateAnimationState(xDirection, rb.velocity.y);
        }

        public bool IsGrounded()
        {
            // 这个函数调用会在玩家周围创造一个新的矩形，会把它往下挪一点，并检查它是否与地面重叠。
            return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        }
    }
}
