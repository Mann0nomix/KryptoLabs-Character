using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotator : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        Vector3 relativePos = ((target.position + new Vector3(0, 2.0f, 0f)) - transform.position);
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
        transform.Translate(0, 0, 10 * Time.deltaTime);
    }
}