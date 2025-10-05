using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;   // Package Manager ? Splines

public class PathMoving : MonoBehaviour
{
    [SerializeField] SplineContainer path;   // drag the Path here
    [SerializeField] float speed = 0.12f;   // 0.12 = ~7 m/min walking pace
    float distance = 0f;

    void Update()
    {
        distance += speed * Time.deltaTime;
        float clamped = Mathf.Repeat(distance, path.Spline.GetLength());
        Vector3 pos = path.EvaluatePosition(clamped);
        Vector3 fwd = path.EvaluateTangent(clamped);

        // Lock position to the spline
        transform.position = pos;

        // Allow ONLY horizontal look-around: keep the path’s forward
        Vector3 flatFwd = Vector3.ProjectOnPlane(fwd, Vector3.up).normalized;
        transform.rotation = Quaternion.LookRotation(flatFwd, Vector3.up);
    }
}
