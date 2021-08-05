using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShots : MonoBehaviour
{
    public string screenshot1 = "screenshot1";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ScreenCapture.CaptureScreenshot(screenshot1);
        }
    }
}
