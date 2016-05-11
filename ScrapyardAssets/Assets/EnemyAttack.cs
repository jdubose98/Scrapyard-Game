using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	// these values will change based on the enemy's stats
	//	so having them serialized is temporary until the stat system is set up
	[SerializeField] float m_Speed;
	[SerializeField] float m_Attack;
	[SerializeField] float m_Defense;
	[SerializeField] float m_Health;
	[SerializeField] float m_BurnDuration;
	[SerializeField] float m_StunDuration;

	float up;
	public Image chargeBar;
	Text pain;
	public Image healthBar;
	PlayerSkill player;
	ParticleSystem hurt;
	EnemyEnable self;
	public bool charge = true;

	void Awake()
	{
		healthBar = GameObject.Find ("EHBar").GetComponent<Image>();
		healthBar.fillAmount = 0;
		chargeBar = GameObject.Find ("ECharge").GetComponent<Image> ();
		chargeBar.fillAmount = 0;
		player = GameObject.Find ("Ethan").GetComponent<PlayerSkill> ();
		hurt = GetComponent<ParticleSystem> ();
		self = this.GetComponent<EnemyEnable> ();
		pain = GameObject.Find ("Pain").GetComponent<Text> ();
		charge = true;
		up = this.m_Speed * Time.fixedDeltaTime;

		Debug.Log (Time.fixedDeltaTime);
		Debug.Log (up);

	}

	void FixedUpdate()
	{
		if (self.screen.enabled && healthBar.fillAmount < 1) 
		{
			if (charge) 
			{
				chargeBar.fillAmount += up;
			}
			else 
			{
				chargeBar.fillAmount += 0;
				Debug.Log (up);
			}

			if (chargeBar.fillAmount == 1) 
			{
				int move = (int) Random.Range (0, 3);

				switch (move) {
				case 0:
					Attack ();
					break;
				case 1:
					Burn();
					chargeBar.fillAmount = 0;
					break;
				case 2:
					player.Stun ();
					chargeBar.fillAmount = 0;
					break;
				default:
					Attack ();
					break;
				}
			}
		}
	}

	void Attack()
	{
		player.TakeDamage (m_Attack);
		chargeBar.fillAmount = 0;
	}

	void BaseAttack()
	{
		player.TakeDamage (1.0f);
	}

	void Burn()
	{
		InvokeRepeating ("BaseAttack", 1.0f, 2.0f);
		StartCoroutine (Extinguish ());
	}

	public void TakeDamage (float damage)
	{
		healthBar.fillAmount += ((damage - m_Defense) * (1/m_Health));
		pain.text = "- " + (damage - m_Defense);
		StartCoroutine (DamageToScreen ());
//		hurt.maxParticles = (int) damage;
//		hurt.Play ();
	}

	public void Stun()
	{
		StartCoroutine (Freeze ());
	}

	public void Die()
	{
		hurt.Play ();
		pain.text = " ";
		self.screen.enabled = false;
		healthBar.fillAmount = 0;
		Destroy (gameObject);
	}

	IEnumerator DamageToScreen()
	{
		yield return new WaitForSeconds (3);
		pain.text = " ";
	}

	IEnumerator Freeze ()
	{
		charge = false;
		yield return new WaitForSeconds (m_StunDuration);
		charge = true;
	}
		
	IEnumerator Extinguish()
	{
		yield return new WaitForSeconds (m_BurnDuration);
		CancelInvoke ();
	}
}
