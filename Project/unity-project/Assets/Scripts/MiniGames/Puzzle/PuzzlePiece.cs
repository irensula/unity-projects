using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform correctSlot;
    private RectTransform rectTransform;
    private Canvas canvas;
    private bool isPlaced = false;
    private Vector2 startPosition;
    public PuzzleManager puzzleManager;

    // start position of puzzle piece
    void Start()
    {
        startPosition = rectTransform.anchoredPosition;
    }

    // get components
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlaced) return;

        SoundEvents.Puzzle.Pickup();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlaced) return;

        float distance = Vector2.Distance(rectTransform.position, correctSlot.position);

        if (distance < 80f)
        {
            if(puzzleManager == null) return;                  
            
            rectTransform.position = correctSlot.position;
            isPlaced = true;
            SoundEvents.Puzzle.Drop();
            // check if all pieces are in their places
            puzzleManager.CheckTaskCompleted();
        } 
        else
        {
            StartCoroutine(MoveBack());    
        }
    }

    public bool IsPlaced()
    {
        return isPlaced;
    }

    IEnumerator MoveBack()
    {
        Vector2 start = rectTransform.anchoredPosition;
        float time = 0;

        while (time < 0.2f)
        {
            time += Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.Lerp(start, startPosition, time / 0.2f);
            yield return null;
        }

        rectTransform.anchoredPosition = startPosition;
        SoundEvents.Puzzle.Back();
    }
}
