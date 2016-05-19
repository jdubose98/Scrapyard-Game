using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;

public class EnemyEnable : MonoBehaviour {

    [SerializeField]
    AudioMixerSnapshot BattleSnapshot;
    [SerializeField]
    AudioMixerSnapshot RoamSnapshot;

    [SerializeField]
    AudioClip[] BattleMusicClips;

    [SerializeField]
    AudioSource BattleMusic;

    [SerializeField]
    Camera BattleCamera;


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
            gameObject.GetComponent<AudioSource>().Play();
			screen.enabled = true;
			self.enabled = true;
            BattleSnapshot.TransitionTo(1f);
            BattleMusic.clip = BattleMusicClips[Random.Range(0,BattleMusicClips.Length-1)];
            BattleMusic.Play();
            BattleCamera.depth = 3f; ;
            GameObject.Find("Controller").GetComponent<DeadSimpleMoveScript>().enabled = false;
        }
	}
}
