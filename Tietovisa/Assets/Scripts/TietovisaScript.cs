using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class TietovisaScript : MonoBehaviour
{
    public TMP_Text txtQuestion;
    public Button btnAnswer1;
    public Button btnAnswer2;
    public Button btnAnswer3;
    public Button btnAnswer4;
    public TimerScript TimerScript;
    public TMP_Text txtScore;
    public int score;
    public int percent;
    public int questionsNumber;

    List<Question> listQuestions; // create a list
    int currentIndex = 0; // number of the current question

    public class Question // create a class Question with properties
    {
        public string question { get; set; }
        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public string answer4 { get; set; }
        public string correctAnswer {get; set; }
    }

    string CleanAnswer(string answer) // find a correct answer with a flag "-correct", delete the flag
    {
        if (answer.EndsWith("-correct"))
        {
            return answer.Replace("-correct", "");
        }
        return answer;
    }

    void Start()
    {
        LoadQuestions(); // loads all questions from the .csv file
        ShowQuestion(currentIndex); // shows the first question
    }

    // shuffle the questions
    void ShuffleQuestions()
    {
        for (int i = 0; i < listQuestions.Count; i++)
        {
            // pick a random index from i to end 
            int randIndex = UnityEngine.Random.Range(i, listQuestions.Count);
            Question temp = listQuestions[i];
            listQuestions[i] = listQuestions[randIndex];
            listQuestions[randIndex] = temp;
        }
    }

    void LoadQuestions()
    {
        string fileName = "Kysymykset.csv";
        string path = Path.Combine(Application.dataPath, fileName);

        if (!File.Exists(path))
        {
            Debug.Log("No save file found at " + path);
            return;
        }

        string row = "";

        // total number of questions
        string[] lines = File.ReadAllLines(path); 
        questionsNumber = lines.Length;
        
        listQuestions = new List<Question>(); // create a new empty list of questions

        StreamReader file = new StreamReader(path); // read the file
        
        // iterate every row in the file
        while ((row = file.ReadLine()) != null)
        {
            try
            {
                string[] fields = row.Split(';'); // divide every row by ";"
                
                Question q = new Question(); // creates the Question object
                q.question = fields[0];
                // insert Question object to the list
                q.answer1 = CleanAnswer(fields[1]);
                q.answer2 = CleanAnswer(fields[2]);
                q.answer3 = CleanAnswer(fields[3]);
                q.answer4 = CleanAnswer(fields[4]);
                
                // look for a correct answer and remove the suffix -correct
                foreach (string f in fields)
                {
                    if (f.Contains("-correct"))
                    {
                        q.correctAnswer = f.Replace("-correct", "");
                    }
                }
                listQuestions.Add(q); // add questions into the questions list
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message.ToString());
            }
        }

        file.Close(); // close the file
        
        ShuffleQuestions();
        // clean all text fields
        txtQuestion.text = ""; 
        btnAnswer1.GetComponentInChildren<TMP_Text>().text = "";
        btnAnswer2.GetComponentInChildren<TMP_Text>().text = "";
        btnAnswer3.GetComponentInChildren<TMP_Text>().text = "";
        btnAnswer4.GetComponentInChildren<TMP_Text>().text = "";
    }

    void ShowQuestion(int index) // show the question with the current index
    {
        if (index < 0 || index >= listQuestions.Count) return;
	
            Question x = listQuestions[index];
            
            // shows the question and answers on the txt and buttons
            txtQuestion.text = x.question;	
            btnAnswer1.GetComponentInChildren<TMP_Text>().text = x.answer1;
            btnAnswer2.GetComponentInChildren<TMP_Text>().text = x.answer2;
            btnAnswer3.GetComponentInChildren<TMP_Text>().text = x.answer3;
            btnAnswer4.GetComponentInChildren<TMP_Text>().text = x.answer4;
    }

    public void NewQuestion() // show the new question
    {
        currentIndex++;
        // if there is no questions anymore save the score and percent, show the next scene
        if (currentIndex >= listQuestions.Count)
        {
            currentIndex = 0;
            percent = Mathf.RoundToInt(((float)score / questionsNumber) * 100f);
            PlayerPrefs.SetInt("PlayerScore", score);
            PlayerPrefs.SetInt("PlayerPercent", percent);
            SceneManager.LoadScene("FinalResult");
        }
        ShowQuestion(currentIndex);
        // set the timer to 0
        if (TimerScript != null)
        {
            TimerScript.currentTime = TimerScript.startingTime;
        }
    }

    public void AnswerQuestion(int index)
    {
        if (currentIndex < 0 || currentIndex >= listQuestions.Count) return;

        Question currentQ = listQuestions[currentIndex];

        string selectedAnswer = "";

        // get the text of the chosen button
        switch (index) 
        {
            case 1: selectedAnswer = currentQ.answer1; break;
            case 2: selectedAnswer = currentQ.answer2; break;
            case 3: selectedAnswer = currentQ.answer3; break;
            case 4: selectedAnswer = currentQ.answer4; break;
        }
        
        // check if it is correct
        if (selectedAnswer == currentQ.correctAnswer)
        {
            score += 1;
            if (txtScore != null) 
            {
                txtScore.text = "Your score: " + score;
                Debug.Log("The answer is correct");
            }
        } 
        else 
        {
            Debug.Log("The answer is uncorrect. The correct answer is " + currentQ.correctAnswer);
        }

        NewQuestion();
    }
}

// laadi aliohjelma joka näyttää yhden satunnaisen kysymyksen listastasi