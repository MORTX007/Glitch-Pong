using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIndicatorManager : MonoBehaviour
{
    public Transform ball;

    private void FixedUpdate()
    {
        Vector2 targetPos = new Vector2(ball.position.x, transform.position.y);
        transform.position = targetPos;
    }
}
