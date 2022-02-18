using UnityEngine;
using System;

public class WallCheck : MonoBehaviour
{
    public Moving moving;
    public Transform wct;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.position.x < Math.Round(moving.playerT.position.x))
        {
            moving.wallDirection = -1;
        }
        if (collision.transform.position.x > Math.Round(moving.playerT.position.x))
        {
            moving.wallDirection = 1;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        moving.wallDirection = 0;
    }
}
