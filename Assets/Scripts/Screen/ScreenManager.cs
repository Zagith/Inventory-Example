using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    
    #region Properties

    [SerializeField] List<ResolutionScriptableObject> resolutionsList;

    [SerializeField] TMP_Text resolutionText;

    private Resolution[] resolutions = new Resolution[3];
    private int _currentScreenSize;

    public int CurrentScreenSize
    {
        get { return _currentScreenSize; }
        set { _currentScreenSize = value; }
    }

    #endregion

    #region  Methods
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < resolutionsList.Count; i++)
        {
            Resolution res = new Resolution
            {
                width = resolutionsList[i].Width,
                height = resolutionsList[i].Height,
                refreshRate = resolutionsList[i].FrameRate
            };
            resolutions[i] = res;
        }
        Screen.SetResolution(resolutions[1].width, resolutions[1].height, true);
        UpdateResolutionText(resolutions[1]);
        CurrentScreenSize = 1;
    }

    public void ScreenResize(bool increase = false)
    {
        if (increase)
        {
            switch (CurrentScreenSize)
            {
                case 0:
                    Screen.SetResolution(resolutions[1].width, resolutions[1].height, true);
                    UpdateResolutionText(resolutions[1]);
                    CurrentScreenSize = 1;
                    break;
                case 1:
                    Screen.SetResolution(resolutions[2].width, resolutions[2].height, true);
                    UpdateResolutionText(resolutions[2]);
                    CurrentScreenSize = 2;
                    break;

            }
        }
        else
        {
            switch (CurrentScreenSize)
            {
                case 1:
                    Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);
                    UpdateResolutionText(resolutions[0]);
                    CurrentScreenSize = 0;
                    break;
                case 2:
                    Screen.SetResolution(resolutions[1].width, resolutions[1].height, true);
                    UpdateResolutionText(resolutions[1]);
                    CurrentScreenSize = 1;
                    break;

            }
        }
    }

    void UpdateResolutionText(Resolution resolution)
    {
        resolutionText.text = $"Current Resolution: {resolution.width}x{resolution.height}:{resolution.refreshRate}";
    }

    #endregion
}
