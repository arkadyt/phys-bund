using UnityEngine;
using System.Collections;

public class FreezeRotationScript : MonoBehaviour {
	private float defaultZRotation;
    void Start(){
        defaultZRotation = transform.rotation.eulerAngles.z;
    }
	void Update () {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.z = defaultZRotation;
		transform.rotation = Quaternion.Euler (currentRotation);
	}
}
