using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField] private Limb[] _limbs;
    private bool _limbUp;//there is a limb in the air

    //set one limb to be on the ground first, move to ground, reverse endpoint and root

    //this script will organize step locations

    // Start is called before the first frame update
    void Start()
    {
        _limbs[0].SetLimbGoal(new Vector3(transform.position.x, 0, transform.position.z + 1));
        _limbs[1].SetLimbGoal(new Vector3(transform.position.x, 0, transform.position.z - 1));
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _limbs.Length; i++){
            if(Vector3.Distance(_limbs[i].GetLimbEndPosition(),_limbs[i].GetLimbGoal()) < 0.01f && !_limbUp){
                _limbs[i].ReverseJoints();//need to fix this function to change the root
                _limbUp = true;
                _limbs[i].SetLimbGoal(_limbs[i].GetLimbEndPosition() + Vector3.left);
            }
        }
        
    }
}
