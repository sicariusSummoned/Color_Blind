using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 12/14/2017
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

    #region EventManager Player References
    List<Player> m_players;
    List<GhostPlayer> m_ghostPlayers;
    #endregion
    #endregion

    #region EventManager Methods
    void Awake()
    {
        // Destroy if there are multiple instances
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        // Stay persistent between levels
        //DontDestroyOnLoad(gameObject);
        Instance = this;

        // Get Players
        m_players = new List<Player>();
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            m_players.Add(player.GetComponent<Player>());
        }

        // Get Ghost Players
        m_ghostPlayers = new List<GhostPlayer>();
        foreach (var ghostPlayer in GameObject.FindGameObjectsWithTag("SpotlightPlayer"))
        {
            m_ghostPlayers.Add(ghostPlayer.GetComponent<GhostPlayer>());
        }
    }

    public void ProcessCollision(RaycastHit2D hit, string tag)
    {
        if ((tag == "RedKillsPlayer" ||
            tag == "GreenKillsPlayer" ||
            tag == "BlueKillsPlayer" ||
            tag == "WhiteKillsPlayer") &&
            PlayersNotDyingOrDead())
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

    private bool PlayersNotDyingOrDead()
    {
        foreach (var player in m_players)
            if (player.Dying || player.Dead)
                return false;

        foreach (var ghostPlayer in m_ghostPlayers)
            if (ghostPlayer.Dying || ghostPlayer.Dead)
                return false;

        return true;
    }
    #endregion
}
