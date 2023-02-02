using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGraph : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;

    private void Update()
    {
        _curve.AddKey(Time.time, transform.position.x);
    }
}
