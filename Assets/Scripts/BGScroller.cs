using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;
    private Vector3 startPosition;
    private float planeSize;

    void Start()
    {
        startPosition = transform.position;
        planeSize = transform.localScale.y;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, planeSize);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
