using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class DataPersistence : MonoBehaviour
{

    [System.Serializable]
    public class PlayerData
    {
        public string Name = "new guy";
        public int Score;
        public string HighScoreName;
        public int HighScore;
    }

    public PlayerData TheGuy;
    public static DataPersistence Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(TheGuy);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void Load()
    {
        Debug.Log(Application.persistentDataPath);
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            TheGuy = JsonUtility.FromJson<PlayerData>(json);
            GameObject.Find("UsersName").GetComponent<InputField>().text = TheGuy.Name;
        }
    }

    public void GetNewName()
    {
        string usersName = GameObject.Find("UsersName").GetComponent<InputField>().text;
        DataPersistence.Instance.TheGuy.Name = usersName;
    }

}
