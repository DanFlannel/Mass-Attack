using UnityEngine;
using System.Collections;

public class playerControls : MonoBehaviour {


    public float playerSpeed;
    public float jumpHeight;
    private bool isJumping;

    public float health;

    private Vector3 targetPos;

    private AudioSource aSource;
    private MachineGun machineGun;
    private RocketLauncher rocketLauncher;
    private Vector3 mousePos;

    private DevLog devLog;
    private GameMaster gm;

    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag(customTags.GameMaster).GetComponent<GameMaster>();
        devLog = GameObject.Find(customStrings.devConsole).GetComponent<DevLog>();
        gm.player = this.gameObject;
    }

    void Start()
    {
        rocketLauncher = this.GetComponent<RocketLauncher>();
        machineGun = this.GetComponent<MachineGun>();
        aSource = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!gm.isPaused)
        {
            Move();
            Rotate();

            shootControls();
        }
	}

    void FixedUpdate()
    {
        if (!gm.isPaused)
        {
            UpdateMousePosition();
            Jump();
        }
    }

    private void Move()
    {
        targetPos = new Vector3(this.transform.position.x + Input.GetAxis("Horizontal"), this.transform.position.y, this.transform.position.z + Input.GetAxis("Vertical"));
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, playerSpeed);
    }

    private void Rotate()
    {
        Vector3 relativePos = targetPos - this.transform.position;
        if (relativePos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            //Debug.Log("PressedSpace");
            Rigidbody rigid = this.GetComponent<Rigidbody>();
            Vector3 jumpForce = new Vector3(0, jumpHeight, 0);
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, jumpHeight, 0);

            devLog.WriteConsole("Player Jumping");

            isJumping = true;
        }
    }

    private void UpdateMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            mousePos = hit.point;  
        }

        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void shootControls()
    {
        if (Input.GetMouseButton(0))
        {
            
            machineGun.Shoot(aSource, mousePos);
        }

        if (Input.GetMouseButtonDown(1))
        {
            rocketLauncher.Shoot(aSource, mousePos);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == customTags.Ground)
        {

            isJumping = false;
        }
    }

    public void applyDamage(int dmg)
    {
        health -= dmg;

        if(health <= 0)
        {
            GameObject death = Instantiate(gm.bloodPrefab, this.transform.position, Quaternion.identity) as GameObject;
            death.transform.parent = gm.transform;
            this.GetComponent<Rigidbody>().AddExplosionForce(gm.bulletExpForce * 50f, this.transform.position, gm.bulletExpRadius * 50f, gm.bulletUpwardMod * 50f, ForceMode.Impulse);
            Destroy(this.gameObject);
        }
    }
}
