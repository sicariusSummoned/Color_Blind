using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 9/10/2017
/// Purpose: Holds the modifiable input variables that will change how the player functions.
/// </summary>

public class PlayerParameters : MonoBehaviour
{
    public float Gravity = -30f;
    public float FallMultiplier = 1f;
    public float AscentMultiplier = 1f;
    
    public Vector2 MaxVelocity = new Vector2(100f, 100f);
    public float SpeedAccelerationOnGround = 20f;
    public float SpeedAccelerationInAir = 5f;
    public float SpeedFactor = 1f;

    public int HorizontalRaycasts = 8;
    public int VerticalRaycasts = 8;

    public bool Player1 = true;
    public bool Player2 = false;
    public bool Player3 = false;
    public bool Player4 = false;
}

//using UnityEngine;

///// <summary>
///// Author: Dante Nardo
///// Last Modified: 9/11/2017
///// Purpose: Adds forces to the player based off of input.
///// </summary>

//public class PlayerInput : MonoBehaviour
//{
//    #region PlayerInput Members
//    public PlayerController Controller;
//    public PlayerParameters Parameters;
//    #endregion

//    #region PlayerInput Methods
//    void Update()
//    {
//        if (Parameters.Player1)
//        {
//            float moveHorizontal = Input.GetAxis("Player1_Horizontal");
//            float moveVertical = Input.GetAxis("Player1_Vertical");

//            Vector2 force = new Vector2(moveHorizontal, moveVertical) * Parameters.SpeedFactor;
//            Controller.AddForce(force);
//        }
//        else if (Parameters.Player2)
//        {
//            float moveHorizontal = Input.GetAxis("Player2_Horizontal");
//            float moveVertical = Input.GetAxis("Player2_Vertical");

//            Vector2 force = new Vector3(moveHorizontal, moveVertical) * Parameters.SpeedFactor;
//            Controller.AddForce(force);
//        }
//    }
//    #endregion
//}
