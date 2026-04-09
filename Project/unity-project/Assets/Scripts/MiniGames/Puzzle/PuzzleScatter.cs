using UnityEngine;

public class PuzzleScatter : MonoBehaviour
{
    public RectTransform leftPanel;
    public RectTransform rightPanel;
    
    public RectTransform[] leftPieces;
    public RectTransform[] rightPices;

    public float padding = 20f;
    void OnEnable()
    {
        ScatterPieces(leftPanel, leftPieces);
        ScatterPieces(rightPanel, rightPices);
    }

    void ScatterPieces(RectTransform panel, RectTransform[] pieces)
    {
        // get width and height of the panel
        float panelWidth = panel.rect.width;
        float panelHeight = panel.rect.height;

        // piece is a current RectTransform of the puzzle piece
        foreach (var piece in pieces)
        {
            // count half of width and half of height of the piece
            float pieceHalfWidth = piece.rect.width / 2;
            float pieceHalfHeight = piece.rect.height / 2;
            
            // get a random position of the piece center, Random.Range(a, b)
        
            float x = Random.Range(-panelWidth/2 + padding + pieceHalfWidth, panelWidth/2 - padding - pieceHalfWidth);
            float y = Random.Range(-panelHeight/2 + padding + pieceHalfHeight, panelHeight/2 - padding - pieceHalfHeight);
            
            piece.SetParent(panel); // set the parent panel to the piece
            piece.localScale = Vector3.one; // set scale of the piece 1,1,1, so its size will not be changed

            piece.localPosition = new Vector3(x, y, 0); // set the local position inside the panel
        }
    }
}
