using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;

public class PlayerSkill : MonoBehaviour {

	// these values will change based on the player's stats
	//	so having them serialized is temporary until the stat system is set up
	[SerializeField] float m_Speed; // keep speed values between the interval (0, 1)
	[SerializeField] float m_Attack;
	[SerializeField] float m_Defense;
	[SerializeField] float m_Health;
	[SerializeField] float m_BurnDuration;
	[SerializeField] float m_StunDuration;
    [SerializeField]
    Camera BattleCamera;

    [SerializeField]
    AudioMixerSnapshot RoamSnapshot;

    // public variables

    public bool fight;
	public Image chargeBar;
	public EnemyAttack enemy;

	// private variables

	public Image healthBar;
	EnemyEnable battle;
    PlayerAttack ok;
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
        ok = GameObject.Find("Battle canvas").GetComponent<PlayerAttack>();
		fight = false;
		charge = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
            
            enemy = other.gameObject.GetComponent<EnemyAttack> ();
            ok.i = 1;
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
                Time.timeScale = 0;
			}
		}

		if (enemy.healthBar.fillAmount == 1) 
		{
            // Relinquish control.
			CancelInvoke ();
			enemy.Die ();
            BattleCamera.depth = -1;
            RoamSnapshot.TransitionTo(1f);
		}

		if (healthBar.fillAmount == 1) 
		{
            BattleCamera.depth = -1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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
        float hit = m_Attack * (m_Attack / 10);
		enemy.TakeDamage (hit * mult);
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
        float block = damage * (m_Defense / 10);

        if (block <= (m_Defense * 1.25))
        {
            healthBar.fillAmount += ((damage - block) * (1 / m_Health));
        }
        else
        {
            block = m_Defense * 1.25f;
            healthBar.fillAmount += ((damage - block) * (1 / m_Health));
        }

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
