using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotator : MonoBehaviour
{
    public GameObject ob;
    private Transform target;

    private void Start()
    {
        target = ob.transform;
    }
    void Update()
    {
        Vector3 relativePos = ((target.position + new Vector3(0, 5.0f, 0f)) - transform.position);
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
        transform.Translate(0, 0, 10 * Time.deltaTime);
    }
}