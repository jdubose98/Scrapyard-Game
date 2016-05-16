using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyEnable : MonoBehaviour {

	public Canvas screen;
	EnemyAttack self;

	void Awake()
	{
		screen = GameObject.Find ("Battle canvas").GetComponent<Canvas>();
		screen.enabled = false;
		self = this.GetComponent<EnemyAttack> ();
		self.enabled = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			screen.enabled = true;
			self.enabled = true;
		}
	}
}
