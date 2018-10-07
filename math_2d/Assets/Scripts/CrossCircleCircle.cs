using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossCircleCircle : MonoBehaviour
{

    public Circle c1;
    public Circle c2;


    void OnDrawGizmos()
    {
        if (c1 == null || c2 == null)
        {
            return;
        }


        var cross = Utils.Intersect2D(c1.transform.position, c1.Radius, c2.transform.position, c2.Radius);
        foreach (var it in cross)
        {
            Gizmos.DrawSphere(it, 0.1f);
        }
    }

}
