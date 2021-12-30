using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool isWhite;
    public bool isKing;

    public bool IsForceToMove(Piece[,] board, int x, int y)
    {
        if (isWhite || isKing)
        {
            if (x >= 2 && y <= 5)
            {
                Piece targetPiece = board[x - 1, y + 1];
                if (targetPiece != null && targetPiece.isWhite != isWhite)
                {
                    if (board[x - 2, y + 2] == null)
                        return true;
                }
            }
            
            if (x <= 5 && y <= 5)
            {
                Piece targetPiece = board[x + 1, y + 1];
                if (targetPiece != null && targetPiece.isWhite != isWhite)
                {
                    if (board[x + 2, y + 2] == null)
                        return true;
                }
            }
        }
        
        if(!isWhite || isKing)
        {
            if (x >= 2 && y >= 2)
            {
                Piece targetPiece = board[x - 1, y - 1];
                if (targetPiece != null && targetPiece.isWhite != isWhite)
                {
                    if (board[x - 2, y - 2] == null)
                        return true;
                }
            }
            
            if (x <= 5 && y >= 2)
            {
                Piece targetPiece = board[x + 1, y - 1];
                if (targetPiece != null && targetPiece.isWhite != isWhite)
                {
                    if (board[x + 2, y - 2] == null)
                        return true;
                }
            }
        }

        return false;
    }
    
    public bool ValidMove(Piece[,] board, int startPositionX, int startPositionY, int endPositionX, int endPositionY)
    {
        if (board[endPositionX, endPositionY] != null)
            return false;

        int deltaMoveX = Mathf.Abs(startPositionX - endPositionX);
        int deltaMoveY = endPositionY - startPositionY;
        
        if (isWhite || isKing)
        {
            if (deltaMoveX == 1)
            {
                if (deltaMoveY == 1)
                    return true;
            }
            else if (deltaMoveX == 2)
            {
                if (deltaMoveY == 2)
                {
                    Piece piece = board[(startPositionX + endPositionX) / 2, (startPositionY + endPositionY) / 2];
                    if (piece != null && piece.isWhite != isWhite)
                        return true;
                }
            }
        }
        
        if (!isWhite || isKing)
        {
            if (deltaMoveX == 1)
            {
                if (deltaMoveY == -1)
                    return true;
            }
            else if (deltaMoveX == 2)
            {
                if (deltaMoveY == -2)
                {
                    Piece piece = board[(startPositionX + endPositionX) / 2, (startPositionY + endPositionY) / 2];
                    if (piece != null && piece.isWhite != isWhite)
                        return true;
                }
            }
        }
        return false;
    }
}
