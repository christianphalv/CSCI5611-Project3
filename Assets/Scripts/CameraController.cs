using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject character;
    private Vector3 _distance;

    void Start() {
        _distance = new Vector3(0f, 10f, -10f);
    }

    void Update() {
        transform.position = character.transform.position + _distance;
        transform.LookAt(character.transform);
    }
}
