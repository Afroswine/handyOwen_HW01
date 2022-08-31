using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureIncrease : CollectibleBase
{
    [SerializeField] int _treasureAmount = 1;

    protected override void Collect(Player player)
    {
        player.IncreaseTreasure(_treasureAmount);
    }

    protected override void Movement(Rigidbody rb)
    {
        Quaternion turnOffset = Quaternion.Euler
            (MovementSpeed, MovementSpeed, MovementSpeed);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
}
