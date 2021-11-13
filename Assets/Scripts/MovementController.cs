using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    protected Leg[] _legs;
    protected Arm[] _arms;

    protected int _currentLeg;
    protected int _currentArm;

    protected float _movementSpeed;



    void Start() {

        // Initialize limbs
        _legs = GetComponentsInChildren<Leg>();
        _arms = GetComponentsInChildren<Arm>();

        _currentLeg = 0;
        _currentArm = 0;
        _movementSpeed = 8f;
    }

    void Update() {

        // Switch current leg
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (_currentLeg == 0) {
                _currentLeg = 1;
            }
            else {
                _currentLeg = 0;
            }
        }

        // Switch current arm
        if (Input.GetKeyDown("r")) {
            if (_currentArm == 0) {
                _currentArm = 1;
            } else {
                _currentArm = 0;
            }
        }

        // Update current leg and arm from user input
        _legs[_currentLeg].getGoal().transform.position += getLegInput() * _movementSpeed * Time.deltaTime;
        _arms[_currentArm].getGoal().transform.position += getArmInput() * _movementSpeed * Time.deltaTime;

    }

    private Vector3 getArmInput() {
        Vector3 direction = new Vector3(0f, 0f, 0f);

        if (Input.GetKey("w")) {
            direction += new Vector3(0, 0, 1);
        }

        if (Input.GetKey("s")) {
            direction += new Vector3(0, 0, -1);
        }

        if (Input.GetKey("a")) {
            direction += new Vector3(-1, 0, 0);
        }

        if (Input.GetKey("d")) {
            direction += new Vector3(1, 0, 0);
        }

        if (Input.GetKey("e")) {
            direction += new Vector3(0, 1, 0);
        }

        if (Input.GetKey("q")) {
            direction += new Vector3(0, -1, 0);
        }

        return direction.normalized;
    }

    private Vector3 getLegInput() {
        Vector3 direction = new Vector3(0f, 0f, 0f);

        if (Input.GetKey(KeyCode.UpArrow)) {
            direction += new Vector3(0, 0, 1);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            direction += new Vector3(0, 0, -1);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            direction += new Vector3(-1, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            direction += new Vector3(1, 0, 0);
        }

        return direction.normalized;
    }


}
