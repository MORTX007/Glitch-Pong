using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Collider2D playerBoundary;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    private void Update()
    {
        
    }
}
