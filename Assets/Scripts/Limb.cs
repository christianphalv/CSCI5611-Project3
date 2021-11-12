using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Limb : MonoBehaviour {

    private Joint[] _joints;
    private LimbGoal _limbGoal;
    private float _speed;
    private float _angleLimit;


    void Start() {

        // Initialize limb components
        _joints = GetComponentsInChildren<Joint>();
        _limbGoal = GetComponentInChildren<LimbGoal>();
        _speed = 100f;
        _angleLimit = 90f;

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

            // Limit rotation speed
            newRotation = Quaternion.RotateTowards(_joints[i].transform.rotation, newRotation, _speed * Time.deltaTime);

            // Limit rotation angle
            if (i > 0) {
                newRotation = limitedRotationAngle(newRotation, _joints[i - 1].transform.rotation, _angleLimit);
            } else if (i == 0) { 
                newRotation = limitedRotationAngle(newRotation, this.transform.rotation, _angleLimit);
            }

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

    private Quaternion limitedRotationAngle(Quaternion rotation, Quaternion connectedJoint, float maxAngle) {

        // Corrects rotation if greater than maximum angle
        if (Quaternion.Angle(rotation, connectedJoint) > maxAngle) {
            return Quaternion.RotateTowards(connectedJoint, rotation, maxAngle);
        }

        return rotation;
    }
}
