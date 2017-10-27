using UnityEngine;

/// <summary>
/// Programmer: Dante Nardo
/// Last Modified: 10/24/2017
/// Purpose: Determines the input for each different ghost player.
/// </summary>
[RequireComponent(typeof(GhostPlayer))]
public class GhostPlayerInput : MonoBehaviour
{
    private GhostPlayer m_player;
    public string m_horizontal;
    public string m_vertical;

    private void Start()
    {
        m_player = GetComponent<GhostPlayer>();
    }

    private void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw(m_horizontal), Input.GetAxisRaw(m_vertical));
        m_player.SetDirectionalInput(directionalInput);
    }
}
