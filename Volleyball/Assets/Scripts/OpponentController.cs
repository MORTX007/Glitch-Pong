using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    public Transform ball;
    public Transform net;

    public Vector2 optimalDefensePos;
    public float speed;

    private void Start()
    {
        transform.position = optimalDefensePos;
    }

    private void FixedUpdate()
    {
        
    }
}
