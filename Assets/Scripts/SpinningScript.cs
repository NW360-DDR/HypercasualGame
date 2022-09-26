using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningScript : MonoBehaviour
{

    // This script exists for spinning objects to spin, mainly the SpinnyDiamond and LongSpinny hazards.

    float SpinSpeed = 1.0f;
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * SpinSpeed);
    }
}
