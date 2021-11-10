using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Limb : MonoBehaviour {

    private LimbSegment[] _limbSegments;
    private LimbEnd _limbEnd;


    void Start() {

        // Initialize limb components
        _limbSegments = GetComponentsInChildren<LimbSegment>();
        //Array.Sort(_limbSegments);
        Array.Sort(_limbSegments, new Comparison<LimbSegment>((x, y) => x.orderNumber.CompareTo(y.orderNumber)));
        _limbEnd = GetComponentInChildren<LimbEnd>();
    }

    void Update() {
        IKSolver();
    }

    private void IKSolver() {

        Vector3 startToGoal = _limbEnd.transform.position - _limbSegments.Last().transform.position;
        Vector3 startToEndEffector = 

        for (int i = _limbSegments.Length - 1; i >= 0; i--) { 
            
        }
    }



}
