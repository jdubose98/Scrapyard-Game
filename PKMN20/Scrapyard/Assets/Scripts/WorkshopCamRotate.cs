using UnityEngine;
using System.Collections;

public class WorkshopCamRotate : MonoBehaviour {

    [SerializeField] float SpinSpeed = -20;

    [SerializeField] bool CanSpin = true;

    [SerializeField] GameObject ObjectToFocusOn;

	// Update is called once per frame
	void FixedUpdate () {

        if (CanSpin)
        transform.RotateAround(Vector3.zero, Vector3.up, SpinSpeed * Time.deltaTime);

	}
}
