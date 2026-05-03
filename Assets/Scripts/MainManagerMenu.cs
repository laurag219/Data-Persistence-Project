using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManagerMenu : MonoBehaviour
{
    public static MainManagerMenu Instance;
    public string PlayerName;
    public int BestScore;
    public string BestPlayerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadGameData();

    }

    [System.Serializable]
    class SaveData
    {
        public string PlayerName;
        public int BestScore;
        public string BestPlayerName;
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();
        data.PlayerName = PlayerName;
        data.BestPlayerName = BestPlayerName;
        data.BestScore = BestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            PlayerName = data.PlayerName;
            BestScore = data.BestScore;
            BestPlayerName = data.BestPlayerName;
        }
    }

    public void UpdateBestScore(int newScore, string currentPlayerName)
    {
        if (newScore > BestScore)
        {
            BestScore = newScore;
            BestPlayerName = currentPlayerName; 
            SaveGameData();
        }
    }
}