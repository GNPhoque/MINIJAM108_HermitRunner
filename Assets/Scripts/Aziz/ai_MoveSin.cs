using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_MoveSin : MonoBehaviour
{
    Rigidbody2D rb2d;
    float initialPos;
    float amplitude;
    float frequency;
    bool  isInverted;
    Transform _parentTransform;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        initialPos = transform.position.y;
        _parentTransform = transform.parent.transform;

    }

    private void Start()
    {
        amplitude = _parentTransform.GetComponent<ai_MoveSin_parent>().Amplitude;
        frequency = _parentTransform.GetComponent<ai_MoveSin_parent>().Frequency;
        isInverted = _parentTransform.GetComponent<ai_MoveSin_parent>().IsInverted;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float sin = Mathf.Sin(pos.x*frequency) * amplitude;
        if(isInverted)
        {
            sin *= -1;
        }
        pos.y = initialPos + sin;

        transform.position = pos;
    }
}
