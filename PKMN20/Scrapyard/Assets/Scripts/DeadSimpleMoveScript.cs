using UnityEngine;
using System.Collections;

public class DeadSimpleMoveScript : MonoBehaviour {

    [SerializeField]
    float MoveSpeed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) {
                gameObject.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed, 0, Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed);
        }


    }
}
