using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossCircleRectangle : MonoBehaviour {

    public Rectangle R;
    public Circle C;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnDrawGizmos()
    {
        if (C == null || R == null)
        {
            return;
        }

        var p1 = new Vector3(-R.width / 2, -R.height / 2, 0);
        var p2 = p1 + new Vector3(R.width, 0, 0);
        var p3 = p2 + new Vector3(0, R.height, 0);
        var p4 = p1 + new Vector3(0, R.height, 0);

        p1 = R.transform.TransformPoint(p1);
        p2 = R.transform.TransformPoint(p2);
        p3 = R.transform.TransformPoint(p3);
        p4 = R.transform.TransformPoint(p4);

        var cross1 = Utils.SegmentCircle(p1, p2, C.transform.position, C.Radius);
        var cross2 = Utils.SegmentCircle(p2, p3, C.transform.position, C.Radius);
        var cross3 = Utils.SegmentCircle(p3, p4, C.transform.position, C.Radius);
        var cross4 = Utils.SegmentCircle(p4, p1, C.transform.position, C.Radius);

        List<Vector3> cross = new List<Vector3>();
        cross.AddRange(cross1);
        cross.AddRange(cross2);
        cross.AddRange(cross3);
        cross.AddRange(cross4);

        var old = Gizmos.color;
        Gizmos.color = Color.red;
        foreach (var it in cross)
        {
            Gizmos.DrawSphere(it, 0.1f);
        }
        Gizmos.color = old;

    }
}
