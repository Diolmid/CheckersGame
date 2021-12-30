using UnityEngine;

public class MovePiece : MonoBehaviour
{
    public void TryMoveSelectedPiece(ref Piece selectedPiece, ref Vector2 startPosition, Vector2 endPosition)
    {
        bool hasKil = false;
        
        int startPositionX = (int)startPosition.x;
        int startPositionY = (int)startPosition.y;
        int endPositionX = (int)endPosition.x;
        int endPositionY = (int)endPosition.y;

        Board board = Board.instance;
        board.ScanForPossibleMove();
        Piece[,] pieces = board.pieces;
        
        if (endPositionX is < 0 or > 8 || endPositionY is < 0 or > 8)
        {
            if(selectedPiece != null)
                MoveSelectedPiece(selectedPiece, startPositionX, startPositionY);
            
            startPosition = Vector2.zero;
            selectedPiece = null;
            return;
        }
        
        if (selectedPiece != null)
        {
            if ((startPositionX, startPositionY) == (endPositionX, endPositionY))
            {
                MoveSelectedPiece(selectedPiece, startPositionX, startPositionY);
                startPosition = Vector2.zero;
                selectedPiece = null;
                return;
            }

            if (selectedPiece.ValidMove(pieces, startPositionX, startPositionY, endPositionX, endPositionY))
            {
                if (Mathf.Abs(startPositionX - endPositionX) == 2)
                {
                    Piece piece = pieces[(startPositionX + endPositionX) / 2, (startPositionY + endPositionY) / 2];
                    if (piece != null)
                    {
                        pieces[(startPositionX + endPositionX) / 2, (startPositionY + endPositionY) / 2] = null;
                        Destroy(piece.gameObject);
                        hasKil = true;
                    }
                }
                
                if (board.forcePieces.Count != 0 && !hasKil)
                {
                    MoveSelectedPiece(selectedPiece, startPositionX, startPositionY);
                    startPosition = Vector2.zero;
                    selectedPiece = null;
                    return;
                }
                
                pieces[endPositionX, endPositionY] = selectedPiece;
                pieces[startPositionX, startPositionY] = null;
                MoveSelectedPiece(selectedPiece, endPositionX, endPositionY);
                TurnManager.instance.EndTurn();
                board.forcePieces.Clear();
            }
            else
            {
                MoveSelectedPiece(selectedPiece, startPositionX, startPositionY);
                startPosition = Vector2.zero;
                selectedPiece = null;
            }
        }
    }

    private void MoveSelectedPiece(Piece piece, int x, int y)
    {
        Transform pieceTransform = piece.transform;
        pieceTransform.position = new Vector3(x - 3.5f, 0, y - 3.5f);
    }
}
