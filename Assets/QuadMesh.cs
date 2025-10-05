using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class QuadMesh : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // Define vertices
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f, 0), // Bottom-left
            new Vector3(0.5f, -0.5f, 0),  // Bottom-right
            new Vector3(-0.5f, 0.5f, 0),  // Top-left
            new Vector3(0.5f, 0.5f, 0)    // Top-right
        };

        // Define triangles (two triangles make a quad)
        int[] triangles = new int[]
        {
            0, 2, 1, // First triangle
            1, 2, 3  // Second triangle
        };

        // Define UVs (for texture mapping)
        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0, 0), // Bottom-left
            new Vector2(1, 0), // Bottom-right
            new Vector2(0, 1), // Top-left
            new Vector2(1, 1)  // Top-right
        };

        // Assign data to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        // Recalculate normals for lighting
        mesh.RecalculateNormals();
    }
}