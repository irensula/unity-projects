using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    private float playerTime;
    
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtTime;

    public TMP_InputField inputName;
    public TMP_Text txtReadName;
    public TMP_Text txtReadTime;
    public TMP_Text txtReadDate;
    
    List<Person> listPersons;

    void Start() 
    {
        int playerScore = PlayerPrefs.GetInt("PlayerScore");
        playerTime = PlayerPrefs.GetFloat("PlayerTime");
        txtScore.text = "Your score: " + playerScore;
        txtTime.text = "Your time: " + Mathf.Round(playerTime);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SaveData()
    {
        string fileName = "Data.csv";
        string path = "Assets/";
        string name = inputName.text;
        string date = System.DateTime.Now.ToString("dd.MM.yyyy");
        StreamWriter writer = new StreamWriter(path + fileName, true);
        string row = name + ";" + Mathf.Round(playerTime) + ";" + date;
        writer.WriteLine(row);
        Debug.Log(row);
        writer.Close();

        inputName.text = "";
    }

    public class Person 
    {
        public string name { get; set; }
        public string time { get; set; }
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
            Debug.Log(rows + "\t" + row);

            try
            {
                
                string[] fields = row.Split(';');
                
                Person p = new Person();
                
                p.name = fields[0];
                p.time = fields[1];
                p.date = fields[2];
                
                listPersons.Add(p);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message.ToString());
            }
    }
    file.Close();

    listPersons.Sort((x,y) => float.Parse(x.time).CompareTo(float.Parse(y.time)));

    txtReadName.text = "";
    txtReadTime.text = "";
    txtReadDate.text = "";

    for (int i = 0; i < listPersons.Count; i++) {
        Person x = listPersons[i];
        Debug.Log(i + "\t" + x.name);
        txtReadName.text += x.name + "\n";
        txtReadTime.text += x.time + "\n";
        txtReadDate.text += x.date + "\n";
    }
    }
}
