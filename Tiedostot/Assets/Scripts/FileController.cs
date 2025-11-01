using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class FileController : MonoBehaviour
{
    public TMP_InputField inputNimi;
    public TMP_InputField inputOsoite;
    public TMP_InputField inputPuhelinnumero;

    public TMP_Text txtReadOsoite;
    public TMP_Text txtReadNimi;
    public TMP_Text txtReadAika;
    public TMP_Text txtReadPuhelinnumero;
    
    // lista henkilöille
    List<Henkilo> listHenkilot; // generic List from System.Collections.Generic

    public void SaveData() // saves data when user writes inputs and push 'Save' button
    {
        string fileName = "Data.csv";
        string path = "Assets/";
        // haetaan kirjoitettavat tiedot:
        string osoite = inputOsoite.text;
        string nimi = inputNimi.text;
        string puhelinnumero = inputPuhelinnumero.text;
        string aika = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        // luodaan tiedosto
        StreamWriter writer = new StreamWriter(path + fileName, true);
        // kirjoitetaan rivi tekstiä, erotinmerkkinä ;
        string rivi = nimi + ";" + osoite + ";" + puhelinnumero + ";" + aika;
        writer.WriteLine(rivi);
        Debug.Log(rivi);
        writer.Close();

        // empty fields after push Save button
        inputNimi.text = "";
        inputOsoite.text = "";
        inputPuhelinnumero.text = "";
    }

    public class Henkilo 
    {
        public string nimi { get; set; }
        public string osoite { get; set; }
        public string aika { get; set; }
        public string puhelinnumero { get; set; }
    }

    public void ReadData() // shows data when user pushes 'Read file' button
    {
        string fileName = "Data.csv";
        string path = "Assets/";
        string row = "";
        int rows = 0;

        // tyhjennetään Henkilot
        listHenkilot = new List<Henkilo>();
        
        // luetaan tiedosto läpi rivi kerrallaan
        StreamReader file = new StreamReader(path + fileName);
        while ((row = file.ReadLine()) != null)
        {
            rows++;
            Debug.Log(rows + "\t" + row);

            try
            {
                // puretaan rivi ;-merkkien kohdalta
                string[] fields = row.Split(';');
                // esitellään uusi Henkilo
                Henkilo h = new Henkilo();
                // asetetaan kentät
                h.nimi = fields[0];
                h.osoite = fields[1];
                h.puhelinnumero = fields[2];
                h.aika = fields[3];
                // lisätään henkilö listaan
                listHenkilot.Add(h);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message.ToString());
            }
        }

        file.Close();
        
        // järjestetään lista 
        listHenkilot.Sort((x, y) => x.nimi.CompareTo(y.nimi)); 
        // empty placeholders before insert there data from the csv-file
        txtReadNimi.text = "";
        txtReadOsoite.text = "";
        txtReadAika.text = "";
        txtReadPuhelinnumero.text = "";
        // Lopuksi käydään henkilöt läpi ja asetetaan näkyviin: 
        
        for (int i = 0; i < listHenkilot.Count; i++) { 	
            Henkilo x = listHenkilot[i];
            Debug.Log(i + "\t" + x.nimi);
            
            txtReadAika.text += x.aika + "\n";	
            txtReadNimi.text += x.nimi + "\n";
            txtReadOsoite.text += x.osoite + "\n";
            txtReadPuhelinnumero.text += x.puhelinnumero + "\n";
        }  
    }
}
