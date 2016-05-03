using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public bool autoStartPosition = true;
	public float hSpeed;
	public Animator anim;
	private float _input;
	private Rigidbody2D _rb;
	public Transform visual;
	public float jump;
	public bool air = false;
	public bool fastFall = false;

	public float stdGravity = 5;
	public float fallGravity = 8;
	public float fastFallSpeed = 5;
	public float maxFallSpeed = 15;

	public float groundCheckDistance = 1f;
	public LayerMask mask;
	public Transform footSensor;
	public float footRadius;

	public bool climbing = false;
	public float climbingSpeed;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		_input = Input.GetAxis("Horizontal");
		if (_input != 0)
		{
			visual.localScale = new Vector3(Mathf.Sign(_input), 1, 1);
		}
		if (Input.GetButtonDown("Jump") && !air)
		{
			_rb.velocity += new Vector2(0, jump);
			air = true;
		}


		var hit = Physics2D.OverlapCircle(footSensor.position, footRadius, mask);
		//Debug.Log(hit);
		if (hit == null)
		{
			air = true;
			transform.SetParent(null);
		} else
		{
			//Debug.Log(hit.collider);
			if (air /* && hit.collider.CompareTag("Ground")*/)
			{
				air = false;
				if (Mathf.Abs(_rb.velocity.y) < 0.1f)
				{
					Camera.main.SendMessage("GroundTouched", SendMessageOptions.DontRequireReceiver);
				}
				if (hit.CompareTag("MovingPlatform"))
				{
					transform.SetParent(hit.gameObject.transform);
				}
			}
		}

		anim.SetBool("climbing", climbing);
		if (climbing)
		{
			var vel = _rb.velocity;
			if (Mathf.Abs(Input.GetAxis("Vertical")) >= 0.1f)
			{
				vel.y = climbingSpeed * Input.GetAxis("Vertical");
			} else
			{
				vel.y = 0;
			}
			_rb.velocity = vel;
			_rb.gravityScale = 0;
		}
	}

	void LateUpdate()
	{
		if (_rb.velocity.y < -fastFallSpeed)
		{
			fastFall = true;
		} else
		{
			fastFall = false;
		}
	}

	void FixedUpdate()
	{
		var vel = _rb.velocity;
		vel.y = Mathf.Clamp(vel.y, -maxFallSpeed, Mathf.Infinity);
		_rb.velocity = new Vector2(_input * hSpeed, vel.y);
		anim.SetFloat("velocidadeX", Mathf.Abs(_rb.velocity.x));
		anim.SetFloat("velocidadeY", Mathf.Abs(_rb.velocity.y));
		if (climbing)
		{
			return;
		}
		if (_rb.velocity.y < 0)
		{
			_rb.gravityScale = fallGravity;
		} else
		{
			_rb.gravityScale = stdGravity;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance, 0));
		Gizmos.DrawWireSphere(footSensor.position, footRadius);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("DeathZone"))
		{
			GameManager.i.SendMessage("Reload", SendMessageOptions.DontRequireReceiver);
		}

		if (other.gameObject.layer == LayerMask.NameToLayer("Stairs"))
		{
			SetClimbing(true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Stairs"))
		{
			SetClimbing(false);
		}
	}

	void SetClimbing(bool state)
	{
		climbing = state;
	}
}
