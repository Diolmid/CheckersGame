using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    
    private SelectPiece _selectPiece;

    public TextMeshProUGUI turn;
    
    public bool isWhiteTurn;
    public bool isWhitePlayer;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        _selectPiece = GetComponent<SelectPiece>();
    }

    private void Start()
    {
        isWhiteTurn = true;
    }

    public void EndTurn()
    {
        int y = (int)_selectPiece.endMovePosition.y;

        Piece selectedPiece = _selectPiece.selectedPiece;
        if (selectedPiece != null)
        {
            if (selectedPiece.isWhite && !selectedPiece.isKing && y == 7)
            {
                selectedPiece.isKing = true;
                selectedPiece.transform.Rotate(Vector3.right * 180);
            }
            else if (!selectedPiece.isWhite && !selectedPiece.isKing && y == 0)
            {
                selectedPiece.isKing = true;
                selectedPiece.transform.Rotate(Vector3.right * 180);
            }
        }
        
        _selectPiece.selectedPiece = null;
        _selectPiece.startMovePosition = Vector2.zero;
        
        isWhiteTurn = !isWhiteTurn;
        isWhitePlayer = !isWhitePlayer;
        turn.color = isWhiteTurn ? Color.white : Color.black;
    }
}
