using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemId;
    public Sprite itemSprite;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddItem(itemId);
           
            GameUIManager.Instance.AddItemToUI(itemSprite);
            Debug.Log("Sprite = " + itemSprite);

            Destroy(gameObject);
        }
    }
}
