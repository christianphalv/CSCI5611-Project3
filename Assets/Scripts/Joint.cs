using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Joint : MonoBehaviour {

    public int _orderNumber;
    private float _length;

    void Start() {
        _length = 1.2f; //same length between all joints atm
    }

    void Update() {
        
    }

    public Vector3 getEndPoint() {
        return transform.position + ((transform.rotation * Vector3.forward).normalized * _length);
    }

    public void SetJointByEndPoint(Vector3 endpoint){
        transform.position = endpoint - ((transform.rotation * Vector3.forward).normalized * _length);
    }

    public void Flip(){  
        Vector3 relativePos = getEndPoint() - transform.position;
        Vector3 endPoint = getEndPoint();
        transform.rotation = Quaternion.FromToRotation(relativePos, Vector3.back);
        transform.position = endPoint;
    }

}
