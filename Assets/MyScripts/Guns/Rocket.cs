using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rocket : MonoBehaviour {

    public Vector3 targetPos;
    public float stoppingDistance;

    private SphereCollider sc;

    private GameMaster gm;
    private DevLog devLog;

    public List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        devLog = GameObject.Find(customStrings.devConsole).GetComponent<DevLog>();
        gm = GameObject.FindGameObjectWithTag(customTags.GameMaster).GetComponent<GameMaster>();
        sc = this.GetComponent<SphereCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, gm.rocketSpeed * Time.deltaTime);
        checkDistance();
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.tag);
        if (other.transform.tag == customTags.Bullet || other.transform.tag == customTags.Player || other.transform.tag == customTags.Ground)
        {
            return;
        }

        if (other.transform.tag == customTags.Enemies)
        {
            Debug.Log("Adding enemy to the list");
            enemies.Add(other.gameObject);
        }
    }

    void checkDistance()
    {
        if(Vector3.Distance(this.transform.position, targetPos) <= stoppingDistance)
        {
            BlowUp();
        }
    }

    private void BlowUp()
    {
        checkEnemies();
        explodeEffect();
        killEnemies();
        addExplosiveForce();
        selfDestruct();
    }

    private void checkEnemies()
    {
        List<GameObject> newList = new List<GameObject>();
        float maxDist = sc.radius / 2f;
        for(int i = 0; i < enemies.Count; i++)
        {
            Vector3 pos = enemies[i].transform.position;
            if (Vector3.Distance(this.transform.position, pos) <= maxDist)
            {
                Debug.Log(Vector3.Distance(this.transform.position, pos) + " : i ");
                newList.Add(enemies[i]);
            }
        }
        enemies.Clear();
        enemies = newList;
    }

    private void addExplosiveForce()
    {
        this.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(25f, 25f, 25f), this.transform.position, ForceMode.Impulse);
    }

    private void killEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            EnemyAI enemyAI = enemies[i].GetComponent<EnemyAI>();
            enemyAI.Death();
        }

    }

    private void selfDestruct()
    {
        Destroy(this.gameObject);
    }

    void explodeEffect()
    {
        GameObject explosion = Instantiate(gm.explosionPrefab, this.transform.position, Quaternion.identity) as GameObject;
        explosion.transform.parent = gm.bulletParent.transform;
    }
}
