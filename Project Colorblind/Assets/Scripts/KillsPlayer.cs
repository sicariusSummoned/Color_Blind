using UnityEngine;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 9/13/2017
/// Purpose: Kills the player when they touch it.
/// </summary>
public class KillsPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
