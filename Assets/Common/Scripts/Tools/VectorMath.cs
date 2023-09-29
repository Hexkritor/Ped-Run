using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorMath 
{
    public static Vector3 RayToPlanePoint(Ray ray, Vector3 vector)
    {
        var plane = new Plane(vector.normalized, 0);
        float rayDot = Vector3.Dot(ray.direction, plane.normal);
        float num = -Vector3.Dot(ray.origin, plane.normal) - plane.distance;
        float distance = 0;

        if (Mathf.Approximately(rayDot, 0.0f))
        {
            distance = 0;
        }
        else
        {
            distance = num / rayDot;
        }

        return ray.origin + distance * ray.direction;
    }
}
