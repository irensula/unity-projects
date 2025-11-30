using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    public TextMeshProUGUI ghostScoreText;
    public TextMeshProUGUI coinScoreText;
    public TextMeshProUGUI diamondScoreText;

    public int ghostScore = 0;
    public int coinScore = 0;
    public int diamondScore = 0;

    private int totalCoinsInScene = 0;
    private int totalDiamondsInScene = 0;

    public GameObject congratsGhostPanel;
    public GameObject congratsCoinsPanel;
    public GameObject congratsDiamondsPanel;
    public GameObject infoInventoryPanel;
    public GameObject inventoryPanel;
    public GameObject uiInventoryItemPrefab;
    public GameObject currentPanel;
    public Transform inventoryContent;

    private bool allCoinsCollected = false;
    private bool allDiamondsCollected = false;

    public Dictionary<string, Sprite> itemSprites;

    public Sprite mapSprite;
    public Sprite keySprite;
    public Sprite flashlightSprite;
    public Sprite bookSprite;
    public Sprite bagSprite;

    private Queue<GameObject> panelQueue = new Queue<GameObject>();
    public Button nextButton;

    private bool ghostPanelShown = false;
    private bool coinsPanelShown = false;
    private bool diamondsPanelShown = false;

    
    void Awake()
    {
        ResetProgress(); // DELETE IT LATER!!!
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

        allCoinsCollected = false;
        allDiamondsCollected = false;

        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
        
        LoadProgress();
        UpdateUI();
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
        PlayerPrefs.DeleteAll();
        ghostScore = 0;
        coinScore = 0;
        diamondScore = 0;
        allCoinsCollected = false;
        allDiamondsCollected = false;
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
        if (coinScore >= totalCoinsInScene && totalCoinsInScene > 0 && !allCoinsCollected)
        {
            allCoinsCollected = true;
            Debug.Log("All coins were gathered!");
            ShowCongratsPanel(congratsCoinsPanel);
            coinsPanelShown == true;
        }
    }

    public void AddDiamondScore(int amount)
    {
        diamondScore += amount;
        SaveProgress();
        UpdateUI();

        if (diamondScore >= totalDiamondsInScene && totalDiamondsInScene > 0 && !allDiamondsCollected)
        {
            Debug.Log("All diamonds were gathered!");
            GameUIManager.Instance.ShowCongratsPanel(GameUIManager.Instance.congratsDiamondsPanel);
            diamondsPanelShown = true;
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

    public void ShowCongratsPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
            if (ghostPanelShown && coinsPanelShown && diamondsPanelShown)
            {
                StartCoroutine(ShowInventorySequence());
            }
        }
    }

    IEnumerator ShowPanel(GameObject panel)
    {
        currentPanel = panel;
        currentPanel.SetActive(true);
        currentPanel.transform.localScale = Vector3.zero;
        yield return StartCoroutine(AnimatePanelBounce(currentPanel));

        bool nextPressed = false;
        void OnNext() => nextPressed = true;

        nextButton.onClick.AddListener(OnNext);
        yield return new WaitUntil(() => nextPressed);
        nextButton.onClick.RemoveListener(OnNext);

        currentPanel.SetActive(false);
        currentPanel = null;
    }

    public void OnNextButtonPressed()
    {
        if (currentPanel != null)
            currentPanel.SetActive(false);

        Time.timeScale = 1f;

        currentPanel = null;
        
    }    

    void ShowInventory()
    {        
        Debug.Log("📦 ShowInventory CALLED");
    if (inventoryPanel != null && infoInventoryPanel != null)
    {
        Debug.Log("📦 Panels are NOT null");
        StartCoroutine(ShowInventorySequence());
    }
    else
    {
        Debug.LogError("❌ Inventory panels are NULL!");
    }
    }

    IEnumerator ShowInventorySequence()
    {
        if (infoInventoryPanel == null || inventoryPanel == null)
        {
            Debug.LogError("Inventory panels are not assigned!");
            yield break;
        }
        // show info inventory panel
        infoInventoryPanel.SetActive(true); // show info about inventory panel
        yield return StartCoroutine(AnimatePanelBounce(infoInventoryPanel)); // animation effect for the infoInventoryPanel
        // yield return new WaitForSecondsRealtime(5f); // infoInventory panel is shown for 5 sec

        infoInventoryPanel.SetActive(false); // hide infoInventoryPanel 
        // show inventory panel
        inventoryPanel.SetActive(true); // show nventory panel
       
        currentPanel = inventoryPanel;
        yield return StartCoroutine(AnimatePanelBounce(inventoryPanel)); // animation effect for the inventoryPanel
        
        // spawn inventory items in the scene
        InventorySpawner spawner = FindObjectOfType<InventorySpawner>();
        if (spawner != null)
        {
            spawner.SpawnInventoryItems();
        } 
        // make UI elements for all gathered inventory items
        if (GameManager.Instance != null && GameManager.Instance.inventory != null)
    {
        foreach (string itemId in GameManager.Instance.inventory)
        {
            if (itemSprites.TryGetValue(itemId, out Sprite sprite))
            {
                GameObject uiItem = Instantiate(uiInventoryItemPrefab, inventoryContent);
                Debug.Log("inventoryContent = " + inventoryContent);
                UIInventoryItem uiItemScript = uiItem.GetComponent<UIInventoryItem>();
                if (uiItemScript != null)
                    uiItemScript.Setup(sprite);
            }
        }
    }
        Time.timeScale = 1f;
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

    
}
