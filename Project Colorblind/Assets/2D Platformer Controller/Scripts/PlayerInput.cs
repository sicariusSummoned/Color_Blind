using UnityEngine;

/// <summary>
/// Programmer: Dante Nardo
/// Last Modified: 9/21/2017
/// Purpose: Determines the input for each different player.
/// </summary>

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player m_player;
    public string m_horizontal;
    public string m_vertical;
    public string m_jump;

    private void Start()
    {
        m_player = GetComponent<Player>();
    }

    private void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw(m_horizontal), Input.GetAxisRaw(m_vertical));
        m_player.SetDirectionalInput(directionalInput);

        if (Input.GetButtonDown(m_jump))
        {
            m_player.OnJumpInputDown();
        }

        if (Input.GetButtonUp(m_jump))
        {
            m_player.OnJumpInputUp();
        }
    }
}
