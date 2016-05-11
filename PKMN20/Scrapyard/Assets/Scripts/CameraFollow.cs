using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	Transform player;
	EnemyEnable game;
	Vector3 battleMode;

	void Awake()
	{
		player = GameObject.FindWithTag ("Player").GetComponent<Transform>();
		game = GameObject.Find ("Bot").GetComponent<EnemyEnable> ();

	}

	void FixedUpdate()
	{
		transform.position = new Vector3 (player.position.x + 1.5f, 1.5f, player.position.z + 1.0f);

		if (game.screen.enabled)
		{
			battleMode = new Vector3 (player.position.x + 1.0f, 0.8f, player.position.z + 2.5f);
			transform.position = Vector3.Lerp (transform.position, battleMode, 1.0f);
		}
			
	}

}
