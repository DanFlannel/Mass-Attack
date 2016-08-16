using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    public GameMaster gm;
    private DevLog devLog;

    void Start()
    {
        devLog = GameObject.Find(customStrings.devConsole).GetComponent<DevLog>();

        gm = GameObject.FindGameObjectWithTag(customTags.GameMaster).GetComponent<GameMaster>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == customTags.Bullet || other.transform.tag == customTags.Enemies)
        {
            return;
        }


        if (other.transform.tag == customTags.Player)
        {
            devLog.WriteConsole(customStrings.hitPlayer);
            playerControls player = other.GetComponent<playerControls>();
            player.applyDamage(gm.enemyDamage);
        }
        selfDestruct();
    }

    void selfDestruct()
    {
        Destroy(this.gameObject);
    }
}
