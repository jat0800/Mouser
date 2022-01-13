using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] Vector3 movePosition;
    [SerializeField] [Range(0,1)] float moveFactor;
    [SerializeField] float Period = 2f;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        if (Period <= Mathf.Epsilon)
            {
                return;
            }
        
        float cycle = Time.time / Period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycle * tau);

        moveFactor = (rawSinWave + 1f) / 2f; // ทำให้เป็น 0 to 2 

        Vector3 offset = movePosition * moveFactor;
        transform.position = startPosition + offset;
    }
}
