using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/19/2017
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

    public void PlayerDeath()
    {
        if (OnDeath != null)
            OnDeath();
    }

    public void SeeSawActive()
    {
        if (OnSeeSawActive != null)
            OnSeeSawActive();
    }

    public void SeeSawInactive()
    {
        if (OnSeeSawInactive != null)
            OnSeeSawInactive();
    }

    public void FadeBlockActive()
    {
        if (OnFadeBlock != null)
            OnFadeBlock();
    }
    #endregion
}
