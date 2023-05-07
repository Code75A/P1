using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction {up,down,left,right};
    public Direction direction;

    [Header("房间信息")]
    public GameObject RoomPrefab;
    public int RoomNumber;
    public Color StartColor, EndColor;

    private GameObject EndRoom;
    
    [Header("位置控制")]
    public Transform GeneratorP;
    public float deltaX, deltaY;
    public LayerMask RoomLayer;

    public List<GameObject> rooms = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<RoomNumber;i++)
        {
            rooms.Add(Instantiate(RoomPrefab, GeneratorP.position, Quaternion.identity));
            ChangePointPosition();
        }
        EndRoom = rooms[0];
        foreach(var room in rooms)
        {
            if(room.transform.position.sqrMagnitude>EndRoom.transform.position.sqrMagnitude)
            {
                EndRoom = room;
            }
        }
        rooms[0].GetComponent<SpriteRenderer>().color = StartColor;
        EndRoom.GetComponent<SpriteRenderer>().color = EndColor;
    }
    private void ChangePointPosition()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);
            switch (direction)
            {
                case Direction.up:
                    GeneratorP.position += new Vector3(0, deltaY, 0);
                    break;
                case Direction.down:
                    GeneratorP.position += new Vector3(0, -deltaY, 0);
                    break;
                case Direction.left:
                    GeneratorP.position += new Vector3(-deltaX, 0, 0);
                    break;
                case Direction.right:
                    GeneratorP.position += new Vector3(deltaX, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(GeneratorP.position, 0.3f, RoomLayer));
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
