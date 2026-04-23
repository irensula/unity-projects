using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public PuzzlePiece[] pieces;
    public TaskCompleted taskCompleted;
    
    private bool isCompleted;

    public void CheckTaskCompleted()
    {
        if (isCompleted) return;
    
        foreach (var piece in pieces)
        {
            if (!piece.IsPlaced())
                return;
        }

        isCompleted = true;
        StartCoroutine(taskCompleted.ShowTaskCompletedPanel());
    }
    
}
