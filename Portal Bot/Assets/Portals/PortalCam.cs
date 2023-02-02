using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PortalCam : MonoBehaviour
{
    public PortalCam _other;
    public Camera _portalView;
    [SerializeField] private CharController _character;
    [SerializeField] private Hero _heroPos;
    private void Start()
    {
        _other._portalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponentInChildren<MeshRenderer>().sharedMaterial.mainTexture = _other._portalView.targetTexture;
    }


    void Update()
    {
        Vector3 chPos = _heroPos.GetPosition();
        //Vector3 chPos = _character.GetRB().transform.position;
        Vector3 lookerPosition = _other.transform.worldToLocalMatrix.MultiplyPoint3x4(chPos);
        _portalView.transform.localPosition =-lookerPosition;

        _portalView.nearClipPlane = lookerPosition.magnitude;
    }
}
