using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    public Image itemImage; // place for the sprite

    public void Setup(Sprite sprite)
    {
        itemImage.sprite = sprite;
        itemImage.enabled = true;
        itemImage.color = Color.white;
    }
}
