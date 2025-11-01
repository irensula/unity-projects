using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public TMP_Text txtReadName;
    public TMP_Text txtReadScore;
    public TMP_Text txtReadDate;

    List<Person> listPersons;

    void Start() 
    {
        scoreText.text = "Score: " + GameData.playerScore;
        SaveData();
        ReadeData();
    }

    public void StartGame()
    {
        GameData.playerScore = 0;
        GameData.playerHealth = 5;
        GameData.currentLevel = 1;
        GameData.playerSpeed = 5f;
        GameData.invaderSpeed = 2f;
        GameData.spawnInterval = 3f;

        SceneManager.LoadScene("StartScene");
    } 

    public void SaveData()
    {
        string fileName = "Data.csv";
        string path = "Assets/";
        string name = GameData.playerName;
        int score = GameData.playerScore;
        string date = System.DateTime.Now.ToString("dd.MM.yyyy");
        StreamWriter writer = new StreamWriter(path + fileName, true);
        string row = score + ";" + name + ";" + date;
        writer.WriteLine(row);
        writer.Close();
    }

    public class Person 
    {
        public string name { get; set; }
        public string score { get; set; }
        public string date { get; set; }
    }

    public void ReadeData()
    {
        string fileName = "Data.csv";
        string path = "Assets/";
        string row = "";
        int rows = 0;

        listPersons = new List<Person>();

        StreamReader file = new StreamReader(path + fileName);
        while ((row = file.ReadLine()) != null)
        {
            rows++;

            try
            {
                string[] fields = row.Split(';');
                
                Person p = new Person();
                
                p.score = fields[0];
                p.name = fields[1];
                p.date = fields[2];
                
                listPersons.Add(p);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message.ToString());
            }
    }
    file.Close();

    listPersons.Sort((x,y) => int.Parse(y.score).CompareTo(int.Parse(x.score)));

    txtReadScore.text = "";
    txtReadName.text = "";
    txtReadDate.text = "";

    for (int i = 0; i < listPersons.Count; i++) {
        Person x = listPersons[i];
        txtReadScore.text += x.score + "\n";
        txtReadName.text += x.name + "\n";
        txtReadDate.text += x.date + "\n";
    }
    }
}