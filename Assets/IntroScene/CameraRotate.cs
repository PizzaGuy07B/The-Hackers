using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotate : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float rotationSpeed = 30f;

    private CinemachineOrbitalTransposer orbital;

    void Start()
    {
        if (vcam == null)
            vcam = GetComponent<CinemachineVirtualCamera>();

        orbital = vcam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    void Update()
    {
        if (orbital != null)
        {
            // Continuously rotate around the target
            orbital.m_Heading.m_Bias += rotationSpeed * Time.deltaTime;
        }
    }
}
