using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/24/17
/// Purpose: A ghost controller that moves the ghost through everything.
/// </summary>
public class GhostController2D : MonoBehaviour
{
    #region GhostController2D Methods
    public void Move(Vector2 moveAmount)
    {
        transform.Translate(moveAmount);
    }
    #endregion
}
