using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Teleporter _otherPortal;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        float yPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).y;

        if (yPos < 0)
        {
            Teleport(other.transform);
        }
    }

    private void Teleport(Transform obj)
    {
        Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
        localPos = new Vector3(localPos.x, localPos.y, localPos.z);
        obj.position = _otherPortal.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.layer = 7;
    }
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.layer = 6;
    }
}
