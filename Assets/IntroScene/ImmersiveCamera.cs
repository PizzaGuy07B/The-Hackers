using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ImmersiveCamera : MonoBehaviour
{
    [Header("Cinemachine Setup")]
    public CinemachineVirtualCamera vcam;
    public Transform planetTarget;

    [Header("Zoom Settings")]
    public float zoomSpeed = 2f;               // Speed of zoom
    public float targetFOV = 20f;              // Smaller FOV = more zoomed in
    public float delayBeforeSceneChange = 0.5f;

    [Header("References")]
    public SceneChange sceneChanger;

    private bool zooming = false;
    private bool zoomCompleted = false;

    void Update()
    {
        if (sceneChanger != null && sceneChanger.canZoom && !zooming && !zoomCompleted)
        {
            zooming = true;
        }

        if (zooming)
        {
            ZoomIn();
        }
    }

    void ZoomIn()
    {
        var lens = vcam.m_Lens;

        // Move FOV smoothly *toward* the target value
        lens.FieldOfView = Mathf.MoveTowards(lens.FieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
        vcam.m_Lens = lens;

        // Ensure camera is aimed at the planet
        if (planetTarget != null)
            vcam.LookAt = planetTarget;

        // When close enough, trigger scene change
        if (Mathf.Abs(lens.FieldOfView - targetFOV) < 0.1f)
        {
            zooming = false;
            zoomCompleted = true;
            Debug.Log("✅ Zoom complete, changing scene...");
            StartCoroutine(ChangeSceneAfterDelay());
        }
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSceneChange);
        sceneChanger.ChangeScene();
    }
}