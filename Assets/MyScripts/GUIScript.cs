using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour {

    public Text kills;
    public Text shots;
    public Text rockets;
    public Text Health;
    public GameObject Tutorial;

    public GameMaster gm;

    void Start()
    {
        Time.timeScale = 0;
        gm = GameObject.FindGameObjectWithTag(customTags.GameMaster).GetComponent<GameMaster>();
	}

    void Update()
    {

        DestroyTutorial();
        UpdateUI();
    }

    private void DestroyTutorial()
    {
        if (Tutorial.activeInHierarchy)
        {
            if (Input.anyKey)
            {
                Tutorial.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void UpdateUI()
    {
        kills.text = "Kills: " + gm.kills;
        shots.text = "MG Shots: " + gm.shots;
        rockets.text = "Rockets Fired: " + gm.rockets;
        Health.text =  "Health: " + gm.player.GetComponent<playerControls>().health.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(1);
    }
}
