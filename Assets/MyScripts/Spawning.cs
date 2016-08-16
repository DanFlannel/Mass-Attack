using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {

    private BoxCollider bc;
    private DevLog devLog;
    private GameMaster gm;

    private float timer;


    void Start()
    {
        devLog = GameObject.Find(customStrings.devConsole).GetComponent<DevLog>();
        gm = GameObject.FindGameObjectWithTag(customTags.GameMaster).GetComponent<GameMaster>();
        bc = this.GetComponent<BoxCollider>();
        timer = gm.spawnTimer;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = gm.spawnTimer;
            spawn();
        }
	}

    private void spawn()
    {

        Bounds bounds = bc.bounds;

        Vector3 center = bounds.center;
        float x = UnityEngine.Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
        float z = UnityEngine.Random.Range(center.z - bounds.extents.y, center.z + bounds.extents.z);

        Vector3 spawnLoc = new Vector3(x, bounds.center.y, z);
        int rnd = UnityEngine.Random.Range(0, gm.enemyPrefabs.Length);

        GameObject alien = Instantiate(gm.enemyPrefabs[rnd], spawnLoc, Quaternion.identity) as GameObject;
        alien.transform.parent = gm.enemyParent.transform;


    }


}
