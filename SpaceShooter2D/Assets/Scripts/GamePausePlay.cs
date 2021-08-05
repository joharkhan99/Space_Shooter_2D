using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePausePlay : MonoBehaviour
{
    bool isPaused = false;
    public Sprite Pause, Play;
    public GameObject ExitButton;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetComponent<UnityEngine.UI.Image>().sprite = Pause;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            this.transform.GetComponent<UnityEngine.UI.Image>().sprite = Pause;
            ExitButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            this.transform.GetComponent<UnityEngine.UI.Image>().sprite = Play;
            ExitButton.SetActive(true);
        }
    }

}
