using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] private float moveSpeed = 7.5f;

	[Header("Jump")]
	[SerializeField] private float jumpForce = 5f;
	[SerializeField] private float jumpTime = 0.5f;

	[Header("Turn Check")]
	[SerializeField] private GameObject lLeg;
	[SerializeField] private GameObject rLeg;

	[Header("Ground Check")]
	[SerializeField] private float extraHeight = 0.25f;
	[SerializeField] private LayerMask whatIsGround;

	[HideInInspector] public bool isFacingRight;

	private Rigidbody2D rb;
	private Collider2D coll;
	private float moveInput;
	private Animator anim;

	private bool isJumping;
	private bool isFalling;
	private float jumpTimeCounter;

	private RaycastHit2D groundHit;

	private Coroutine resetTriggerCoroutine;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();
		StartDirectionCheck();
	}

	private void Update()
	{
		Move();
		Jump();
	}

	#region Movement Functions
	private void Move()
	{
		moveInput = UserInput.instance.moveInput.x;

		if (moveInput > 0 || moveInput < 0)
		{
			anim.SetBool("isWalking", true);
			TurnCheck();
		}
		else
		{
			anim.SetBool("isWalking", false);
		}
		rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
	}

	private void Jump()
	{
		// button was pushed this frame
		if (UserInput.instance.controls.Jumping.Jump.WasPressedThisFrame() && IsGrounded())
		{
			isJumping = true;
			jumpTimeCounter = jumpTime;
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);

			anim.SetTrigger("jump");
		}
		// button is being held
		if (UserInput.instance.controls.Jumping.Jump.IsPressed())
		{
			if (jumpTimeCounter > 0 && isJumping)
			{
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
			else if (jumpTimeCounter == 0)
			{
				isFalling = true;
				isJumping = false;
			}
			else
			{
				isJumping = false;
			}
		}
		// button was release this frame
		if (UserInput.instance.controls.Jumping.Jump.WasReleasedThisFrame())
		{
			isJumping = false;
			isFalling = true;
		}

		if (!isJumping && CheckForLand())
		{
			anim.SetTrigger("land");
		}
	}
	#endregion

	#region Ground/Landed check
	private bool IsGrounded()
	{
		groundHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);
		if (groundHit.collider != null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private bool CheckForLand()
	{
		if (isFalling)
		{
			if (IsGrounded())
			{
				isFalling = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}

	private IEnumerator Reset()
	{
		yield return null;
		anim.ResetTrigger("land");
		resetTriggerCoroutine = StartCoroutine(Reset());
	}
	#endregion

	#region Turn Check

	private void StartDirectionCheck()
	{
		if (rLeg.transform.position.x > lLeg.transform.position.x)
		{
			isFacingRight = true;
		}
		else
		{
			isFacingRight = false;
		}
	}

	private void TurnCheck()
	{
		if (UserInput.instance.moveInput.x > 0 && !isFacingRight)
		{
			Turn();
		}
		else if (UserInput.instance.moveInput.x < 0 && isFacingRight)
		{
			Turn();
		}
	}

	private void Turn()
	{
		if (isFacingRight)
		{
			Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
			transform.rotation = Quaternion.Euler(rotator);
			isFacingRight = !isFacingRight;
		}
		else
		{
			Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
			transform.rotation = Quaternion.Euler(rotator);
			isFacingRight = !isFacingRight;
		}
	}
	#endregion
}
