using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Joint : MonoBehaviour {

    public int _orderNumber;
    private float _length;


    void Start() {
        _length = 1.2f;
    }

    void Update() {
        
    }

    public Vector3 getEndPoint() {
        return transform.position + ((transform.rotation * Vector3.forward).normalized * _length);
    }

}
