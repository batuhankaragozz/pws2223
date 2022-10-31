using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //speed staat voor de snelheid
    //currentPosX geeft aan wat de positie van de speler is op een moment
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        //SmoothDamp gebruiken we om de camera te verplaatsen richting een bepaald doelwit.
        //We vermenigvuldigen de snelheid met time.deltatime om het frame rate onafhankelijk te maken.
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);
    }

    //We veranderen hiermee de bestemming van de camera.
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
