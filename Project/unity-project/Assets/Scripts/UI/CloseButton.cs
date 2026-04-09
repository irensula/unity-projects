using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public GameObject panel;

    public void Close()
    {
         panel.SetActive(false);
    }
}
