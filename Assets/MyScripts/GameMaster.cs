using UnityEngine;

public class GameMaster : MonoBehaviour {

    [Header("Enemies")]
    public GameObject[] enemyPrefabs;
    public GameObject enemyParent;
    public float spawnTimer;
    public int enemyDamage;

    [Header("Gun Attributes")]
    public GameObject bulletParent;
    public float mgBulletForce;
    public float rocketSpeed;
    public GameObject explosionPrefab;

    [Header("Stats")]
    public float kills;
    public float shots;
    public float rockets;

    [Header("Explosive Force")]
    public float bulletExpForce;
    public float bulletExpRadius;
    public float bulletUpwardMod;

    [Header("Other")]
    public GameObject player;
    public GameObject bloodPrefab;
    public bool isPaused;

    void Start()
    {
        isPaused = false;
    }



}
