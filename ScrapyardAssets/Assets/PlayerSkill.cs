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
	[SerializeField] float m_BurnDuration;
	[SerializeField] float m_StunDuration;

	// public variables

	public bool fight;
	public Image chargeBar;
	public EnemyAttack enemy;

	// private variables

	Image healthBar;
	EnemyEnable battle;
	bool charge;

	// Private methods

	void Awake()
	{
		healthBar = GameObject.Find ("PHBar").GetComponent<Image> ();
		healthBar.fillAmount = 0;
		chargeBar = GameObject.Find ("PCharge").GetComponent<Image> ();
		chargeBar.fillAmount = 0;
		enemy = GameObject.Find ("Bot").GetComponent<EnemyAttack> ();
		battle = enemy.GetComponent<EnemyEnable> ();
		fight = false;
		charge = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			enemy = other.gameObject.GetComponent<EnemyAttack> ();
			Debug.Log (other.name);
		}
	}

	void FixedUpdate()
	{
		if (battle.screen.enabled) 
		{
			if (charge) 
			{
				chargeBar.fillAmount += (m_Speed * Time.fixedDeltaTime);
			}
			else 
			{
				chargeBar.fillAmount += 0;
			}

			if (chargeBar.fillAmount == 1) 
			{
				fight = true;
			}
		}

		if (enemy.healthBar.fillAmount == 1) 
		{
			CancelInvoke ();
			enemy.Die ();
		}

		if (healthBar.fillAmount == 1) 
		{
			CameraFollow cam = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
			cam.enabled = false;
			Destroy (gameObject);
		}
	}

	void Attack()
	{
		enemy.TakeDamage (1.0f);
	}

	// Public methods

	public void Attack(float mult)
	{
		enemy.TakeDamage (m_Attack * mult);
	}

	public void Burn()
	{
		InvokeRepeating ("Attack", 1.0f, 2.0f);
		StartCoroutine (Extinguish ());
	}

	public void Stun()
	{
		StartCoroutine (Freeze ());
	}
		
	public void TakeDamage (float damage)
	{
		healthBar.fillAmount += ((damage - m_Defense) * (1/m_Health));
	}

	// IEnumerators

	IEnumerator Extinguish()
	{
		yield return new WaitForSeconds (m_BurnDuration);
		CancelInvoke ();
	}

	IEnumerator Freeze ()
	{
		charge = false;
		yield return new WaitForSeconds (m_StunDuration);
		charge = true;
	}

}
