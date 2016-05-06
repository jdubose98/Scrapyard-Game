using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float m_Speed = 6f; // Speed of the player during the game

	Vector3 m_Movement; //  A vector for the movement of the player based on user input
	// Animator m_Anim; // Referenc to the player animator
	Rigidbody m_PlayerRigidbody; // Reference to the player's rigidbody component
	int floorMask; // Reference to the floor
	float camRayLength = 100f; // The length of the camera's cast down on the scene

	void Awake() // Sets the references for the player animator and rigidbody components as
			// well as the floor
	{
		floorMask = LayerMask.GetMask ("Floor");
		// m_Anim = GetComponent<Animator> ();
		m_PlayerRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () // Sets the values for a horizontal and vertical component to the player
			// based on user input and checks to see if the player is moving or turning
	{
		float h = Input.GetAxisRaw ("Horizontal"); // horizontal position of the player based on user 
			// input
		float v = Input.GetAxisRaw ("Vertical"); // vertical position of the player based on user input

		Move (h, v);
//		Animating (h, v);
		Turning ();
	}

	void Move (float h, float v) // sets the movement of the player based on user input and for sets the
			// forward direction of movement based on the players orientation as well as the left and right
			// directions based on the player's orientation
	{
		m_Movement = transform.forward * v + transform.right * h;
		m_Movement = m_Movement.normalized * m_Speed * Time.deltaTime;
		m_PlayerRigidbody.MovePosition (transform.position + m_Movement);
	}

	void Turning () // Changes the rotation of the player based on the direction of a ray cast from the camera
			// and changes the player rotation accordingly
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			m_PlayerRigidbody.MoveRotation(newRotation);
		}
	}

//	void Animating (float h, float v) // Turns on the player animation for walking when the player is in motion by
//			// setting the boolean of this variable
//	{
//		bool walking = h != 0f || v != 0f;
//		m_Anim.SetBool ("IsWalking", walking);
//	}
}
