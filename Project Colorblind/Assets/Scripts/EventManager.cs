using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 11/2/2017
/// Purpose: Singleton that triggers events.
/// </summary>
public class EventManager : MonoBehaviour
{
    #region EventManager Members
    public static EventManager Instance;

    #region EventManager Events
    public delegate void DeathEvent();
    public event DeathEvent OnDeath;

    public delegate void SeeSawActiveEvent();
    public event SeeSawActiveEvent OnSeeSawActive;

    public delegate void SeeSawInactiveEvent();
    public event SeeSawInactiveEvent OnSeeSawInactive;

    public delegate void FadeBlockActiveEvent();
    public event FadeBlockActiveEvent OnFadeBlock;

    public delegate void ButtonHitEvent(int id);
    public event ButtonHitEvent OnButtonHit;
    #endregion
    #endregion

    #region EventManager Methods
    void Awake()
    {
        // Destroy if there are multiple instances
        if (Instance != null && Instance != this)
            Destroy(gameObject);

            // Stay persistent between levels
            // DontDestroyOnLoad(gameObject);
            Instance = this;
    }

    public void ProcessCollision(RaycastHit2D hit, string tag)
    {
        if (tag == "RedKillsPlayer" ||
            tag == "GreenKillsPlayer" ||
            tag == "BlueKillsPlayer" ||
            tag == "WhiteKillsPlayer")
        {
            Instance.PlayerDeath();
            return;
        }

        if (tag == "SeeSaw")
            Instance.SeeSawActive();

        else if (tag != "SeeSaw")
            Instance.SeeSawInactive();

        if (tag == "FadeBlock")
            Instance.FadeBlockActive();

        if (tag == "Button")
            Instance.OnButtonHit(hit.collider.gameObject.GetInstanceID());
    }

    private void PlayerDeath()
    {
        if (OnDeath != null)
            OnDeath();
    }

    private void SeeSawActive()
    {
        if (OnSeeSawActive != null)
            OnSeeSawActive();
    }

    private void SeeSawInactive()
    {
        if (OnSeeSawInactive != null)
            OnSeeSawInactive();
    }

    private void FadeBlockActive()
    {
        if (OnFadeBlock != null)
            OnFadeBlock();
    }
    #endregion
}
