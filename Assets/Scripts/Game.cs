using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject Piece;

    //Pos and each chesspiece team.
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] blackPlayer = new GameObject[16];
    private GameObject[] whitePlayer = new GameObject[16];

    private string currentPlayer = "white";

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        whitePlayer = new GameObject[] { Create("WhiteRook", 0, 0), Create("WhiteKnight", 1, 0),
            Create("WhiteBishop", 2, 0), Create("WhiteQueen", 3, 0), Create("WhiteKing", 4, 0),
            Create("WhiteBishop", 5, 0), Create("WhiteKnight", 6, 0), Create("WhiteRook", 7, 0),
            Create("WhitePawn", 0, 1), Create("WhitePawn", 1, 1), Create("WhitePawn", 2, 1),
            Create("WhitePawn", 3, 1), Create("WhitePawn", 4, 1), Create("WhitePawn", 5, 1),
            Create("WhitePawn", 6, 1), Create("WhitePawn", 7, 1) };
        blackPlayer = new GameObject[] { Create("BlackRook", 0, 7), Create("BlackKnight",1,7),
            Create("BlackBishop",2,7), Create("BlackQueen",3,7), Create("BlackKing",4,7),
            Create("BlackBishop",5,7), Create("BlackKnight",6,7), Create("BlackRook",7,7),
            Create("BlackPawn", 0, 6), Create("BlackPawn", 1, 6), Create("BlackPawn", 2, 6),
            Create("BlackPawn", 3, 6), Create("BlackPawn", 4, 6), Create("BlackPawn", 5, 6),
            Create("BlackPawn", 6, 6), Create("BlackPawn", 7, 6) };

        //Set all chesspiece positions.
        for (int i = 0; i < blackPlayer.Length; i++)
        {
            SetPos(blackPlayer[i]);
            SetPos(whitePlayer[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(Piece, new Vector3(0,0,-1), Quaternion.identity);
        Chesspieces cm = obj.GetComponent<Chesspieces>();
        cm.name = name;
        cm.boardXSet(x);
        cm.boardYSet(y);
        cm.Active();
        return obj;
    }

    public void SetPos(GameObject obj)
    {
        Chesspieces cm = obj.GetComponent<Chesspieces> ();

        positions[cm.boardXGet(), cm.boardYGet()] = obj;
    }

    public void SetPosEmpty(int x,  int y)
    {
        positions[x, y] = null;
    }    

    public GameObject GetPos(int x, int y) 
    {
        return positions[x, y];
    }

    public bool PosOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) 
        {
            return false;
        } 
        return true;
    }
}
