using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Limb : MonoBehaviour {

    private Joint[] _joints;
    private LimbGoal _limbGoal;


    void Start() {

        // Initialize limb components
        _joints = GetComponentsInChildren<Joint>();
        _limbGoal = GetComponentInChildren<LimbGoal>();

        // Sort joints by order defined in editor
        Array.Sort(_joints, new Comparison<Joint>((x, y) => x._orderNumber.CompareTo(y._orderNumber)));
    }

    void Update() {
        fk();
        IKSolver();
    }

    private void IKSolver() {
        for (int i = _joints.Length - 1; i >= 0; i--) {

            // Initialize goal and effector vectors
            Vector3 start = _joints[i].transform.position;
            Vector3 startToGoal = (_limbGoal.transform.position - start).normalized;
            Vector3 startToEndEffector = (_joints.Last().getEndPoint() - start).normalized;

            // Calculate rotation
            Quaternion fromToRotation = Quaternion.FromToRotation(startToEndEffector, startToGoal);
            Quaternion newRotation = fromToRotation * _joints[i].transform.rotation;

            // Update joint rotation
            _joints[i].transform.rotation = newRotation;

            // Update joint positions
            fk();
        }
    }

    private void fk() {

        // Update joint positions to be at the end of the previous segment
        for (int i = 0; i < _joints.Length - 1; i++) {
            _joints[i + 1].transform.position = _joints[i].getEndPoint();
        }
    }
}
