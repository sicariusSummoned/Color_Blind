using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 9/11/2017
/// Purpose: Controls each player in the game.
/// </summary>

/// A Note On Coding Sources
/// Code in this class is derived in part from the following sources:
/// https://www.gamasutra.com/blogs/YoannPignole/20131010/202080/The_hobbyist_coder_1_2D_platformer_controller.php
/// http://yoannpignole.fr/files/hobbyistCoder/PlatformerController.cs
/// https://www.youtube.com/user/Cercopithecan

[RequireComponent(typeof(PlayerParameters))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    #region PlayerController Members

    // Player State and Parameters
    public PlayerState State { get; protected set; }
    public PlayerParameters m_overrideParameters;
    public PlayerParameters DefaultParameters;
    public PlayerParameters Parameters
    {
        get
        {
            return m_overrideParameters ?? DefaultParameters;
        }
    }

    // Physics and Colliders
    public SpriteRenderer m_spriteRenderer;
    public Vector2 ForcesApplied { get; protected set; }
    public Vector2 Speed
    {
        get
        {
            return m_speed;
        }
    }
    public float m_rayDistance;

    // Properties for Collider Syntactic Sugar
    #region Collider Properties
    public Vector2 ColliderSize
    {
        get
        {
            return Vector3.Scale(transform.localScale, m_spriteRenderer.size);
        }
    }

    public Vector2 ColliderCenterPosition
    {
        get
        {
            return m_spriteRenderer.bounds.center;
        }
    }

    public Vector2 ColliderBottomPosition
    {
        get
        {
            m_boundsBottomCenterPosition.x = m_spriteRenderer.bounds.center.x;
            m_boundsBottomCenterPosition.y = m_spriteRenderer.bounds.min.y;
            return m_boundsBottomCenterPosition;
        }
    }

    public Vector2 ColliderTopPosition
    {
        get
        {
            m_boundsTopCenterPosition.x = m_spriteRenderer.bounds.center.x;
            m_boundsTopCenterPosition.y = m_spriteRenderer.bounds.min.y;
            return m_boundsTopCenterPosition;
        }
    }

    public Vector2 ColliderLeftPosition
    {
        get
        {
            m_boundsLeftCenterPosition.x = m_spriteRenderer.bounds.min.x;
            m_boundsLeftCenterPosition.y = m_spriteRenderer.bounds.center.y;
            return m_boundsLeftCenterPosition;
        }
    }

    public Vector2 ColliderRightPosition
    {
        get
        {
            m_boundsRightCenterPosition.x = m_spriteRenderer.bounds.center.x;
            m_boundsRightCenterPosition.y = m_spriteRenderer.bounds.min.y;
            return m_boundsRightCenterPosition;
        }
    }

    public Vector2 ColliderBottomLeft
    {
        get
        {
            m_boundsBottomLeftPosition.x = m_spriteRenderer.bounds.min.x;
            m_boundsBottomLeftPosition.y = m_spriteRenderer.bounds.min.y;
            return m_boundsBottomLeftPosition;
        }
    }

    public Vector2 ColliderBottomRight
    {
        get
        {
            m_boundsBottomRightPosition.x = m_spriteRenderer.bounds.max.x;
            m_boundsBottomRightPosition.y = m_spriteRenderer.bounds.min.y;
            return m_boundsBottomRightPosition;
        }
    }

    public Vector2 ColliderTopLeft
    {
        get
        {
            m_boundsTopLeftPosition.x = m_spriteRenderer.bounds.min.x;
            m_boundsTopLeftPosition.y = m_spriteRenderer.bounds.max.y;
            return m_boundsTopLeftPosition;
        }
    }

    public Vector2 ColliderTopRight
    {
        get
        {
            m_boundsTopRightPosition.x = m_spriteRenderer.bounds.max.x;
            m_boundsTopRightPosition.y = m_spriteRenderer.bounds.max.y;
            return m_boundsTopRightPosition;
        }
    }
    #endregion

    // Local data for internal class usage
    #region Local Data 
    protected Vector2 m_speed;
    protected float m_currentGravity = 0;
    protected Vector2 m_externalForce;
    protected Vector2 m_newPosition;
    protected Transform m_transform;
    protected GameObject m_lastStandingOn;
    protected bool m_gravityActive = true;
    protected RaycastHit2D m_ray;
    protected float m_rayBuffer = 0.01f;
    protected float m_minX = float.MinValue;
    protected float m_maxX = float.MaxValue;
    protected float m_minY = float.MinValue;
    protected float m_maxY = float.MaxValue;

    protected Vector3 m_boundsBottomCenterPosition;
    protected Vector3 m_boundsTopCenterPosition;
    protected Vector3 m_boundsLeftCenterPosition;
    protected Vector3 m_boundsRightCenterPosition;
    protected Vector3 m_boundsBottomLeftPosition;
    protected Vector3 m_boundsBottomRightPosition;
    protected Vector3 m_boundsTopLeftPosition;
    protected Vector3 m_boundsTopRightPosition;
    protected float Width
    {
        get { return m_spriteRenderer.bounds.size.x; }
    }
    protected float Height
    {
        get { return m_spriteRenderer.bounds.size.y; }
    }
    #endregion

    #endregion

    #region PlayerController Methods

    protected void Awake()
    {
        // Get component references
        m_transform = transform;

        // State initialization
        State = new PlayerState();
        State.Reset();
    }

    protected void Update()
    {
        // Every frame we apply gravity
        // Every frame we check for collisions
        // Every frame we modify player position

        FrameBegin();

        ApplyGravity();
        ApplySpeed();
        CheckForCollisions();
        MovePlayer();

        FrameEnd();
    }

    #region Force Methods
    public void AddForce(Vector2 force)
    {
        m_speed += force;
        m_externalForce += force;
    }

    public void AddHorizontalForce(float forcex)
    {
        m_speed.x += forcex;
        m_externalForce.x += forcex;
    }

    public void AddVerticalForce(float forcey)
    {
        m_speed.y += forcey;
        m_externalForce.y += forcey;
    }

    public void SetForce(Vector2 force)
    {
        m_speed = force;
        m_externalForce = force;
    }

    public void SetHorizontalForce(float forcex)
    {
        m_speed.x = forcex;
        m_externalForce.x = forcex;
    }

    public void SetVerticalForce(float forcey)
    {
        m_speed.y = forcey;
        m_externalForce.y = forcey;
    }
    #endregion

    protected void FrameBegin()
    {
        // Prepare for next position
        State.WasGroundedLastFrame = State.IsCollidingBelow;
        State.WasTouchingTheCeilingLastFrame = State.IsCollidingAbove;
        State.Reset();
    }

    protected void FrameEnd()
    {
        SetStates();

        // Remove applied forces at the end of every frame
        m_externalForce.x = 0;
        m_externalForce.y = 0;
    }

    protected void GravityActive(bool state)
    {
        m_gravityActive = state;
    }

    protected void ApplyGravity()
    {
        // Set gravity and apply ascent and falling multipliers
        m_currentGravity = Parameters.Gravity;
        if (m_speed.y > 0) m_currentGravity = m_currentGravity / Parameters.AscentMultiplier;
        if (m_speed.y < 0) m_currentGravity = m_currentGravity * Parameters.FallMultiplier;

        // Apply gravity to speed
        if (m_gravityActive)
            m_speed.y += m_currentGravity * Time.deltaTime;
    }

    protected void ApplySpeed()
    {
        m_newPosition = Speed * Time.deltaTime;
    }

    protected void CheckForCollisions()
    {
        if (m_speed.x > 0)
            RaycastRight();
        else if (m_speed.x < 0)
            RaycastLeft();

        if (m_speed.y > 0)
            RaycastAbove();
        else if (m_speed.y < 0)
            RaycastBelow();
        //RaycastLeft();
        //RaycastRight();
        //RaycastBelow();
        //RaycastAbove();
    }

    protected void RaycastLeft()
    {
        // Raycast from the top left
        float rayLength = Mathf.Abs(m_speed.x * Time.deltaTime);
        m_ray = Physics2D.Raycast(ColliderTopLeft, Vector2.left, rayLength);

        // If we hit
        if (m_ray.distance > 0)
        {
            // Halt right movement
            State.IsCollidingLeft = true;
            HandleCollisionObject(m_ray.collider.gameObject);
            m_newPosition.x = -Mathf.Abs(m_ray.point.x - ColliderTopLeft.x);
            m_speed.x = 0;
            return;
        }

        // Raycast from the bottom right
        rayLength = Mathf.Abs(m_speed.x * Time.deltaTime);
        m_ray = Physics2D.Raycast(ColliderBottomLeft, Vector2.left, rayLength);

        // If we hit
        if (m_ray.distance > 0)
        {
            // Halt right movement
            State.IsCollidingLeft = true;
            HandleCollisionObject(m_ray.collider.gameObject);
            m_newPosition.x = -Mathf.Abs(m_ray.point.x - ColliderBottomLeft.x);
            m_speed.x = 0;
            return;
        }
    }

    protected void RaycastRight()
    {
        // Raycast from the top right
        float rayLength = Mathf.Abs(m_speed.x * Time.deltaTime);
        m_ray = Physics2D.Raycast(ColliderTopRight, Vector2.right, rayLength);

        // If we hit
        if (m_ray.distance > 0)
        {
            // Halt right movement
            State.IsCollidingRight = true;
            HandleCollisionObject(m_ray.collider.gameObject);
            m_maxX = m_ray.point.x + Width / 2 - m_rayBuffer * 2;
            m_speed.x = 0;
            //Debug.Break();
        }

        // Raycast from the bottom right
        rayLength = Mathf.Abs(m_speed.x * Time.deltaTime);
        m_ray = Physics2D.Raycast(ColliderBottomRight, Vector2.right, rayLength);

        // If we hit
        if (m_ray.distance > 0)
        {
            // Halt right movement
            State.IsCollidingRight = true;
            HandleCollisionObject(m_ray.collider.gameObject);

            float temp;
            if ((temp = m_ray.point.x + Width / 2 - m_rayBuffer * 2) < m_maxX)
                m_maxX = temp;
            m_speed.x = 0;
            //Debug.Break();
        }

        // If we didn't hit anything, change maxX
        if (!State.IsCollidingRight)
            m_maxX = float.MaxValue;

        //Vector2 right = new Vector2(ColliderRightPosition.x + 2, ColliderCenterPosition.y);
        //Vector2 rightTop = new Vector2(ColliderRightPosition.x + 2, ColliderTopPosition.y - 2);
        //Vector2 rightBottom = new Vector2(ColliderRightPosition.x + 2, ColliderBottomPosition.y + 2);
        ////RaycastHit2D rayHitTop;
        ////RaycastHit2D rayHitRight;
        ////RaycastHit2D rayHitBottom;

        //Vector3 limitRight = right + Vector2.right * m_rayDistance;
        //Vector3 limitRightTop = rightTop + Vector2.right * m_rayDistance;
        //Vector3 limitRightBottom = rightBottom + Vector2.right * m_rayDistance;

        //RaycastHit2D rayHitTop = Physics2D.Raycast(rightTop, Vector2.right, m_rayDistance);
        //RaycastHit2D rayHitRight = Physics2D.Raycast(right, Vector2.right, m_rayDistance);
        //RaycastHit2D rayHitBottom = Physics2D.Raycast(rightBottom, Vector2.right, m_rayDistance);

        //if (rayHitRight.distance > 0)
        //{
        //    limitRight = rayHitRight.point;
        //    m_speed.x = 0;
        //}

        //if (rayHitTop.distance > 0)
        //{
        //    limitRightTop = rayHitTop.point;
        //    m_speed.x = 0;
        //}

        //if (rayHitBottom.distance > 0)
        //{
        //    limitRightBottom = rayHitBottom.point;
        //    m_speed.x = 0;
        //}

        //m_maxX = Mathf.Min(limitRight.x, limitRightTop.x, limitRightBottom.x);
    }

    protected void RaycastBelow()
    {
        // Raycast from the bottom left
        float rayLength = Mathf.Abs(m_speed.y * Time.deltaTime);
        m_ray = Physics2D.Raycast(ColliderBottomLeft, Vector2.down, rayLength);

        // If we hit
        if (m_ray.distance > 0)
        {
            // Halt bottom movement
            State.IsCollidingBelow = true;
            HandleCollisionObject(m_ray.collider.gameObject);

            if (!State.WasGroundedLastFrame)
                m_minY = m_ray.point.y + Height / 2 + m_rayBuffer * 2;
            m_speed.y = 0;
        }

        // Raycast from the bottom right
        rayLength = Mathf.Abs(m_speed.y * Time.deltaTime);
        m_ray = Physics2D.Raycast(ColliderBottomRight, Vector2.down, rayLength);

        // If we hit
        if (m_ray.distance > 0)
        {
            // Halt bottom movement
            State.IsCollidingBelow = true;
            HandleCollisionObject(m_ray.collider.gameObject);

            if (!State.WasGroundedLastFrame)
            {
                float temp;
                if ((temp = m_ray.point.y + Height / 2 + m_rayBuffer * 2) < m_minY)
                    m_minY = temp;
            }
            m_speed.y = 0;
        }

        // If we didn't hit anything, change miny
        if (!State.IsCollidingBelow)
            m_minY = float.MinValue;

        //Vector2 bottom = new Vector2(ColliderCenterPosition.x, ColliderBottomPosition.y);
        //Vector2 bottomRight = new Vector2(ColliderRightPosition.x + 2, ColliderBottomPosition.y + 2);
        //Vector2 bottomLeft = new Vector2(ColliderLeftPosition.x - 2, ColliderBottomPosition.y + 2);
        ////RaycastHit2D rayHitTop;
        ////RaycastHit2D rayHitRight;
        ////RaycastHit2D rayHitBottom;

        //Vector3 limitBottom = bottom + Vector2.down * m_rayDistance;
        //Vector3 limitBottomRight = bottomRight + Vector2.down * m_rayDistance;
        //Vector3 limitBottomLeft = bottomLeft + Vector2.down * m_rayDistance;

        //RaycastHit2D rayHitBottom = Physics2D.Raycast(bottom, Vector2.down, m_rayDistance);
        //RaycastHit2D rayHitBottomRight = Physics2D.Raycast(bottomRight, Vector2.down, m_rayDistance);
        //RaycastHit2D rayHitBottomLeft = Physics2D.Raycast(bottomLeft, Vector2.down, m_rayDistance);

        //if (rayHitBottom.distance > 0)
        //{
        //    limitBottom = rayHitBottom.point;
        //    m_speed.y = 0;
        //}

        //if (rayHitBottomRight.distance > 0)
        //{
        //    limitBottomRight = rayHitBottomRight.point;
        //    m_speed.y = 0;
        //}

        //if (rayHitBottomLeft.distance > 0)
        //{
        //    limitBottomLeft = rayHitBottomLeft.point;
        //    m_speed.y = 0;
        //}

        //m_minY = Mathf.Min(limitBottom.y, limitBottomRight.y, limitBottomLeft.y);
    }

    protected void RaycastAbove()
    {
        // Raycast from the top left
        float rayLength = Mathf.Abs(m_speed.y * Time.deltaTime);
        m_ray = Physics2D.Raycast(ColliderTopLeft, Vector2.up, rayLength);

        // If we hit
        if (m_ray.distance > 0)
        {
            // Halt bottom movement
            State.IsCollidingAbove = true;
            HandleCollisionObject(m_ray.collider.gameObject);
            m_newPosition.y = Mathf.Abs(m_ray.point.y - ColliderTopLeft.y);
            m_speed.y = 0;
            return;
        }

        // Raycast from the top right
        rayLength = Mathf.Abs(m_speed.y * Time.deltaTime);
        m_ray = Physics2D.Raycast(ColliderTopRight, Vector2.up, rayLength);

        // If we hit
        if (m_ray.distance > 0)
        {
            // Halt bottom movement
            State.IsCollidingAbove = true;
            HandleCollisionObject(m_ray.collider.gameObject);
            m_newPosition.y = Mathf.Abs(m_ray.point.y - ColliderTopRight.y);
            m_speed.y = 0;
            return;
        }
    }

    protected void HandleCollisionObject(GameObject collider)
    {

    }

    protected void MovePlayer()
    {
        ForcesApplied = Speed;

        // Create limits
        m_minX = m_minX + m_spriteRenderer.bounds.size.x / 2 - m_spriteRenderer.bounds.center.x;
        m_maxX = m_maxX - m_spriteRenderer.bounds.size.x / 2 - m_spriteRenderer.bounds.center.x;
        m_minY = m_minY + m_spriteRenderer.bounds.size.y / 2 - m_spriteRenderer.bounds.center.y;
        m_maxY = m_minY - m_spriteRenderer.bounds.size.y / 2 - m_spriteRenderer.bounds.center.y;

        m_transform.Translate(m_newPosition, Space.World);

        //if (m_transform.position.x > m_maxX)
        //    m_transform.position = new Vector3(m_maxX, m_transform.position.y);
        //if (m_transform.position.x < m_minX)
        //    m_transform.position = new Vector3(m_minX, m_transform.position.y);
        //if (m_transform.position.y > m_maxY)
        //    m_transform.position = new Vector3(m_transform.position.x, m_maxY);
        //if (m_transform.position.y < m_minY)
        //    m_transform.position = new Vector3(m_transform.position.x, m_minY);
    }

    protected void SetStates()
    {
        if (!State.WasGroundedLastFrame && State.IsCollidingBelow)
        {
            State.JustGotGrounded = true;
        }
        else if (State.JustGotGrounded)
        {
            State.JustGotGrounded = false;
        }
    }

    #endregion
}
