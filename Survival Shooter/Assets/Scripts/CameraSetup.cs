using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//using Photon.Pun;

public class CameraSetup : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null;

        var brain = Camera.main.GetComponent<CinemachineBrain>();
        var vcam = brain.ActiveVirtualCamera;
        vcam.Follow = transform;
        vcam.LookAt = transform;
    }
}
