using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossLineCircle : MonoBehaviour {

    public Line line;
    public Circle circle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnDrawGizmos()
    {
        if (line == null || circle == null)
        {
            return;
        }

        // 绘制交点
        var cross = Utils.Intersect2D(line.p0.position, line.p1.position, circle.transform.position, circle.Radius);
        foreach (var it in cross)
        {
            Gizmos.DrawSphere(it, 0.1f);
        }
        var proj = Utils.Project2D(line.p0.position, line.p1.position, circle.transform.position);
        Gizmos.DrawSphere(proj, 0.1f);
    }
}
