using UnityEngine;

public class TaskCompleted : MonoBehaviour
{
    public GameObject taskCompletedPanel;
    public GameObject taskPanel;
    protected IEnumerator ShowWinPanel()
    {
        yield return new WaitForSeconds(1.5f);
        taskPanel.SetActive(false);
        taskCompletedPanel.SetActive(true);

        //Win sound
    }

    public void CloseWinPanel()
    {
        taskCompletedPanel.SetActive(false);
    }
}
