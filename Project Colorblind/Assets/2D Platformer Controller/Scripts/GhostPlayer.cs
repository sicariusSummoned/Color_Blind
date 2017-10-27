using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/24/17
/// Purpose: Defines a ghost version of the player script. Less collisions and mechanics.
/// </summary>
[RequireComponent(typeof(GhostController2D))]
public class GhostPlayer : MonoBehaviour
{
    #region GhostPlayer Members
    private bool m_dead;
    public bool Dead { get { return m_dead; } }

    private float moveSpeed = 6f;

    private float velocityXSmoothing;
    private float velocityYSmoothing;
    private Vector2 directionalInput;
    private Vector3 velocity;

    private GhostController2D controller;
    #endregion

    #region GhostController Methods
    private void Start()
    {
        controller = GetComponent<GhostController2D>();
        EventManager.Instance.OnDeath += Die;
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnDeath -= Die;
    }

    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    private void Update()
    {
        CalculateVelocity();
        controller.Move(velocity * Time.deltaTime);//, directionalInput);
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    private void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        float targetVelocityY = directionalInput.y * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, .5f);
        velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityY, ref velocityYSmoothing, .5f);
    }

    public void Die()
    {
        m_dead = true;
        gameObject.SetActive(false);
    }

    public void Respawn(Vector3 position)
    {
        m_dead = false;
        transform.position = position;
        gameObject.SetActive(true);
    }
    #endregion
}
