using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    //kleine vertraging
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    //Hier slaan we de 4 directions op
    private Vector3[] directions = new Vector3[4];
    //Als de spikehead de speler detecteert, zullen we de positie van de speler opslaan in deze variabele;
    private Vector3 destination;
    private float checkTimer;
    //waarde waarmee we de code kunnen vertellen wanneer de spikehead de speler aan het aanvallen is.
    private bool attacking;

    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        //De spikehead laten bewegen richting de bestemming alleen bij het aanvallen
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            //Als checktimer groter is dan checkdelay, dan controleren we of de spikehead de speler ziet
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        //Controleren of de spikehead de speler ziet in alle 4 de directions
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            //We checken of de hitcollider niet gelijk is aan 0.
            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    //Methode waarmee we de 4 directions berekenen
    private void CalculateDirections()
    {
        directions[0] = transform.right * range; //Rechter direction
        directions[1] = -transform.right * range; //Linker direction
        directions[2] = transform.up * range; //Up direction
        directions[3] = -transform.up * range; //Down direction
    }
    private void Stop()
    {
        destination = transform.position; //Set destination as current position so it doesn't move
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop(); //Spikehead stopt wanneer hij iets aanraakt
    }
}