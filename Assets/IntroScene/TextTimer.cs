using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;        // gives us any XR button
using TMPro;

public class TextTimer : MonoBehaviour
{
    [SerializeField] float appearAfter = 15f;
    CanvasGroup cg;
    bool shown = false;
    bool gone  = false;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();

        cg.alpha = 0;                // 1. start hidden
    }

    void Update()
    {
        if (gone)  return;
        if (!shown)
        {
            appearAfter -= Time.unscaledDeltaTime;
            if (appearAfter <= 0)     // 2. reveal after 15 s
            {
                cg.alpha = 1;
                shown = true;
            }
            return;
        }

        // 3. wait for ANY input
        bool kb = Input.anyKeyDown;
        bool xr = false;
        foreach (XRNode hand in new[]{XRNode.LeftHand, XRNode.RightHand})
        {
            InputDevice dev = InputDevices.GetDeviceAtXRNode(hand);
            if (dev.isValid)
            {
                if (dev.TryGetFeatureValue(CommonUsages.triggerButton,  out bool trg) && trg) xr = true;
                if (dev.TryGetFeatureValue(CommonUsages.primaryButton,  out bool pri) && pri) xr = true;
                if (dev.TryGetFeatureValue(CommonUsages.secondaryButton,out bool sec) && sec) xr = true;
                if (dev.TryGetFeatureValue(CommonUsages.gripButton,      out bool grp) && grp) xr = true;
            }
        }

        if (kb || xr) StartCoroutine(FadeOut());
    }

    System.Collections.IEnumerator FadeOut()
    {
        gone = true;
        while (cg.alpha > 0)
        {
            cg.alpha -= Time.unscaledDeltaTime * 4f;   // ¼-s fade
            yield return null;
        }
        gameObject.SetActive(false);   // or Destroy(gameObject);
    }
}
