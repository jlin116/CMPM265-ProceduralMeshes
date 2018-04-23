using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCreator))]
public class Cube : MonoBehaviour
{
    MeshFilter meshFilter;
    MeshCreator meshCreator;
    private bool direction = true;
    private float zValue = 0;

    [SerializeField]
    protected bool isAnimatingUsingPerlinNoise = true;

    [SerializeField]
    private float incrementDelta = 0.05f;

    protected void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCreator = GetComponent<MeshCreator>();
    }

    protected void Update()
    {
        meshCreator.Clear();

        Vector3 center = transform.localPosition;
        CreateCube(center, Time.deltaTime);

        meshFilter.mesh = meshCreator.CreateMesh();
    }

    protected float GetZValue()
    {
        if (isAnimatingUsingPerlinNoise)
        {
            zValue += (direction) ? (incrementDelta) : (-incrementDelta);

        return zValue;
        }
        else
        {
            return 0;
        }
    }

    protected void CreateCube(Vector3 center, float time)
    {
        Vector3 cubeSize = Vector3.one * 0.5f;

        float noise = GenerateNoise();

        // top of the cube
        // t0 is top left point
        Vector3 t0 = new Vector3(center.x + cubeSize.x, center.y + cubeSize.y + noise, center.z - cubeSize.z);
        Vector3 t1 = new Vector3(center.x - cubeSize.x, center.y + cubeSize.y + noise, center.z - cubeSize.z);
        Vector3 t2 = new Vector3(center.x - cubeSize.x, center.y + cubeSize.y + noise, center.z + cubeSize.z);
        Vector3 t3 = new Vector3(center.x + cubeSize.x, center.y + cubeSize.y + noise, center.z + cubeSize.z);

        // bottom of the cube
        Vector3 b0 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y, center.z - cubeSize.z);
        Vector3 b1 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y, center.z - cubeSize.z);
        Vector3 b2 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y, center.z + cubeSize.z);
        Vector3 b3 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y, center.z + cubeSize.z);

        // Top square
        meshCreator.BuildTriangle(t0, t1, t2);
        meshCreator.BuildTriangle(t0, t2, t3);

        // Bottom square
        meshCreator.BuildTriangle(b2, b1, b0);
        meshCreator.BuildTriangle(b3, b2, b0);

        // Back square
        meshCreator.BuildTriangle(b0, t1, t0);
        meshCreator.BuildTriangle(b0, b1, t1);

        meshCreator.BuildTriangle(b1, t2, t1);
        meshCreator.BuildTriangle(b1, b2, t2);

        meshCreator.BuildTriangle(b2, t3, t2);
        meshCreator.BuildTriangle(b2, b3, t3);

        meshCreator.BuildTriangle(b3, t0, t3);
        meshCreator.BuildTriangle(b3, b0, t0);
    }

    protected virtual float GenerateNoise()
    {
        return 1 + 0.9f * Perlin.Noise(transform.localPosition.x, transform.localPosition.z, GetZValue());
    }
}
