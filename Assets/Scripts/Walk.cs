using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField] private Leg[] _legs;
    private bool _limbUp;//there is a limb in the air

    //set one limb to be on the ground first, move to ground, reverse endpoint and root

    //this script will organize step locations

    // Start is called before the first frame update
    void Start()
    {
        _legs[0].SetLimbGoal(new Vector3(transform.position.x, 0, transform.position.z + 1));
        _legs[1].SetLimbGoal(new Vector3(transform.position.x, 0, transform.position.z - 1));
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _legs.Length; i++){
            if(Vector3.Distance(_legs[i].GetLimbEndPosition(),_legs[i].GetLimbGoal()) < 0.01f && !_limbUp){
                //_legs[i].ReverseJoints();//need to fix this function to change the root
                _limbUp = true;
                _legs[i].SetLimbGoal(_legs[i].GetLimbEndPosition() + Vector3.left);
            }
        }
        
    }
}
