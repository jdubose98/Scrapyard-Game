using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSkill : MonoBehaviour {

	// these values will change based on the player's stats
	//	so having them serialized is temporary until the stat system is set up
	[SerializeField] float m_Speed; // keep speed values between the interval (0, 1]
	[SerializeField] float m_Attack;
	[SerializeField] float m_Defense;
	[SerializeField] float m_Health;

	public bool fight;
	public Image chargeBar;

	Image healthBar;
	public EnemyAttack enemy;

	void Awake()
	{
		healthBar = GameObject.Find ("PHBar").GetComponent<Image> ();
		healthBar.fillAmount = 0;
		chargeBar = GameObject.Find ("PCharge").GetComponent<Image> ();
		chargeBar.fillAmount = 0;
		enemy = GameObject.Find ("Bot").GetComponent<EnemyAttack> ();
		fight = false;

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			enemy = other.gameObject.GetComponent<EnemyAttack> ();
		}
	}

	void FixedUpdate()
	{
		if (enemy.screen.enabled) 
		{
			chargeBar.fillAmount += Mathf.Lerp (0, 1, m_Speed * Time.fixedDeltaTime);

			if (chargeBar.fillAmount == 1) 
			{
				fight = true;
			}
		}

		if (enemy.healthBar.fillAmount == 1) 
		{
			enemy.Die ();
		}
	}

	public void Attack(float mult)
	{
		enemy.TakeDamage (m_Attack * mult);
	}

	public void TakeDamage (float damage)
	{
		healthBar.fillAmount += ((damage - m_Defense) * (1/m_Health));
	}

//	IEnumerator Charge (float time)
//	{
//		
//	}
}
