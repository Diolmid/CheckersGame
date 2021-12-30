using UnityEngine;

public class SelectPiece : MonoBehaviour
{
    public Piece selectedPiece;

    public Vector2 startMovePosition;
    public Vector2 endMovePosition;
    
    private MouseOverBoard _mouseOver;
    private Board _board;
    private MovePiece _movePiece;

    private void Awake()
    {
        _mouseOver = GetComponent<MouseOverBoard>();
        _board = Board.instance;
        _movePiece = GetComponent<MovePiece>();
    }

    void Update()
    {
        int mousePositionX = (int)_mouseOver.mouseOverPosition.x;
        int mousePositionY = (int)_mouseOver.mouseOverPosition.y;

        if(selectedPiece != null)
            UpdatePieceDragPosition();
        
        if(Input.GetMouseButtonDown(0) && selectedPiece == null)
            SelectPieceOnBoard(mousePositionX, mousePositionY);
        else if (Input.GetMouseButtonDown(0) && selectedPiece != null)
        {
            endMovePosition = _mouseOver.mouseOverPosition;
            _movePiece.TryMoveSelectedPiece(ref selectedPiece, ref startMovePosition, endMovePosition);
        }
    }

    private void SelectPieceOnBoard(int x, int y)
    {
        if(x < 0 || x > _board.pieces.GetLength(0) - 1 || y < 0 || y > _board.pieces.GetLength(1) - 1)
            return;

        Piece piece = _board.pieces[x, y];
        if (piece != null && piece.isWhite == TurnManager.instance.isWhitePlayer)
        {
            if (_board.forcePieces.Count == 0)
            {
                selectedPiece = piece;
                startMovePosition = _mouseOver.mouseOverPosition;
            }
            else
            {
                if(_board.forcePieces.Find(forcePiece => forcePiece == piece) == null)
                    return;

                selectedPiece = piece;
                startMovePosition = _mouseOver.mouseOverPosition;
            }
        }
    }
    
    private void UpdatePieceDragPosition()
    {
        selectedPiece.transform.position = new Vector3(_mouseOver.mouseOverPosition.x - 3.5f, 1, _mouseOver.mouseOverPosition.y - 3.5f);
    }
}
