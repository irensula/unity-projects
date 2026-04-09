using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemId;
    public Sprite itemSprite;
    public GameUIManager.MiniGameType miniGameToShow;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddItem(itemId);
           
            GameUIManager.Instance.AddItemToUI(itemSprite);

            GameUIManager.Instance.ShowMiniGame(miniGameToShow); // show game corresponding with item

            Destroy(gameObject);
        }
    }
}
