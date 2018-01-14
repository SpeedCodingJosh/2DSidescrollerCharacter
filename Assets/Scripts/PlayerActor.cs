using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour 
{
	public float moveSpeed = 4, runSpeed = 8, gravityMultiplier = 1;
	public int jumpForce = 6;
	private bool isFacingRight = true;
	private Vector2 move;

	public Transform groundChecker;
	public float groundRadius = 0.1f;
	public LayerMask whatIsGround;

	private Collider2D anythingCollided;
	private bool isGrounded;

	private Rigidbody2D rbody;
	private PlayerControls controls;
	private SpriteRenderer render2D;
	private PlayerAnimations playerAnim;

	private void Start ()
	{
		rbody = GetComponent<Rigidbody2D>();
		controls = GetComponent<PlayerControls>();
		render2D = GetComponent<SpriteRenderer>();
		playerAnim = GetComponent<PlayerAnimations>();
	}

	private void Update ()
	{
		// Check the ground
		CheckIfGrounded();

		// Set movement
		move.x = controls.horizontal;

		// Apply boost when running
		if(controls.isRunning)
		{
			move *= runSpeed;
		}
		else
		{
			// Speed when walking
			move *= moveSpeed;
		}

		if(move.x > 0 && !isFacingRight) // Moving right but not facing right, then flip
		{
			Flip();
		}
		else if(move.x < 0 && isFacingRight)
		{
			Flip();
		}

		// Set animation data
		HandleAnimations();
	}

	private void FixedUpdate ()
	{
		rbody.velocity = new Vector2(move.x, rbody.velocity.y);

		if(controls.jumped && isGrounded)
		{
			rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
		}
	}

	private void Flip ()
	{
		isFacingRight = !isFacingRight; // Set the opposite value

		//render2D.flipX = !isFacingRight; // the flip value of renderer should be opposite
		Vector3 scale = transform.localScale;
		scale.x *= -1;

		transform.localScale = scale;
	}

	private void CheckIfGrounded ()
	{
		anythingCollided = Physics2D.OverlapCircle(groundChecker.position, groundRadius, whatIsGround);

		if(anythingCollided != null) // Collided with something that is not the player
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
			// We are on air so apply any extra gravity if needed
			rbody.AddForce((Physics2D.gravity * gravityMultiplier) - Physics2D.gravity);
		}
	}

	private void HandleAnimations ()
	{
		// Set movement, are we moving?
		playerAnim.SetValue("IsMoving", controls.AreWeMoving());
		// Are we running?
		playerAnim.SetValue("IsRunning", controls.isRunning);
		// Are we grounded
		playerAnim.SetValue("IsGrounded", isGrounded);
		// Y velocity
		playerAnim.SetValue("YVelocity", rbody.velocity.y);
	}
}
