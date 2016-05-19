using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	[SerializeField] float m_Attack1;
	[SerializeField] float m_Attack2;
	[SerializeField] float m_Attack3;

	public bool caught;
    public int i;

	PlayerSkill player;
	EnemyAttack enemy;
	// Text reward;
    GameObject base_Panel, attack_Panel, defend_Panel, special_Panel;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerSkill> ();
		// reward = GameObject.Find ("Reward").GetComponent<Text>();
        base_Panel = GameObject.Find("StandardMoves");
        base_Panel.SetActive(true);
        attack_Panel = GameObject.Find("AttackMoves");
        attack_Panel.SetActive(false);
        defend_Panel = GameObject.Find("DefendMoves");
        defend_Panel.SetActive(false);
        special_Panel = GameObject.Find("PeripheralMoves");
        special_Panel.SetActive(false);
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
            Time.timeScale = 1;
            base_Panel.SetActive(true);
            attack_Panel.SetActive(false);
            player.chargeBar.fillAmount = 0;
		}
	}

	public void Two()
	{
		if (player.fight) 
		{
			player.Attack (m_Attack2);
			player.fight = false;
            Time.timeScale = 1;
            base_Panel.SetActive(true);
            attack_Panel.SetActive(false);
            player.chargeBar.fillAmount = 0;
		}
	}

	public void Three()
	{
		if (player.fight) 
		{
			player.Attack (m_Attack3);
			player.fight = false;
            Time.timeScale = 1;
            base_Panel.SetActive(true);
            attack_Panel.SetActive(false);
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
            Time.timeScale = 1;
            base_Panel.SetActive(true);
            attack_Panel.SetActive(false);
            player.chargeBar.fillAmount = 0;
		}
	}

	public void SpecialB()
	{
		if (player.fight) 
		{
			player.Burn ();
			player.fight = false;
            Time.timeScale = 1;
            base_Panel.SetActive(true);
            special_Panel.SetActive(false);
            player.chargeBar.fillAmount = 0;
		}		
	}

	public void SpecialS()
	{
		if (player.fight) 
		{
			enemy.Stun ();
			player.fight = false;
            Time.timeScale = 1;
            base_Panel.SetActive(true);
            special_Panel.SetActive(false);
            player.chargeBar.fillAmount = 0;
		}
	}

    public void AttackUp()
    {
        base_Panel.SetActive(false);
        special_Panel.SetActive(false);
        defend_Panel.SetActive(false);
        attack_Panel.SetActive(true);
    }

    public void DefendUp()
    {
        base_Panel.SetActive(false);
        special_Panel.SetActive(false);
        attack_Panel.SetActive(false);
        defend_Panel.SetActive(true);
    }

    public void SpecialUp()
    {
        base_Panel.SetActive(false);
        attack_Panel.SetActive(false);
        defend_Panel.SetActive(false);
        special_Panel.SetActive(true);
    }

    public void Block()
    {
        int attempt = Random.Range(1, i);
        Debug.Log(i + ", " + attempt);
        if (attempt == i)
        {
            i++;
            player.healthBar.fillAmount -= 0.1f;
            player.fight = false;
            Time.timeScale = 1;
            base_Panel.SetActive(true);
            defend_Panel.SetActive(false);
            player.chargeBar.fillAmount = 0;
        }
        else
        {
            i++;
            player.fight = false;
            Time.timeScale = 1;
            base_Panel.SetActive(true);
            defend_Panel.SetActive(false);
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
		// reward.text = "Enemy Captured!";
		Debug.Log ("Part 3");
		yield return new WaitForSeconds (3);
		// reward.text = " ";
		Debug.Log ("Part 4");
	}
}
