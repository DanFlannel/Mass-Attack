using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DevLog : MonoBehaviour {

    public GameObject DevConsole;

    public GameObject prefab;
    public GameObject content;

    private bool toggle;
    private GameMaster gm;

    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag(customTags.GameMaster).GetComponent<GameMaster>();
        toggle = false;
        DevConsole.SetActive(false);
    }

	public void WriteConsole(string textToWrite)
    {
        GameObject clone = Instantiate(prefab, content.transform.position, Quaternion.identity) as GameObject;
        
        Text console = clone.GetComponent<Text>();

        float time = Time.realtimeSinceStartup;
        string finalText = string.Format("{00000:0.000}", time);
        finalText += "    " + textToWrite;

        console.text = finalText;
        console.transform.SetParent(content.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }

    public void ToggelConsole()
    {
        toggle = DevConsole.activeInHierarchy;
        toggle = !toggle;
        if (toggle)
        {
            gm.isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            gm.isPaused = false;
            Time.timeScale = 1f;
        }
        DevConsole.SetActive(toggle);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggelConsole();
        }
    }
}
