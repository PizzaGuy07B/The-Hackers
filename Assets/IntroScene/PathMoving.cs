using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class PathMoving : MonoBehaviour
{
    [SerializeField] SplineContainer path;
    [SerializeField] float speed = 0.12f;
    float distance = 0f;
    bool ready = false;

    IEnumerator Start()
    {
        // Wait for XR initialization & scene load
        yield return new WaitForSeconds(1f);

        // Try to find spline if not assigned
        if (path == null)
        {
            path = FindObjectOfType<SplineContainer>();
            if (path == null)
            {
                Debug.LogError("No SplineContainer found in scene!");
                yield break;
            }
        }

        ready = true;
        Debug.Log("PathMoving is ready, starting movement.");
    }

    void Update()
    {
        if (!ready || path == null)
            return;

        distance += speed * Time.deltaTime;

        float length = path.Spline.GetLength();
        float normalized = Mathf.Repeat(distance, length) / length;

        Vector3 pos = path.EvaluatePosition(normalized);
        Vector3 fwd = path.EvaluateTangent(normalized);

        transform.position = pos;
        Vector3 flatFwd = Vector3.ProjectOnPlane(fwd, Vector3.up).normalized;
        transform.rotation = Quaternion.LookRotation(flatFwd, Vector3.up);
    }
}