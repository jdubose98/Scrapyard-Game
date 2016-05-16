using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	[SerializeField] float m_Attack1;
	[SerializeField] float m_Attack2;
	[SerializeField] float m_Attack3;

	public bool caught;

	PlayerSkill player;
	EnemyAttack enemy;
	Text reward;

	void Start()
	{
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerSkill> ();
		//reward = GameObject.Find ("Reward").GetComponent<Text>();
		caught = false;
	}

	void FixedUpdate()
	{
		enemy = player.enemy;
	}

	public void One()
	{
		if (player.fight) 
		{
			player.Attack (m_Attack1);
			player.fight = false;
			player.chargeBar.fillAmount = 0;
		}
	}

	public void Two()
	{
		if (player.fight) 
		{
			player.Attack (m_Attack2);
			player.fight = false;
			player.chargeBar.fillAmount = 0;
		}
	}

	public void Three()
	{
		if (player.fight) 
		{
			player.Attack (m_Attack3);
			player.fight = false;
			player.chargeBar.fillAmount = 0;
		}
	}

	public void Capture()
	{
		if (player.fight) 
		{
			int seize = Random.Range (0, (int)(enemy.healthBar.fillAmount * 100));

			Debug.Log (seize);

			if (seize >= 50) 
			{
				Debug.Log ("Capture");

				StartCoroutine (Catch ());
			}

			player.fight = false;
			// caught = false;
			player.chargeBar.fillAmount = 0;
		}
	}

	public void SpecialB()
	{
		if (player.fight) 
		{
			player.Burn ();
			player.fight = false;
			player.chargeBar.fillAmount = 0;
		}
			
	}

	public void SpecialS()
	{
		if (player.fight) 
		{
			enemy.Stun ();
			player.fight = false;
			player.chargeBar.fillAmount = 0;
		}
	}

	IEnumerator Catch()
	{
		enemy.hurt.maxParticles = 10;
		enemy.hurt.Play();
		yield return new WaitForSeconds (2);
		enemy.Die ();
		Debug.Log ("Part 2");
		reward.text = "Enemy Captured!";
		Debug.Log ("Part 3");
		yield return new WaitForSeconds (3);
		reward.text = " ";
		Debug.Log ("Part 4");
	}
}
