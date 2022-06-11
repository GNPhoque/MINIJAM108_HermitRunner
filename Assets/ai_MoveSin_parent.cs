using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_MoveSin_parent : MonoBehaviour
{

    public float Amplitude { get; set; }
    public float Frequency { get; set; }

    [SerializeField] private bool _isInverted;
    public bool IsInverted { get; set; }

    void Awake()
    {
        Amplitude = Random.Range(1, 4);
        Frequency = Random.Range(1, 3);
    }

    void Update()
    {
        IsInverted = _isInverted;
    }
}
