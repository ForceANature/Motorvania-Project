using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joints_Handler : MonoBehaviour
{
    private Joint[] _joints;
    private readonly List<Vector3> _connectedAnchors = new();
    private readonly List<Vector3> _anchors = new();

    private void Start()
    {
        // Grab references to joints anchors, to update them during the game.
        _joints = transform.GetComponentsInChildren<Joint>();
        for (int i = 0; i < _joints.Length; i++)
        {
            Joint curJoint = _joints[i];
            _connectedAnchors.Add(curJoint.connectedAnchor);
            _anchors.Add(curJoint.anchor);
        }
    }

    public void UpdateJoints()
    {
        // Update joints by resetting them to their original values
        for (int i = 0; i < _joints.Length; i++)
        {
            Joint joint = _joints[i];
            joint.connectedAnchor = _connectedAnchors[i];
            joint.anchor = _anchors[i];
        }

        Debug.Log("Joints Fixed!");
    }
}
