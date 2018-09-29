using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // x在直线(p1,p2)上的投影
    public static Vector3 Project2D(Vector3 p1, Vector3 p2, Vector3 x)
    {
        var cp1 = x - p1;
        var p2p1 = p2 - p1;
        var p2p1Normal = p2p1.normalized;
        var z = p1 + Vector3.Dot(p2p1.normalized, cp1.normalized) * cp1.magnitude * p2p1Normal;
        return z;
    }

    // 世界坐标
    public static List<Vector3> Intersect2D(Vector3 p1, Vector3 p2, Vector3 circleCenter, float radius)
    {
        var ret = new List<Vector3>();
        /*
         * 圆心到直线的投影点E，以及距离d，比较d和radius
         *      d < radius：相交
         *          左右两点与E的距离是：dt = sqrt(r*r-d*d)
         *          所有左右两点是：(B-A).normal * ((E-A).mag +/- dt)
         *      d == radius：相切
         *          E点，就是交点
         *      d > radius: 相离
         *          无交点
         */
        var p1c = circleCenter - p1;
        var p1p2 = p2 - p1;
        var p1p2Normal = p1p2.normalized;
        var E = p1 + Vector3.Dot(p1p2.normalized, p1c.normalized) * p1c.magnitude * p1p2Normal;
        var ce = E - circleCenter;
        var ceDist = ce.magnitude;
        if (ceDist < radius)
        {
            var p1eDist = (E - p1).magnitude;
            var dt = Mathf.Sqrt(radius * radius - ceDist * ceDist);
            ret.Add((p1eDist - dt) * p1p2Normal + p1);
            ret.Add((p1eDist + dt) * p1p2Normal + p1);
        }
        else if (ceDist == radius)
        {
            ret.Add(E);
        }
        else
        {

        }

        return ret;
    }
}
