using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refractive : MonoBehaviour
{
    public float refractionIndex = 1.5f; // 折射率，可以根据需要调整

    public Vector2 CalculateRefraction(Vector2 incident, Vector2 normal)
    {
        // 简单的折射实现，实际情况可能需要根据斯涅尔定律调整
        return Vector2.Reflect(incident, normal) * refractionIndex;
    }
}
