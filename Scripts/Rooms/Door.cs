using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Dit checkt of de object waarmee we hebben gebotst de tag gelijk aan player heeft.
        if (collision.tag == "Player")
        {
            //We checken of de spelers x positie kleiner is dan die van de deur
            //Als dat zo is, komt de speler van links, en kan hij naar de volgende room.
            if (collision.transform.position.x < transform.position.x)
                cam.MoveToNewRoom(nextRoom);
            //Als dat niet zo is, gaat de camera naar de vorige room.
            else
                cam.MoveToNewRoom(previousRoom);
        }
    }

}
