using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board instance;
    
    public Piece[,] pieces = new Piece[8, 8];
    public List<Piece> forcePieces;
    
    public Piece whitePiece;
    public Piece blackPiece;

    public Transform whiteTeamParent;
    public Transform blackTeamParent;

    private Vector3 _piecePositionMultiplier = new(3.5f, 0, 3.5f);

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        forcePieces = new List<Piece>();
        
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        for (int y = 0; y < 3; y++)
        {
            bool oddRow = y % 2 == 0;
            for (int x = 0; x < 8; x += 2)
                GeneratePiece(oddRow ? x : x + 1, y, whitePiece, whiteTeamParent);
        }
        
        for (int y = 7; y > 4; y--)
        {
            bool oddRow = y % 2 == 0;
            for (int x = 0; x < 8; x += 2)
                GeneratePiece(oddRow ? x : x + 1, y, blackPiece, blackTeamParent);
        }
    }

    private void GeneratePiece(int x, int y, Piece piece, Transform pieceParent)
    {
        Piece newPiece = Instantiate(piece, pieceParent, true);
        pieces[x, y] = newPiece;
        CorrectPiecePosition(newPiece, x, y);
    }

    private void CorrectPiecePosition(Piece piece, int x, int y)
    {
        piece.transform.position = Vector3.right * x + Vector3.forward * y - _piecePositionMultiplier;
    }

    public void ScanForPossibleMove()
    {
        forcePieces = new List<Piece>();

        for (int i = 0; i < pieces.GetLength(0); i++)
        {
            for (int j = 0; j < pieces.GetLength(1); j++)
            {
                Piece piece = pieces[i, j];
                
                if(piece != null && piece.isWhite == TurnManager.instance.isWhiteTurn)
                    if(piece.IsForceToMove(pieces,i,j))
                        forcePieces.Add(piece);
            }
        }
    }
}
