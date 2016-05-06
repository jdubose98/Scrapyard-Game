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

	int burn;
	public Image chargeBar;
	Text pain;
	public Image healthBar;
	PlayerSkill player;
	ParticleSystem hurt;
	public bool charge = true;

	public Canvas screen;

	void Awake()
	{
		healthBar = GameObject.Find ("EHBar").GetComponent<Image>();
		healthBar.fillAmount = 0;
		chargeBar = GameObject.Find ("ECharge").GetComponent<Image> ();
		chargeBar.fillAmount = 0;
		player = GameObject.Find ("Ethan").GetComponent<PlayerSkill> ();
		hurt = GetComponent<ParticleSystem> ();
		screen = GameObject.Find ("Canvas").GetComponent<Canvas>();
		screen.enabled = false;
		pain = GameObject.Find ("Pain").GetComponent<Text> ();
		// InvokeRepeating ("Attack", m_Speed, 1.0f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			screen.enabled = true;
		}
	}

	void FixedUpdate()
	{
		if (screen.enabled && healthBar.fillAmount < 1) 
		{
			if (charge) 
			{
				chargeBar.fillAmount += Mathf.Lerp (0, 1, m_Speed * Time.fixedDeltaTime);
			}
			if (chargeBar.fillAmount == 1) 
			{
				Attack ();
			}
		}
	}

	void Attack()
	{
		player.TakeDamage (m_Attack);
		chargeBar.fillAmount = 0;
	}

	public void TakeDamage (float damage)
	{
		healthBar.fillAmount += ((damage - m_Defense) * (1/m_Health));
		pain.text = "- " + (damage - m_Defense);
		StartCoroutine (DamageToScreen ());
//		hurt.maxParticles = (int) damage;
//		hurt.Play ();
	}

	public void Die()
	{
		hurt.Play ();
		pain.text = " ";
		screen.enabled = false;
		healthBar.fillAmount = 0;
		Destroy (gameObject);
	}

	IEnumerator DamageToScreen()
	{
		yield return new WaitForSeconds (3);
		pain.text = " ";
	}
		
}
