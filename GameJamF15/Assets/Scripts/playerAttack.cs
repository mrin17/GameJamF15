using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	readonly string KICK_ATTACK = "z";
	readonly string LIFT_ATTACK = "x";
	readonly string OVERHEAD_ATTACK = "c";

	const float KICK_MAX = .2f;
	const float LIFT_MAX = .1f;
	const float OVERHEAD_MAX = .5f;
	const float KICK_INTRO_MAX = .12f;
	const float LIFT_INTRO_MAX = .0001f;
	const float OVERHEAD_INTRO_MAX = .0001f;
	const float KICK_COOL_MAX = .0001f;
	const float LIFT_COOL_MAX = .3f;
	const float OVERHEAD_COOL_MAX = .0001f;
	float attackTimer = 0;
	float attackIntroTimer = 0;
	float attackCooldownTimer = 0;
    float gameOver = 0;

    private AudioSource source;
    AudioClip kickSound;
    AudioClip liftSound;
    AudioClip deathSound;
    AudioClip crumbleSound;

    GameObject attack;
	string attackType = ""; //kick, lift, overhead

	public const float MAX_HEALTH = 5;
	float health = MAX_HEALTH;
    public const float X_LIFE_SCORE = 1000;
    float score = 0;

	Animator anim;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
		anim = GetComponent<Animator> ();
        kickSound = (AudioClip)Resources.Load("Kick");
        liftSound = (AudioClip)Resources.Load("Lift");
        deathSound = (AudioClip)Resources.Load("GameOver");

    }
	
	// Update is called once per frame
	void Update () {
		//time before the attack is created
		if (attackIntroTimer > 0) {
			attackIntroTimer -= Time.deltaTime;
		}
		//you cant attack while youre attacking
		else if (attackTimer > 0) {
			if (attackIntroTimer < 0) {
				attackIntroTimer = 0;
				//create attack
				if (attackType == "kick")
					attack = (GameObject) Instantiate (Resources.Load ("prePushAttack"), transform.position, Quaternion.identity);
				else if (attackType == "lift")
					attack = (GameObject) Instantiate (Resources.Load ("preLiftAttack"), transform.position, Quaternion.identity);
				else if (attackType == "overhead")
					attack = (GameObject) Instantiate (Resources.Load ("preThrownRock"), transform.position + new Vector3(0, .5f, 0), Quaternion.identity);
			}
			attackTimer -= Time.deltaTime;
		}
		else if (attackCooldownTimer > 0) {
			if (attackTimer < 0) {
				attackTimer = 0;
				Destroy (attack);
			}
			attackCooldownTimer -= Time.deltaTime;
		}
		//attack keys
		else if (Input.GetKeyDown (KICK_ATTACK)) {
			attackTimer = KICK_MAX;
			attackIntroTimer = KICK_INTRO_MAX;
			attackCooldownTimer = KICK_COOL_MAX;
            //SET KICK ANIM
            source.PlayOneShot(kickSound, 1F);
			attackType = "kick";
			anim.SetInteger("attackType", 1);
			//CREATE SOME SORT OF OBJ (after introtimer is done)

		}
		else if (Input.GetKeyDown (LIFT_ATTACK)) {
			attackTimer = LIFT_MAX;
			attackIntroTimer = LIFT_INTRO_MAX;
			attackCooldownTimer = LIFT_COOL_MAX;
			attackType = "lift";
            //SET LIFT ANIM
            source.PlayOneShot(liftSound, 1F);
            anim.SetInteger("attackType", 2);
			//CREATE SOME SORT OF OBJ (after introtimer is done)

		}
		else if (Input.GetKeyDown (OVERHEAD_ATTACK)) {
			attackTimer = OVERHEAD_MAX;
			attackIntroTimer = OVERHEAD_INTRO_MAX;
			attackCooldownTimer = OVERHEAD_COOL_MAX;

			attackType = "overhead";
            source.PlayOneShot(liftSound, 1F);
            //we dont need to set an anim for this
            //CREATE SOME SORT OF OBJ (after introtimer is done)
        }
		if (attack != null && attackType != "overhead") {
			float addOn = 0;
			if (attackType == "lift")
				addOn = .1f;
			//it has to lock on to the same location
			float dir = GetComponent<CubeControl>().getLastDir();
			if (dir > 0)
				attack.transform.position = transform.position + new Vector3(.5f, -.35f+addOn, 0);
			else if (dir < 0)
				attack.transform.position = transform.position + new Vector3(-.5f, -.35f+addOn, 0);
		}

		if (attackCooldownTimer < 0) {
			attackCooldownTimer = 0;
			attackType = "";
			anim.SetInteger("attackType", 0);
		}
        if (getHealth() <= 0)
        {
            if (gameOver < 1 )
            {
                death();
                gameOver++;
            }
            else if(!source.isPlaying)
            { Application.LoadLevel("introScene"); }
        }
	}

	public bool isAttacking() {
		return attack != null;
	}

    public string getAttackType()
    {
        return attackType;
    }

	public void takeDamage() {
		health--;
		FindObjectOfType<scrCameraShake> ().Shake (.5f);
		FindObjectOfType<heartController> ().removeHeart ();
	}

	public float getHealth() { 
		return health; 
	}
    public float getScore()
    {
        return score;
    }
    public void addToScore() {
        score += 100;
    }

    public bool canExplode(GameObject go)
    {
        return (!isAttacking() || (isAttacking() && (getAttackType() == "overhead" ||
         !((transform.position.x > go.transform.position.x && GetComponent<CubeControl>().getLastDir() < 0) ||
         (transform.position.x < go.transform.position.x && GetComponent<CubeControl>().getLastDir() > 0)))));
    }

    public void death() {
        FindObjectOfType<bombSpawner>().GetComponent<AudioSource>().Stop();
        anim.SetBool("crumbling", true);
        source.PlayOneShot(deathSound, .5f);
    }
}
