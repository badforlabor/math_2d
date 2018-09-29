using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossLineLine : MonoBehaviour {

    public Line line;
    public Line line2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    void OnDrawGizmos()
    {
        if (line == null || line2 == null)
        {
            return;
        }

        // 绘制交点
        var cross = Utils.Intersect2D(line.p0.position, line.p1.position, line2.p0.position, line2.p1.position);
        foreach (var it in cross)
        {
            Gizmos.DrawSphere(it, 0.1f);
        }
    }
}
