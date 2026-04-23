using UnityEngine;
using System.Collections;

public class TaskCompleted : MonoBehaviour
{
    public GameObject taskCompletedPanel;
    public GameObject taskPanel;
    public AudioClip taskCompletedClip;

    public IEnumerator ShowTaskCompletedPanel()
    {
        yield return new WaitForSeconds(1.5f);

        taskPanel.SetActive(false);
        taskCompletedPanel.SetActive(true);
        
        SoundEvents.UI.TaskCompleted();
    }

    public void CloseTaskCompletedPanel()
    {
        taskCompletedPanel.SetActive(false);
    }
}
