using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .1f;
    private float accelerationTimeGrounded = .1f;
    private float moveSpeed = 6f;

	public float jumpPadHeight=8f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public Vector3 respawnPoint;
    public LevelManager gameLevelManager;
    public float deathTime;

    public bool canDoubleJump;
    private bool isDoubleJumping = false;

    private bool m_dead;
    private bool m_dying;
    public bool Dead { get { return m_dead; } }
    public bool Dying { get { return m_dying; } }

    public float wallSlideSpeedMax = 3f;
    public float wallStickTime = .25f;
    private float timeToWallUnstick;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;


    public SpriteRenderer spriteRenderer;
    private Controller2D controller;

    private Vector2 directionalInput;
    private bool wallSliding;
    private int wallDirX;
	private bool onPad;

    private void Start()
    {
		onPad = false;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        respawnPoint = transform.position;
        gameLevelManager = FindObjectOfType<LevelManager>();

        EventManager.Instance.OnDeath += Die;
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnDeath -= Die;
    }

    public bool OnPad{
		get{ return onPad; }
		set{ onPad = value; }

	}

    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    public float Gravity
    {
        get { return gravity; }
        
    }

    private void Update()
    {
        if (!Dying)
        {
            CalculateVelocity();
            HandleWallSliding();

            controller.Move(velocity * Time.deltaTime, directionalInput, false, true);

            if (controller.collisions.above || controller.collisions.below && onPad == false)
            {
                if (!controller.collisions.above)
                    velocity.y = 0f;
            }
        }
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public Vector2 GetDirectionalInput()
    {
        return directionalInput;
    }

	public void JumpPad(){
		velocity.y = jumpPadHeight;
	}

    public void OnJumpInputDown()
    {
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
            isDoubleJumping = false;
        }
        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = false;
        }
        if (canDoubleJump && !controller.collisions.below && !isDoubleJumping && !wallSliding)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = true;
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    private void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0f)
            {
                velocityXSmoothing = 0f;
                velocity.x = 0f;
                if (directionalInput.x != wallDirX && directionalInput.x != 0f)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
    }

    private void CalculateVelocity()
    {
        velocity.x = directionalInput.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;

        if (velocity.x > 0 && transform.localScale.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (velocity.x < 0 && transform.localScale.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void Die()
    {
        SoundManager.instance.PlayDeathSound(3);
        StartCoroutine(DeathTimer());
    }

    public void Respawn(Vector3 position)
    {
        m_dead = false;
        transform.position = position;
    }

    private IEnumerator DeathTimer()
    {
        m_dying = true;
        yield return new WaitForSeconds(deathTime);
        m_dying = false;
        m_dead = true;
    }
}
