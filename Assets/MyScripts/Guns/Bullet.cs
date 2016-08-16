using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public GameMaster gm;
    private DevLog devLog;

    void Start()
    {
        devLog = GameObject.Find(customStrings.devConsole).GetComponent<DevLog>();

        gm = GameObject.FindGameObjectWithTag(customTags.GameMaster).GetComponent<GameMaster>();
    }
	
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.tag);
        if (other.transform.tag == customTags.Bullet || other.transform.tag == customTags.Player)
        {
            return;
        }
        if(other.transform.tag == customTags.Enemies)
        {
            devLog.WriteConsole(customStrings.enemyKilledMG);
            EnemyAI enemyAI = other.transform.GetComponent<EnemyAI>();
            enemyAI.Death();
            this.GetComponent<Rigidbody>().AddExplosionForce(gm.bulletExpForce, this.transform.position, gm.bulletExpRadius, gm.bulletUpwardMod, ForceMode.Impulse);
        }
        else
        {
            devLog.WriteConsole(customStrings.playerMissedMG);
        }
        selfDestruct();
    }

    void selfDestruct()
    {
        Destroy(this.gameObject);
    }
}
