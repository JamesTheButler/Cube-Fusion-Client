using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour {
    public float rotationSpeed;
    	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
