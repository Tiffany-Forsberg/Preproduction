using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [Tooltip("The amount that the average ground normal has to face upwards to count as ground")]
    [SerializeField, Range(0f, 1f)] private float groundNormalTolerance;

    private HashSet<GameObject> groundObjects;
    
    public bool OnGround => groundObjects.Count > 0;
    
    private void Awake()
    {
        groundObjects = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 averageNormal = Vector2.zero;

        foreach (ContactPoint2D point in other.contacts)
        {
            averageNormal += point.normal;
        }

        if (other.contacts.Length > 0)
        {
            averageNormal /= other.contacts.Length;
        }

        averageNormal.Normalize();

        if (averageNormal.y >= groundNormalTolerance)
        {
            groundObjects.Add(other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (groundObjects.Contains(other.gameObject))
        {
            groundObjects.Remove(other.gameObject);
        }
    }
}
