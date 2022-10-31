using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    //Movementdistance: geeft weer hoe ver deze object beweegt
    [SerializeField] private float movementDistance;
    //Snelheid van de beweging
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    //De enemy beweegt naar links
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        //berekeningen van de linkerkant en rechterkant 
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        //Als de object naar links beweegt
        if (movingLeft)
        {
            //we controleren of de x positie van de object groter is dan de leftEdge
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;
        }
        //Object beweegt naar rechts
        else
        {
            //Rechterkant is groter dan de x positie van de object
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }

    //We detecteren botsingen met de player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //
        if (collision.tag == "Player")
        {
            //We verminderen de health van de player door de enemy's damage
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
