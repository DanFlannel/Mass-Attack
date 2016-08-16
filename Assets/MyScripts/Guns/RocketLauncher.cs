using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {

    public int RPM;
    private float sec_BetweenShots;
    private bool canShoot;


    public AudioClip rocketNoise;
    public GameObject bullet;
    public GameObject muzzelPos;

    private GameMaster gm;
    private float shotTimer;
    private DevLog devLog;

    void Start()
    {
        devLog = GameObject.Find(customStrings.devConsole).GetComponent<DevLog>();
        gm = GameObject.FindGameObjectWithTag(customTags.GameMaster).GetComponent<GameMaster>();
        sec_BetweenShots = 60f / RPM;
        shotTimer = sec_BetweenShots;
        //Debug.Log(sec_BetweenShots);
    }

    void Update()
    {
        if (shotTimer <= 0)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
        if (!canShoot)
        {
            shotTimer -= Time.deltaTime;
        }
    }

    public void Shoot(AudioSource aSource, Vector3 targetPos)
    {
        if (!canShoot)
        {
            return;
        }

        aSource.PlayOneShot(rocketNoise);
        GameObject clone = Instantiate(bullet, muzzelPos.transform.position, Quaternion.identity) as GameObject;
        clone.name = "rocket";
        clone.transform.parent = gm.bulletParent.transform;

        targetPos = new Vector3(targetPos.x, clone.transform.position.y, targetPos.z);

        Vector3 relativePos = targetPos - clone.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        clone.transform.rotation = rotation;

        clone.GetComponent<Rocket>().targetPos = targetPos;

        devLog.WriteConsole(customStrings.shootRL);
        shotTimer = sec_BetweenShots;
        gm.rockets++;
    }
}
