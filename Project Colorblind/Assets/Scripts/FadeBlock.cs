using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 9/28/2017
/// Purpose: Player interaction fades the block and causes it to stop colliding.
/// </summary>
public class FadeBlock : MonoBehaviour
{
    #region FadeBlock Members
    private BoxCollider2D m_collider;
    public SpriteRenderer m_sprite;
    private Color m_tempColor;
    private Color m_maxColor;
    public bool m_activated = false;
    public bool m_reappears = false;
    public bool m_reappearing = false;
    public bool m_fading = false;
    public bool m_faded = false;
    #endregion

    #region FadeBlock Methods
    private void Awake()
    {
        m_collider = GetComponent<BoxCollider2D>();
        m_sprite = GetComponentInChildren<SpriteRenderer>();

        // Determine max color
        m_maxColor = m_sprite.color;
    }

    private void OnEnable()
    {
        EventManager.Instance.OnFadeBlock += Activate;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnFadeBlock -= Activate;
    }

    private void LateUpdate()
    {
        // If not being touched, start reappearing if possible
        if (!m_activated && 
            m_reappears && 
            m_sprite.color.a != m_maxColor.a &&
            !m_reappearing)
            StartCoroutine(Reappear());

        // Reset activation
        m_activated = false;
    }

    public void Activate()
    {
        // Activated
        m_activated = true;

        // Start fading if not faded or already fading
        if (!m_faded && !m_fading)
        {
            StartCoroutine(Fade());
            return;
        }
    }

    private IEnumerator Fade()
    {
        m_fading = true;

        while (true)
        {
            // While fading if the player stops touching us, break
            if (!m_fading)
                yield break;

            // Change material opacity
            m_tempColor = m_sprite.color;
            m_tempColor.a -= 0.001f;
            m_sprite.color = m_tempColor;

            // If faded, break
            if (m_sprite.color.a <= 0)
                break;

            yield return null;
        }

        m_faded = true;
        m_fading = false;

        // Disable collisions
        m_collider.enabled = false;
    }

    private IEnumerator Reappear()
    {
        // Reenable collisions
        m_collider.enabled = true;
        m_reappearing = true;

        while (true)
        {
            // While reappearing if the player touches us, break
            if (m_activated)
            {
                m_reappearing = false;
                yield break;
            }

            // Change material opacity
            m_tempColor = m_sprite.color;
            m_tempColor.a += 0.001f;

            // Cap the color alpha at starting alpha
            // We are now fully reappeared so exit coroutine
            if (m_tempColor.a > m_maxColor.a)
            {
                m_tempColor.a = m_maxColor.a;
                m_sprite.color = m_tempColor;
                break;
            }

            m_sprite.color = m_tempColor;
            yield return null;
        }

        m_faded = false;
        m_reappearing = false;
    }
    #endregion
}
