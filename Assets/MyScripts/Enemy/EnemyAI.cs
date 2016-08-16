using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public AudioClip shotSound;

    public float range;
    public int RPM;

    public float sec_BetweenShots;
    private float shotTimer;

    public GameObject muzzelPos;
    public GameObject bullet;

    private NavMeshAgent agent;
    private GameMaster gm;
    private DevLog devLog;
    private GameObject player;

    public bool canShoot;
    public bool isInRange;

    void Start()
    {
        canShoot = false;
        devLog = GameObject.Find(customStrings.devConsole).GetComponent<DevLog>();
        gm = GameObject.FindGameObjectWithTag(customTags.GameMaster).GetComponent<GameMaster>();
        agent = this.GetComponent<NavMeshAgent>();
        player = gm.player;
        sec_BetweenShots = 60f / RPM;
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            agent.destination = player.transform.position;

            shotTimer -= Time.deltaTime;
            checkRange();
            checkShoot();
        }
	}

    public void Shoot(Vector3 targetPos)
    {
        if (!canShoot)
        {
            return;
        }
        AudioSource aSource = this.GetComponent<AudioSource>();
        aSource.PlayOneShot(shotSound);

        GameObject clone = Instantiate(bullet, muzzelPos.transform.position, Quaternion.identity) as GameObject;
        clone.name = "Lazer";
        clone.transform.parent = gm.bulletParent.transform;

        targetPos = new Vector3(targetPos.x, clone.transform.position.y, targetPos.z);

        Vector3 relativePos = targetPos - clone.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        clone.transform.rotation = rotation;

        Rigidbody rigid = clone.GetComponent<Rigidbody>();
        rigid.AddForce(clone.transform.forward * gm.mgBulletForce);

        shotTimer = sec_BetweenShots;
        canShoot = false;
    }

    private void checkShoot()
    {
        if (shotTimer <= 0)
        {
            canShoot = true;
        }
        if (canShoot && isInRange)
        {
            Shoot(player.transform.position);
        }
    }

    public void checkRange()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) < range)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }
    }

    public void Death()
    {
        GameObject death = Instantiate(gm.bloodPrefab, this.transform.position, Quaternion.identity) as GameObject;
        death.transform.parent = gm.transform;
        gm.kills++;

        foreach(Transform child in death.transform)
        {
            if (child.GetComponent<Rigidbody>())
            {
                child.GetComponent<Rigidbody>().AddExplosionForce(gm.bulletExpForce, this.transform.position, gm.bulletExpRadius, gm.bulletUpwardMod, ForceMode.Impulse);
            }
        }

        Destroy(this.gameObject);
    }
}
