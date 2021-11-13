using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    //[SerializeField] private Leg[] _legs;
    private Leg[] _legs;
    private float _movementThreshold;
    //private bool _limbUp;//there is a limb in the air


    void Start() {
        _legs = GetComponentsInChildren<Leg>();
        _movementThreshold = 1f;
    }

    private void Update() {
        updateBodyPosition();
    }

    private void updateBodyPosition() {

        // Retrieve leg goal positions
        Vector3 goalOnePosition = _legs[0].getGoal().transform.position;
        Vector3 goalTwoPosition = _legs[1].getGoal().transform.position;

        // Calculate midpoint between goals
        Vector3 differenceVector = goalTwoPosition - goalOnePosition;
        Vector3 midpoint = goalOnePosition + (differenceVector / 2f);

        // Translate body to be at midpoint of leg goals
        transform.position = new Vector3(midpoint.x, transform.position.y, midpoint.z);


        // Reset positions of leg goals
        _legs[0].getGoal().transform.position = goalOnePosition;
        _legs[1].getGoal().transform.position = goalTwoPosition;
    }

    //set one limb to be on the ground first, move to ground, reverse endpoint and root

    //this script will organize step locations

    // Start is called before the first frame update
    /*
    void Start()
    {
        _legs = GetComponentsInChildren<Leg>();

        _legs[0].SetLimbGoal(new Vector3(transform.position.x, 0, transform.position.z + 1));
        _legs[1].SetLimbGoal(new Vector3(transform.position.x, 0, transform.position.z - 1));
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _legs.Length; i++){
            if(Vector3.Distance(_legs[i].GetLimbEndPosition(),_legs[i].GetLimbGoal()) < 0.01f && !_limbUp){
                //_legs[i].SetLimbGoal(_legs[i].GetLimbEndPosition() + Vector3.left);
                Vector3 currentRoot = _legs[i].transform.position;
                _legs[i].SetLimbGoal(currentRoot);
                _legs[i].ReverseJoints();//need to fix this function to change the root
                
                _limbUp = true;
                
            }
        }
        
    }
    */
}
