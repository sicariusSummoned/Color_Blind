/// <summary>
/// Author: Dante Nardo
/// Last Modified: 9/10/2017
/// Purpose: Holds the variables necessary to determine and store the player's current state at any time.
/// </summary>

public class PlayerState
{
    public bool IsCollidingRight { get; set; }
    public bool IsCollidingLeft { get; set; }
    public bool IsCollidingAbove { get; set; }
    public bool IsCollidingBelow { get; set; }
    public bool HasCollisions { get { return IsCollidingRight || IsCollidingLeft || IsCollidingAbove || IsCollidingBelow; } }
    
    public bool IsGrounded { get { return IsCollidingBelow; } }
    public bool IsFalling { get; set; }
    public bool WasGroundedLastFrame { get; set; }
    public bool WasTouchingTheCeilingLastFrame { get; set; }
    public bool JustGotGrounded { get; set; }
    
    public virtual void Reset()
    {
        IsCollidingLeft = false;
        IsCollidingRight = false;
        IsCollidingAbove = false;
        IsCollidingBelow = false;
        JustGotGrounded = false;
        IsFalling = true;
    }
}
