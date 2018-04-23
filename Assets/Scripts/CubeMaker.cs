using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaker : MonoBehaviour
{
    [SerializeField]
    private GameObject cubeObject;

    [SerializeField]
    private Vector3 size = Vector3.one;

    private void Start()
    {
        for (int row = 0; row < 20; ++row)
        {
            for (int col = 0; col < 20; ++col)
            {
                Instantiate(cubeObject, new Vector3(col * size.x * 1.2f, 0, row * size.z * 1.2f), Quaternion.identity, transform);
            }
        }
    }
}
