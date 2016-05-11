using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyEnable : MonoBehaviour {

	public Canvas screen;
	EnemyAttack self;

    [SerializeField]
    AudioSource BattleMusic;
    [SerializeField]
    AudioClip[] BattleMusicClips;

    void Awake()
	{
		
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

            BattleMusic.clip = BattleMusicClips[Random.Range(0, BattleMusicClips.Length - 1)];
            BattleMusic.Play();
        }
	}
}
