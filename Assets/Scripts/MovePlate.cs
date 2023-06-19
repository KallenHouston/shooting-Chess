using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject control;

    GameObject reference = null;

    //Board positions.
    int matrixX;
    int matrixY;

    //fast: movement, true: attacking.
    public bool attacking = false;

    public void Start()
    {
        if(attacking)
        {
            //change the sprite to Red.
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        }   
    }

    public void OnMouseUp()
    {
        control = GameObject.FindGameObjectWithTag("GameController");

        if (attacking)
        {
            GameObject chesspiece = control.GetComponent<Game>().GetPos(matrixX, matrixY);

            Destroy(chesspiece);
        }

        control.GetComponent<Game>().SetPosEmpty(reference.GetComponent<Chesspieces>().boardXGet(),
            reference.GetComponent<Chesspieces>().boardYGet());

        reference.GetComponent<Chesspieces>().boardXSet(matrixX);
        reference.GetComponent<Chesspieces>().boardYSet(matrixY);
        reference.GetComponent<Chesspieces>().SetCoords();

        control.GetComponent<Game>().SetPos(reference);
        reference.GetComponent<Chesspieces>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetRef(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetRef()
    {
        return reference;
    }
}
