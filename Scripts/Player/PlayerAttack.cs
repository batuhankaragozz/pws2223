using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Deze waarde staat voor hoeveel tijd er gepasseerd zijn voordat we het volgende schot afvuren.
    //Na de laatste schot moet er genoeg tijd gepasseerd zijn. Hiervoor gebruiken we cooldownTimer
    //Achter cooldownTimer zetten we Mathf.Infinity, want anders is de cooldownTimer gelijk aan 0, wat betekent dat we niet kunnen aanvallen, omdat het programma denkt dat er niet genoeg tijd voorbij is sinds de vorige schot.
    //firePoint is de positie van waar de kogels gevuurd gaan worden.
    //fireballs is voor de 10 fireballs die we hebben gecreeërd.
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        //We gebruiken GetComponent om referenties te krijgen voor animator en de speler.
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //We checken of de linker muisknop ingedrukt is met Input.GetMouseButton(0)
        //We checken ook of er genoeg tijd is gepasseerd om de volgende schot af te kunnen vuren met cooldownTimer > attackCooldown
        //Als dat helemaal goed is, beginnen we met aanvallen.
        //We zorgen ervoor dat de speler in staat is om aan te mogen vallen
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        //cooldownTimer wordt steeds hoger met de gepasseerde tijd.
        cooldownTimer += Time.deltaTime;
    }

    //Aanvallen
    private void Attack()
    {
        //We zorgen ervoor dat de aanval animatie wordt afgespeeld bij het aanvallen.
        anim.SetTrigger("attack");
        //cooldownTimer wordt bij attack gereset op 0
        cooldownTimer = 0;

        //Elke keer wanneer we aanvallen zullen we een van de fireballs nemen en zijn positie resetten tot de positie van de firepoint.
        fireballs[FindFireball()].transform.position = firePoint.position;
        //Hiermee krijgen we de Projectile component van de fireball.
        //We gebruiken SetDirection om het te sturen in de richting waarin de speler staat
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    //Deze methode gebruiken we om meerdere fireballs achter elkaar af te kunnen vuren
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            //Hier controleren we of de fireball niet actief is in de hierarchy
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
            
        return 0;
    }
}
