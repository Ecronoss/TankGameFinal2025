using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;
    public Toggle splitScreenToggle;
    public TMP_Dropdown levelSizeDropdown;
    public TMP_Dropdown levelModeDropdown;
    public TMP_InputField seedNum;
    public LevelGenerator levelGen;
    public int currentLevelMode;

    //Run when game object is enabled
    void OnEneable()
    {
        //Set our volume sliders
        float temp;
        audioMixer.GetFloat("VolumeMaster", out temp);
        masterSlider.value = temp;
        audioMixer.GetFloat("VolumeMusic", out temp);
        musicSlider.value = temp;
        audioMixer.GetFloat("VolumeSFX", out temp);
        SFXSlider.value = temp;
    }
    public void BackButton()
    {
        GameManager.gameInstance.ActivateMainMenu();
    }

    public void OnChangeMasterVolume()
    {
        //Change Master Volume in mixer based on slider
        audioMixer.SetFloat("VolumeMaster", masterSlider.value);
    }

    public void OnChangeMuiscVolume()
    {
        //Change Music Volume in mixer based on slider
        audioMixer.SetFloat("VolumeMusic", musicSlider.value);
    }

    public void OnChangeSFXVolume()
    {
        //Change SFX Volume in mixer based on slider
        audioMixer.SetFloat("VolumeSFX", SFXSlider.value);
    }

    public void OnToggleSplitScreen()
    {
        // Set Game Manager variable
        GameManager.gameInstance.isSplitScreen = splitScreenToggle.isOn;
    }

    public void LevelSizeDrop()
    {
        int currentEntry = levelSizeDropdown.value;

        if (currentEntry == 0)
        {
            levelGen.numRows = 3;
            levelGen.numCols = 3;
            GameManager.gameInstance.timeClock = 300;
            currentLevelMode = 0;
        }
        if (currentEntry == 1)
        {
            levelGen.numRows = 4;
            levelGen.numCols = 4;
            GameManager.gameInstance.timeClock = 600;
            currentLevelMode = 1;
        }
        if (currentEntry == 2)
        {
            levelGen.numRows = 6;
            levelGen.numCols = 6;
            GameManager.gameInstance.timeClock = 900;
            currentLevelMode = 2;
        }
        if (currentEntry == 3)
        {
            levelGen.numRows = 2;
            levelGen.numCols = 10;
            GameManager.gameInstance.timeClock = 600;
            currentLevelMode = 3;
        }
    }

    public void LevelModeDrop()
    {
        int currentEntry = levelModeDropdown.value;

        if (currentEntry == 0)
        {
            levelGen.randomType = LevelGenerator.levelRandomType.Random;
            levelSizeDropdown.interactable = true;
            seedNum.interactable = false;
            ResetClockStat();
        }
        if (currentEntry == 1)
        {
            levelGen.randomType = LevelGenerator.levelRandomType.MapOfDay;
            levelSizeDropdown.interactable = true;
            seedNum.interactable = false;
            ResetClockStat();
        }
        if (currentEntry == 2)
        {
            levelGen.randomType = LevelGenerator.levelRandomType.Seeded;
            levelSizeDropdown.interactable = true;
            seedNum.interactable = true;
            ResetClockStat();
        }
        if (currentEntry == 3)
        {
            levelGen.randomType = LevelGenerator.levelRandomType.Gauntlet;
            levelSizeDropdown.interactable = false;
            seedNum.interactable = false;
            GameManager.gameInstance.timeClock = 900;
            GameManager.gameInstance.gauntletLevel = 1;
        }
    }

    public void SeedField()
    {
        string rawInput = seedNum.text;
        int inputInt = int.Parse(rawInput);
        levelGen.levelSeed = inputInt;
    }

    public void ResetClockStat()
    {
        if (currentLevelMode == 0)
        {
            GameManager.gameInstance.timeClock = 300;
        }
        if (currentLevelMode == 1)
        {
            GameManager.gameInstance.timeClock = 600;
        }
        if (currentLevelMode == 2)
        {
            GameManager.gameInstance.timeClock = 900;
        }
        if (currentLevelMode == 3)
        {
            GameManager.gameInstance.timeClock = 600;
        }
    }
}
