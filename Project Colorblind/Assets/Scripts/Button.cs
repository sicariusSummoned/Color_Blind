using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 11/2/2017
/// Purpose: Acts as a trigger for a door to move.
/// </summary>
public class Button : Trigger
{
    #region Button Members

    [Header("N, S, E, or W (North, south...etc)")]
    public string m_animationDirection;
    private float m_speed = 0.25f;
    #endregion

    #region Button Methods
    private void Start()
    {
        m_activated = false;
        EventManager.Instance.OnButtonHit += CheckHit;
    }

    private void CheckHit(int id)
    {
        if (gameObject.GetInstanceID() == id)
            GetHit();
    }

    private void GetHit()
    {
        // Animate away
        switch (m_animationDirection)
        {
            case "N": transform.Translate(Vector3.up * m_speed); break;
            case "S": transform.Translate(Vector3.down * m_speed); break;
            case "E": transform.Translate(Vector3.right * m_speed); break;
            case "W": transform.Translate(Vector3.left * m_speed); break;
        }

        // Set to activated which 
        m_activated = true;

        // Play sound
        SoundManager.instance.PlayButtonSound();

    }
    #endregion
}