using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenshotTaker : MonoBehaviour
{
    public string screenshotDirectory = "Screenshots";
    public string screenshotFileName = "screenshot";
    public KeyCode screenshotKey = KeyCode.P;

    void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenshot();
        }
    }

    void TakeScreenshot()
    {
        string fullPath = System.IO.Path.Combine(Application.dataPath, screenshotDirectory);

        if (!System.IO.Directory.Exists(fullPath))
        {
            System.IO.Directory.CreateDirectory(fullPath);
        }

        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HHmmss");
        string screenshotPath = System.IO.Path.Combine(fullPath, $"{screenshotFileName} {timestamp}.png");

        ScreenCapture.CaptureScreenshot(screenshotPath);
        Debug.Log("Screenshot taken and saved as " + screenshotPath);
    }
}
