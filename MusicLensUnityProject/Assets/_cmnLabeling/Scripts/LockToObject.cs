using UnityEngine;
using System.Collections;

/// <summary>
/// A method which will latch this object to the center of its target every frame.
/// </summary>
[ExecuteInEditMode]
public class LockToObject : MonoBehaviour {

    //Where to calculate the targets center at.
    public enum LockType
    {
        Center, BoundingBox
    }

    public LockType lockType;

    public Transform target;

    public Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!target)
            return;
        switch (lockType)
        {
            case LockType.Center:
                LockOnCenter();
                break;
            case LockType.BoundingBox:
                LockOnBoundingBox();
                break;
            default:
                LockOnCenter();
                break;
        }
        transform.position += offset;
	}

    /// <summary>
    /// Simply moves this object to the center of the 
    /// </summary>
    void LockOnCenter()
    {
        transform.position = target.position;
    }

    void LockOnBoundingBox()
    {
        MeshFilter mf = target.GetComponent<MeshFilter>();
        if (mf)
        {
            Bounds b = mf.mesh.bounds;
            transform.position = b.center;
        }
    }
}
