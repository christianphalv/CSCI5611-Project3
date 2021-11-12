using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Limb : MonoBehaviour {

    private Joint[] _joints;
    private LimbGoal _limbGoal;
    private float _speed;

    private float angleLimit = 135f;


    void Start() {

        // Initialize limb components
        _joints = GetComponentsInChildren<Joint>();
        _limbGoal = GetComponentInChildren<LimbGoal>();
        _speed = 10f;

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
            //Quaternion fromToRotation = Quaternion.RotateTowards(startToEndEffector, startToGoal, angleLimit * Time.deltaTime);


            Quaternion newRotation = fromToRotation * _joints[i].transform.rotation;

            Quaternion angle_limited_rotation = Quaternion.RotateTowards(_joints[i].transform.rotation, newRotation, angleLimit * Time.deltaTime);
        

            // Slow rotation (Works kinda)
            //newRotation = Quaternion.LerpUnclamped(_joints[i].transform.rotation, newRotation, _speed * Time.deltaTime);

            // Limit rotation angle
            // if (i > 0) {
            //     limitedRotationAngle(newRotation, _joints[i - 1].transform.rotation, 90f);
            // }


            // Update joint rotation
           // _joints[i].transform.rotation = newRotation;
           _joints[i].transform.rotation = angle_limited_rotation;

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
            Debug.Log(Quaternion.Angle(rotation, connectedJoint)); // Not finished
            //set rotation to be max angle
        }

        return rotation;
    }
}
