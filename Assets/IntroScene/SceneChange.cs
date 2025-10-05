using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;  // For the new Input System
using UnityEngine.XR;           // For XR controller support

public class SceneChange : MonoBehaviour
{
    [Header("Scene Change Settings")]
    public string nextSceneName = "NextScene"; // Scene to load
    public float waitTime = 5f;                // Time (in seconds) before input is allowed
    [HideInInspector] public bool canZoom = false; // True when player triggers zoom

    private float timer;
    private bool canChange = false;

    void Start()
    {
        timer = waitTime;
    }

    void Update()
    {
        // Countdown timer before player can act
        if (!canChange)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                canChange = true;
                Debug.Log("Player can now interact.");
            }
            return;
        }

        // --- Keyboard input ---
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            canZoom = true;
            Debug.Log("Input detected — canZoom = true");
        }

        // --- VR controller input ---
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(devices);

        foreach (var device in devices)
        {
            bool primaryButtonPressed = false;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButtonPressed) && primaryButtonPressed)
            {
                canZoom = true;
                Debug.Log("Input detected — canZoom = true");
            }
        }
    }

    // You can still keep this public for ImmersiveCamera to call
    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}