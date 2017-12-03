using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 12/2/17
/// Purpose: Defines a ghost version of the player script. Less collisions and mechanics.
/// </summary>
[RequireComponent(typeof(GhostController2D))]
public class GhostPlayer : MonoBehaviour
{
    #region GhostPlayer Members
    private bool m_dead;
    private bool m_dying;
    public bool Dead { get { return m_dead; } }
    public bool Dying { get { return m_dying; } }
    public float deathTime;

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
        if (!Dying)
        {
            CalculateVelocity();
            controller.Move(velocity * Time.deltaTime);
        }
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
    #endregion
}
