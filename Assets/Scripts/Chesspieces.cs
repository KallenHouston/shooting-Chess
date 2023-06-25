using UnityEngine;

public class Chesspieces : MonoBehaviour
{
    //Ref
    public GameObject control;
    public GameObject movePlate;

    //Pos
    private int boardX = -1;
    private int boardY = -1;

    //Keep track of Black and White pieces
    private string player = "player";

    //Sprite refenrences for chess pieces.
    public Sprite blackQueen, blackKnight, blackBishop, blackKing, blackRook, blackPawn;
    public Sprite whiteQueen, whiteKnight, whiteBishop, whiteKing, whiteRook, whitePawn;

    public void Active()
    {
        control = GameObject.FindGameObjectWithTag("GameController");

        //take the instatiation of the object and adjust the transform.
        SetCoords();

        switch (name)
        {
            case "BlackQueen": GetComponent<SpriteRenderer>().sprite = blackQueen; player = "black"; break;
            case "BlackKnight": GetComponent<SpriteRenderer>().sprite = blackKnight; player = "black"; break ;
            case "BlackBishop": GetComponent<SpriteRenderer>().sprite = blackBishop; player = "black"; break;
            case "BlackKing": GetComponent<SpriteRenderer>().sprite = blackKing; player = "black"; break;
            case "BlackRook": GetComponent<SpriteRenderer>().sprite = blackRook; player = "black"; break;
            case "BlackPawn": GetComponent<SpriteRenderer>().sprite = blackPawn; player = "black"; break;

            case "WhiteQueen": GetComponent<SpriteRenderer>().sprite = whiteQueen; player = "white"; break;
            case "WhiteKnight": GetComponent<SpriteRenderer>().sprite = whiteKnight; player = "white"; break;
            case "WhiteBishop": GetComponent<SpriteRenderer>().sprite = whiteBishop; player = "white"; break;
            case "WhiteKing": GetComponent<SpriteRenderer>().sprite = whiteKing; player = "white"; break;
            case "WhiteRook": GetComponent<SpriteRenderer>().sprite = whiteRook; player = "white"; break;
            case "WhitePawn": GetComponent<SpriteRenderer>().sprite = whitePawn;player = "white"; break;
        }
    }

    public void SetCoords()
    {
        // Get a reference to the chess board sprite
        SpriteRenderer boardSprite = GameObject.Find("Board").GetComponent<SpriteRenderer>();

        // Get a reference to the chess piece sprite
        SpriteRenderer pieceSprite = GetComponent<SpriteRenderer>();

        // Calculate the width and height of each square on the chess board
        float squareWidth = boardSprite.bounds.size.x / 8.0f;
        float squareHeight = boardSprite.bounds.size.y / 8.0f;

        // Calculate the position of the chess piece based on its board coordinates
        float x = boardSprite.transform.position.x - (3.5f - boardX) * squareWidth;
        float y = boardSprite.transform.position.y - (3.5f - boardY) * squareHeight;

        // Adjust y position to center the chess piece vertically within the square
        y += squareHeight / 2.0f - pieceSprite.bounds.size.y / 2.0f;

        // Set the position of the chess piece
        transform.position = new Vector3(x, y, -1.0f);
    }

    public int boardXGet()
    {
        return boardX;
    }
    public int boardYGet()
    {
        return boardY;
    }

    public void boardXSet(int x)
    {
        boardX = x;
    }

    public void boardYSet(int y)
    {
        boardY = y;
    }

    private void OnMouseUp()
    {
        if(!control.GetComponent<Game>().GameOver() && control.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            InitMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitMovePlates()
    {
        switch (name)
        {
            case "BlackQueen":
            case "WhiteQueen":
                LineMove(1, 0);
                LineMove(0, 1);
                LineMove(1, 1);
                LineMove(-1, 0);
                LineMove(0, -1);
                LineMove(-1, -1);
                LineMove(-1, 1);
                LineMove(1, -1);
                break;
            case "BlackKnight":
            case "WhiteKnight":
                LMove();
                break;
            case "BlackBishop":
            case "WhiteBishop":
                LineMove(1, 1);
                LineMove(1, -1);
                LineMove(-1, 1);
                LineMove(-1, -1);
                break;
            case "BlackKing":
            case "WhiteKing":
                SurroundMove();
                break;
            case "BlackRook":
            case "WhiteRook":
                LineMove(1, 0);
                LineMove(0, 1);
                LineMove(-1, 0);
                LineMove(0, -1);
                break;
            case "BlackPawn":
                PawnMove(boardX, boardY - 1);
                break;
            case "WhitePawn":
                PawnMove(boardX, boardY + 1);
                break;
        }
    }

    public void LineMove(int xIncrement, int yIncrement)
    {
        Game sc = control.GetComponent<Game>();

        int x = boardX + xIncrement;
        int y = boardY + yIncrement;

        while (sc.PosOnBoard(x, y) && sc.GetPos(x, y) == null)
        {
            SpawnMovePlate(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PosOnBoard(x, y) && sc.GetPos(x, y).GetComponent<Chesspieces>().player != player)
        {
            AttackSpawnMovePlate(x, y);
        }
    }

    public void LMove()
    {
        PointMovePlate(boardX + 1, boardY + 2);
        PointMovePlate(boardX - 1, boardY + 2);
        PointMovePlate(boardX + 2, boardY + 1);
        PointMovePlate(boardX + 2, boardY - 1);
        PointMovePlate(boardX + 1, boardY - 2);
        PointMovePlate(boardX - 1, boardY - 2);
        PointMovePlate(boardX - 2, boardY + 1);
        PointMovePlate(boardX - 2, boardY - 1);
    }

    public void SurroundMove()
    {
        PointMovePlate(boardX, boardY + 1);
        PointMovePlate(boardX, boardY - 1);
        PointMovePlate(boardX - 1, boardY + 0);
        PointMovePlate(boardX - 1, boardY - 1);
        PointMovePlate(boardX - 1, boardY + 1);
        PointMovePlate(boardX + 1, boardY + 0);
        PointMovePlate(boardX + 1, boardY - 1);
        PointMovePlate(boardX + 1, boardY + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = control.GetComponent<Game>();
        if (sc.PosOnBoard(x, y))
        {
            GameObject cp = sc.GetPos(x, y);
            if (cp == null)
            {
                SpawnMovePlate(x, y);
            } else if (cp.GetComponent<Chesspieces>().player != player)
            {
                AttackSpawnMovePlate(x, y);
            }
        }
    }

    public void PawnMove(int x, int y)
    {
        Game sc = control.GetComponent<Game>();
        if (sc.PosOnBoard(x, y))
        {
            if(sc.GetPos(x, y) == null)
            {
                SpawnMovePlate(x, y);
            }

            if(sc.PosOnBoard(x + 1, y) && sc.GetPos(x+1, y) != null && sc.GetPos(x+1, y).GetComponent<Chesspieces>().player != player)
            {
                AttackSpawnMovePlate(x + 1, y);
            }


            if (sc.PosOnBoard(x - 1, y) && sc.GetPos(x - 1, y) != null && sc.GetPos(x - 1, y).GetComponent<Chesspieces>().player != player)
            {
                AttackSpawnMovePlate(x - 1, y);
            }
        }
    }

    public void SpawnMovePlate(int xMatrix, int yMatrix)
    {
        // Get a reference to the chess board sprite
        SpriteRenderer boardSprite = GameObject.Find("Board").GetComponent<SpriteRenderer>();

        // Get the width and height of each square on the chess board
        float squareWidth = boardSprite.bounds.size.x / 8.0f;
        float squareHeight = boardSprite.bounds.size.y / 8.0f;

        // Calculate the position of the moveplate based on its board coordinates
        float x = boardSprite.transform.position.x - (3.5f - xMatrix) * squareWidth;
        float y = boardSprite.transform.position.y - (3.5f - yMatrix) * squareHeight;

        // Set the position of the moveplate
        GameObject moveplate = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate moveplateScript = moveplate.GetComponent<MovePlate>();
        moveplateScript.SetRef(gameObject);
        moveplateScript.SetCoords(xMatrix, yMatrix);
    }

    public void AttackSpawnMovePlate(int xMatrix, int yMatrix)
    {
        // Get a reference to the chess board sprite
        SpriteRenderer boardSprite = GameObject.Find("Board").GetComponent<SpriteRenderer>();

        // Get the width and height of each square on the chess board
        float squareWidth = boardSprite.bounds.size.x / 8.0f;
        float squareHeight = boardSprite.bounds.size.y / 8.0f;

        // Calculate the position of the moveplate based on its board coordinates
        float x = boardSprite.transform.position.x - (3.5f - xMatrix) * squareWidth;
        float y = boardSprite.transform.position.y - (3.5f - yMatrix) * squareHeight;

        // Set the position of the moveplate
        GameObject moveplate = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate moveplateScript = moveplate.GetComponent<MovePlate>();
        moveplateScript.attacking = true;
        moveplateScript.SetRef(gameObject);
        moveplateScript.SetCoords(xMatrix, yMatrix);
    }
}
