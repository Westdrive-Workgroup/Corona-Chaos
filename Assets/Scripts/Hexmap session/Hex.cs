using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex
{
    public readonly int Q; // Column
    public readonly int R; // Row
    public readonly int S;
    public readonly float Radius;
    private readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
    private readonly bool EDGEUP;

    public Hex(int q, int r, float radius = 1f, bool edgeUp = false)
    {
        Q = q;
        R = r;
        S = -(Q + R);
        Radius = radius;
        EDGEUP = edgeUp;
    }

    public Vector3 GetPosition()
    {
        float height = 2 * Radius;
        float width = WIDTH_MULTIPLIER * height;
        float horizontalPosition = width;
        float verticalPosition = height * 0.75f;
        if (!EDGEUP)
        {
            return new Vector3(
                horizontalPosition * (this.Q + this.R / 2f),
                0,
                verticalPosition * this.R
            );
        }
        else
        {
            return new Vector3(
                verticalPosition * this.R,
                0,
                horizontalPosition  * (this.Q + (this.R / 2f ))
            );
        }
    }
}
