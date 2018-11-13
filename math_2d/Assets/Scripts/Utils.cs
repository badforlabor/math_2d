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

            if (Vector3.Dot((E - p1), p1p2) < 0)
            {
                p1eDist *= -1;
            }

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

    public static void GetLineExpr(Vector3 p0, Vector3 p1, out float a, out float b, out float c)
    {
        a = p0.y - p1.y;
        b = p1.x - p0.x;
        c = p0.x * p1.y - p1.x * p0.y;
    }

    // 世界坐标。计算两条线的交点
    public static List<Vector3> Intersect2D(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        var ret = new List<Vector3>();
        /*
         * 直线可以表示成：a*x + b*y + c = 0
         *      假如知道直线上两个点p1,p2，那么有：
         *          a * p1.x + b*p1.y + c = 0
         *          a * p2.x + b* p2.y + c = 0
         *      可以得到其中一个解：a = p1.y-p2.y; b = p2.x - p1.x; c = p1.x * p2.y - p2.x * p1.y
         *      同理可以求出另一个直线的a,b,c。
         *      
         *      于是规定，p1p2直线为a0,b0,c0, p3p4直线为a1,b1,c1
         *      
         *      假设存在交点，且是p0(x,y)，那么一定有：
         *          a0 * x + b0 * y + c0 = 0
         *          a1 * x + b1 * y + c1 = 0
         *      
         *      于是就计算出：
         *          x = (b0*c1 – b1*c0)/(a0*b1 – a1*b0)
         *          y = (a1*c0 – a0*c1)/(a0*b1 – a1*b0)
         *          
         *      于是，可以简写为：
         *          D = a0*b1 – a1*b0
         *          x = (b0*c1 – b1*c0)/D
         *          y = (a1*c0 – a0*c1)/D
         *          
         */

        float a0, b0, c0;
        GetLineExpr(p1, p2, out a0, out b0, out c0);

        float a1, b1, c1;
        GetLineExpr(p3, p4, out a1, out b1, out c1);

        var D = a0 * b1 - a1 * b0;
        if(D == 0)
        {
            return ret;
        }
        var x = (b0 * c1 - b1 * c0) / D;
        var y = (a1 * c0 - a0 * c1) / D;

        ret.Add(new Vector3(x, y, 0));

        return ret;
    }

    // 世界坐标
    public static bool IsIntersect(Vector3 circleCenter, float radius, Vector3 circleCenter1, float radius1)
    {
        return (circleCenter - circleCenter1).magnitude < (radius + radius1);
    }
    static float Sqr(float a)
    {
        return a * a;
    }
    public static List<Vector3> Intersect2D(Vector3 c0, float r0, Vector3 c1, float r1)
    {
        var ret = new List<Vector3>();

        if (!IsIntersect(c0, r0, c1, r1))
        {
            return ret;
        }
        
        var d = Mathf.Sqrt(Sqr(c1.x - c0.x) + Sqr(c1.y - c0.y));
        var a = (Sqr(r0) - Sqr(r1) + Sqr(d)) / (2 * d);
        var h = Mathf.Sqrt(Sqr(r0) - Sqr(a));

        var x2 = c0.x + a * (c1.x - c0.x) / d;
        var y2 = c0.y + a * (c1.y - c0.y) / d;

        var x3 = x2 - h * (c1.y - c0.y) / d;
        var y3 = y2 + h * (c1.x - c0.x) / d;
        var x4 = x2 + h * (c1.y - c0.y) / d;
        var y4 = y2 - h * (c1.x - c0.x) / d;

        ret.Add(new Vector3(x3, y3, 0));
        ret.Add(new Vector3(x4, y4, 0));

        return ret;
    }
    static float Cross2D(Vector3 a, Vector3 b)
    {
        a.z = 0;
        b.z = 0;
        return Vector3.Cross(a, b).z;
    }
    public static List<Vector3> IntersectSegment(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        var ret = new List<Vector3>();

        var a = p2 - p1;
        var b = p4 - p3;

        var cross = Cross2D(a, b);
        if (cross == 0)
        {
            // 平行的
            return ret;
        }

        var t = Cross2D((p3 - p1), b) / cross;

        if (t < 0 || t > 1)
        {
            // 不平行，但无焦点
            return ret;
        }

        ret.Add(p1 + t * a);

        return ret;
    }
    // 世界坐标
    public static List<Vector3> SegmentCircle(Vector3 p1, Vector3 p2, Vector3 circleCenter, float radius)
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
        var Emag = Vector3.Dot(p1p2.normalized, p1c.normalized) * p1c.magnitude;
        var E = p1 +  p1p2Normal * Emag;
        var ce = E - circleCenter;
        var ceDist = ce.magnitude;
        if (ceDist < radius)
        {
            var p1eDist = (E - p1).magnitude;

            if (Vector3.Dot((E - p1), p1p2) < 0)
            {
                p1eDist *= -1;
            }

            var dt = Mathf.Sqrt(radius * radius - ceDist * ceDist);
            var p1p2mag = p1p2.magnitude;
            var mag = (p1eDist - dt);
            if (mag >= 0 && mag <= p1p2mag)
            {
                ret.Add(mag * p1p2Normal + p1);
            }

            mag = (p1eDist + dt);
            if (mag >= 0 && mag <= p1p2mag)
            {
                ret.Add(mag * p1p2Normal + p1);
            }
        }
        else if (ceDist == radius)
        {
            if (Emag >= 0 && Emag <= 1)
            {
                ret.Add(E);
            }
        }
        else
        {

        }

        return ret;
    }    
}
