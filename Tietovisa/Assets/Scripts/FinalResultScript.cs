using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class FinalResultScript : MonoBehaviour
{
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtPercent;
    public TMP_InputField inputName;
    public TMP_Text txtReadName;
    public TMP_Text txtReadScore;
    public TMP_Text txtReadPercent;
    public TMP_Text txtReadTime;
    

    public int score;    
    public int percent;

    List<Person> listPersons;

    void Start() 
    {
        score = PlayerPrefs.GetInt("PlayerScore"); // get the score from the previous scene
        percent = PlayerPrefs.GetInt("PlayerPercent"); // get the percent from the previous scene
        txtScore.text = "Your score: " + score;
        txtPercent.text = "Percent of correct answers: " + percent;
    }

    public void SaveResult() 
    {
        string fileName = "Data.csv";
        string path = "Assets/";
        string fullPath = path + fileName;
        string name = inputName.text;
        string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        using (StreamWriter writer = new StreamWriter(fullPath, true))
        {
            string row = name + ";" + score + ";" + percent + ";" + time;
            writer.WriteLine(row);
            Debug.Log(row);
        }

        inputName.text = "";
    }

    public class Person
    {
        public string name { get; set; }
        public string time { get; set; }
        public string score { get; set; }
        public string percent { get; set; }
    }

    public void ReadData()
    {
        string fileName = "Data.csv";
        string path = "Assets/";
        string row = "";
        int rows = 0; 

        listPersons = new List<Person>();

        StreamReader file = new StreamReader(path + fileName);
        Debug.Log("Saving file to: " + path + fileName);

        while ((row = file.ReadLine()) != null)
        {
            rows++;
            Debug.Log(rows + "\t" + row);

            try
            {
                string[] fields = row.Split(';');
                Person p = new Person();
                
                p.name = fields[0];
                p.score = fields[1];
                p.percent = fields[2];
                p.time = fields[3];
                
                listPersons.Add(p);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message.ToString());
            }
        }

        file.Close();

        listPersons.Sort((x, y) => y.score.CompareTo(x.score));
        int topCount = Mathf.Min(10, listPersons.Count);

        txtReadName.text = ""; 
        txtReadScore.text = "";
        txtReadPercent.text = "";
        txtReadTime.text = "";

        for (int i = 0; i < topCount; i++) 
        {
            Person x = listPersons[i];
            Debug.Log(i + "\n" + x.name);

            txtReadName.text += x.name + "\n";
            txtReadScore.text += x.score + "\n";
            txtReadPercent.text += x.percent + "\n";
            txtReadTime.text += x.time + "\n";
        }
    }
    public void SaveAndShowResults() 
    {
        SaveResult();
        ReadData();
    }
}
