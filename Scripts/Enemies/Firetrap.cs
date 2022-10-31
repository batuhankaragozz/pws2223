using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    //Hoeveel tijd voor de trap nodig is om actief te worden nadat de speler erin is getrapt
    [SerializeField] private float activationDelay;
    //Hoelang de trap actief blijft nadat het is geactiveerd
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; //when the trap gets triggered
    private bool active; //when the trap is active and can hurt the player

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //We controleren of de tag collided is met de player
        if (collision.tag == "Player")
        {
            if (!triggered)
                StartCoroutine(ActivateFiretrap());
            //de speler krijgt damage
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    //We gebruiken IEnumerator omdat we te maken hebben met delays
    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to notify the player and trigger the trap
        //de trap is veroorzaakt:
        triggered = true;
        spriteRend.color = Color.red;

        //Wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; //turn the sprite back to its initial color
        //trap is geactiveerd
        active = true;
        anim.SetBool("activated", true);

        //Wait until X seconds, deactivate trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        //trap is gedeactiveerd
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}