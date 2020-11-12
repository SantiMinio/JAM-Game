using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public const string FileName = "SaveData";
    SaveData data;
    [SerializeField] LevelSelector[] myLevels = new LevelSelector[0];
    [SerializeField] AudioMixer mixerMaster = null;

    private void Start()
    {
        if (BinarySerialization.IsFileExist(FileName)) data = BinarySerialization.Deserialize<SaveData>(FileName);
        else
        {
            data = new SaveData();
            BinarySerialization.Serialize(FileName, data);
        }

        SettingsData settings = new SettingsData();

        if (BinarySerialization.IsFileExist(Settings.SettingsDataName)) settings = BinarySerialization.Deserialize<SettingsData>(Settings.SettingsDataName);
        else
        {
            settings.resolutionWidht = Screen.currentResolution.width;
            settings.resolutionHeight = Screen.currentResolution.height;
            BinarySerialization.Serialize(Settings.SettingsDataName, settings);
        }

        mixerMaster.SetFloat("Volume", settings.volume);
        Screen.SetResolution(settings.resolutionWidht, settings.resolutionHeight, settings.fullScreen);
        QualitySettings.SetQualityLevel(settings.qualityIndex);
    }

    public void CompleteLevelSelector()
    {
        myLevels[0].BlockLevel(false);
        myLevels[0].RefreshStars(data.starsPerLevel[0]);

        for (int i = 1; i < myLevels.Length; i++)
        {
            myLevels[i].BlockLevel(!data.levelsClear[i - 1]);
            myLevels[i].RefreshStars(data.starsPerLevel[i]);
        }
    }
}
