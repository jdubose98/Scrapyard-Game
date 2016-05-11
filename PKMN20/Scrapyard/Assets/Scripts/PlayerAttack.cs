using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    GameObject Player;

	[SerializeField] float m_Attack1;
	[SerializeField] float m_Attack2;
	[SerializeField] float m_Attack3;

	PlayerSkill player;
	EnemyAttack enemy;

	void Start()
	{
		player = Player.GetComponent<PlayerSkill> ();
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
				Debug.Log ("Captured");
			}
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
}
