using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAutoZoom : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float startFOV = 60f;      // Starting zoom level (normal view)
    public float targetFOV = 30f;     // Final zoom level (zoomed-in)
    public float zoomDuration = 2f;   // How long it takes to zoom in (seconds)

    private float timer = 0f;

    void Start()
    {
        if (vcam == null)
            vcam = GetComponent<CinemachineVirtualCamera>();

        // Set initial zoom
        vcam.m_Lens.FieldOfView = startFOV;
    }

    void Update()
    {
        if (timer < zoomDuration)
        {
            timer += Time.deltaTime;
            float t = timer / zoomDuration;

            // Smoothly interpolate from startFOV to targetFOV
            vcam.m_Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
        }
    }
}
