using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbGoal : MonoBehaviour {//All new positions will be on the ground!


    void Start() {
        
    }

    void Update() {
        
    }

    public void SetNewPosition(Vector3 newposition){
        gameObject.transform.position = newposition;
    }

    
}
