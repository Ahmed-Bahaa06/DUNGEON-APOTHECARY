using System;
using System.IO;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private string savePath;
    private SaveData saveData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        savePath = Path.Combine(Application.persistentDataPath, "save.json" );

        Load();
    }

    public int GetHighScore()
    {
        return saveData.highScore;
    }

    public float GetSFXVolume()
    {
        return saveData.sfxVolume;
    }

    public float GetMusicVolume()
    {
        return saveData.musicVolume;
    }

    public void SetHighScore(int highScore)
    {
        saveData.highScore = highScore;
        Save();
    }

    public void SetSFXVolume(float volume)
    {
        saveData.sfxVolume = volume;
        Save();
    }

    public void SetMusicVolume(float volume)
    {
        saveData.musicVolume = volume;
        Save();
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
    }

    private void Load()
    {
        if (!File.Exists(savePath))
        {
            saveData = new SaveData();
            return;
        }

        string json = File.ReadAllText(savePath);

        saveData = JsonUtility.FromJson<SaveData>(json);
    }
}

[Serializable]
public class SaveData
{
    public int highScore = 0;

    public float sfxVolume = 1f;
    public float musicVolume = 1f;
}