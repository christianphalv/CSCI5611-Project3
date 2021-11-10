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
        Array.Sort(_joints, new Comparison<Joint>((x, y) => x._orderNumber.CompareTo(y._orderNumber)));
        _limbGoal = GetComponentInChildren<LimbGoal>();
    }

    void Update() {
        IKSolver();
    }

    private void OnDrawGizmos() {
        for (int i = 0; i < _joints.Length; i++) {
            //Gizmos.DrawSphere(_joints[i].getEndPoint(), 0.2f);
        }
    }

    private void IKSolver() {


        for (int i = _joints.Length - 1; i >= 0; i--) {

            // Initialize goal and effector vectors
            Vector3 start = _joints[i].transform.position;
            Vector3 startToGoal = (_limbGoal.transform.position - start).normalized;
            Vector3 startToEndEffector = (_joints.Last().getEndPoint() - start).normalized;

            // Rotate joint toward goal
            _joints[i].transform.rotation = Quaternion.LookRotation(startToGoal, startToEndEffector);

            fk();
        }
    }

    private void fk() {

        for (int i = 0; i < _joints.Length - 1; i++) {
            _joints[i + 1].transform.position = _joints[i].getEndPoint();
        }
    }




}
