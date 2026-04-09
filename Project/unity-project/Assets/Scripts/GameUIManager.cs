using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO.Compression;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    public TextMeshProUGUI ghostScoreText;
    public TextMeshProUGUI coinScoreText;
    public TextMeshProUGUI diamondScoreText;
    public Button nextButton;

    public int ghostScore = 0;
    public int coinScore = 0;
    public int diamondScore = 0;

    public int totalCoinsInScene = 0;
    public int totalDiamondsInScene = 0;

    public GameObject congratsGhostPanel;
    public GameObject congratsCoinsPanel;
    public GameObject congratsDiamondsPanel;
    public GameObject infoInventoryPanel;
    public GameObject inventoryPanel;
    public GameObject uiInventoryItemPrefab;
    public Transform inventoryContent; 

    public Dictionary<string, Sprite> itemSprites;

    public Sprite mapSprite;
    public Sprite keySprite;
    public Sprite flashlightSprite;
    public Sprite bookSprite;
    public Sprite bagSprite;

    private bool coinPanelShown = false;
    private bool diamondPanelShown = false;
    public bool ghostPanelShown = false;

    private GameObject currentPanel = null;

    public GameObject taskPanel;

    public GameObject[] miniGamePanels;

    public enum MiniGameType { Puzzle, Memo, Game3, Game4, Game5 };
    
    void Awake()
    {
        ResetProgress();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        itemSprites = new Dictionary<string, Sprite>();

        itemSprites.Add("map", mapSprite);
        itemSprites.Add("flashlight", flashlightSprite);
        itemSprites.Add("key", keySprite);
        itemSprites.Add("book", bookSprite);
        itemSprites.Add("bag", bagSprite);
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ghostScoreText = GameObject.Find("ghostScoreText")?.GetComponent<TextMeshProUGUI>();
        coinScoreText = GameObject.Find("coinScoreText")?.GetComponent<TextMeshProUGUI>();
        diamondScoreText = GameObject.Find("diamondScoreText")?.GetComponent<TextMeshProUGUI>();

        totalCoinsInScene = GameObject.FindGameObjectsWithTag("Coin").Length;
        totalDiamondsInScene = GameObject.FindGameObjectsWithTag("Diamond").Length;
        Debug.Log("Total coins in this scene: " + totalCoinsInScene + ". Total diamonds in this scene: " + totalDiamondsInScene);

        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
        
        LoadProgress();
        UpdateUI();
    }

    public void AddGhostScore(int amount)
    {
        ghostScore += amount;
        SaveProgress();
        UpdateUI();
    }

    public void AddCoinScore(int amount)
    {
        coinScore += amount;
        SaveProgress();
        UpdateUI();
        if (coinScore >= totalCoinsInScene && totalCoinsInScene > 0 )
        {
            Debug.Log("All coins were gathered!");
            ShowCongratsPanel(congratsCoinsPanel);
            coinPanelShown = true;
        }
    }

    public void AddDiamondScore(int amount)
    {
        diamondScore += amount;
        SaveProgress();
        UpdateUI();

        if (diamondScore >= totalDiamondsInScene && totalDiamondsInScene > 0)
        {
            Debug.Log("All diamonds were gathered!");
            GameUIManager.Instance.ShowCongratsPanel(GameUIManager.Instance.congratsDiamondsPanel);
            diamondPanelShown = true;
        }
    }

    void UpdateUI()
    {
        if (ghostScoreText != null)
            ghostScoreText.text = ghostScore.ToString();
        if (coinScoreText != null)
            coinScoreText.text = coinScore.ToString();
        if (diamondScoreText !=null)
            diamondScoreText.text = diamondScore.ToString();
    }

    public void ShowCongratsPanel(GameObject panel)
    {
        if (panel != null)
        {
            currentPanel = panel;
            panel.SetActive(true);
            Time.timeScale = 0f;
            panel.transform.localScale = Vector3.zero;
            StartCoroutine(AnimatePanelBounce(panel));
        }
    }

    public void OnNextButtonPressed() 
    { 
        if (currentPanel != null)             

            if (currentPanel == congratsGhostPanel) ghostPanelShown = true;
            if (currentPanel == congratsCoinsPanel) coinPanelShown = true;
            if (currentPanel == congratsDiamondsPanel) diamondPanelShown = true;
    
            currentPanel.SetActive(false);
            currentPanel = null; 
            Time.timeScale = 1f;

            if (ghostPanelShown && coinPanelShown && diamondPanelShown)
            {
                ShowInfoInventoryPanel();
            }
    }

    void ShowInfoInventoryPanel()
    {
        infoInventoryPanel.SetActive(true);
        Time.timeScale = 0f;
        currentPanel = infoInventoryPanel;
        StartCoroutine(AnimatePanelBounce(infoInventoryPanel));
    }

    public void OnInfoInventoryNextButtonPressed()
    { 
        ShowInventoryPanel();
    }

    IEnumerator AnimatePanelBounce(GameObject panel)
    {
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float scale = Mathf.Sin((elapsed / duration) * Mathf.PI);
            panel.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        panel.transform.localScale = Vector3.one;
    }

    void ShowInventoryPanel()
    {
        // show info inventory panel
        infoInventoryPanel.SetActive(false); // show info about inventory panel
        inventoryPanel.SetActive(true); // show nventory panel
        Time.timeScale = 1f;
        currentPanel = inventoryPanel;
        StartCoroutine(AnimatePanelBounce(inventoryPanel));
        
        // spawn inventory items in the scene
        InventorySpawner spawner = FindObjectOfType<InventorySpawner>();
        if (spawner != null)
        {
            spawner.SpawnInventoryItems();
        } 
        // make UI elements for all gathered inventory items
        foreach (string itemId in GameManager.Instance.inventory)
        {
            if (itemSprites.TryGetValue(itemId, out Sprite sprite))
            {
                GameObject uiItem = Instantiate(uiInventoryItemPrefab, inventoryContent);
                UIInventoryItem uiItemScript = uiItem.GetComponent<UIInventoryItem>();
                uiItemScript.Setup(sprite);
            }
        }
    }

    public void AddItemToUI(Sprite itemSprite)
    {
        GameObject newItem = Instantiate(uiInventoryItemPrefab, inventoryContent);
        UIInventoryItem uiItem = newItem.GetComponent<UIInventoryItem>();

        if (uiItem != null)
        {
            uiItem.Setup(itemSprite);
        }
    }

    // save progress
    void SaveProgress()
    {
        PlayerPrefs.SetInt("GhostScore", ghostScore);
        PlayerPrefs.SetInt("CoinScore", coinScore);
        PlayerPrefs.SetInt("DiamondScore", diamondScore);
        PlayerPrefs.Save();
    }

    // load progress
    void LoadProgress()
    {
        ghostScore = PlayerPrefs.GetInt("GhostScore", 0);
        coinScore = PlayerPrefs.GetInt("CoinScore", 0);
        diamondScore = PlayerPrefs.GetInt("DiamondScore", 0);
    }

    // clear all saved data (on restarting the game)
    public void ResetProgress()
    {
        ghostScore = 0;
        coinScore = 0;
        diamondScore = 0;

        PlayerPrefs.DeleteKey("GhostScore");
        PlayerPrefs.DeleteKey("CoinScore");
        PlayerPrefs.DeleteKey("DiamondScore");

        UpdateUI();
    }

    // show panel with minigame 
    public void ShowMiniGame(MiniGameType gameType)
    {
        // close all minigame panels
        foreach (var panel in miniGamePanels)
            panel.SetActive(false);

        // show panel with minigame
        int index = (int)gameType;
        if(index >= 0 && index < miniGamePanels.Length)
        {
            miniGamePanels[index].SetActive(true);
            taskPanel.SetActive(true);
        }
    }
}
