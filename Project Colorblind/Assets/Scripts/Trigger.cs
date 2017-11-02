using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Purpose: A parent class that all trigger scripts inherit from.
/// </summary>
public class Trigger : MonoBehaviour
{
    #region Trigger Members
    protected bool m_activated;
    public bool Activated
    {
        get { return m_activated; }
    }
    #endregion
}
