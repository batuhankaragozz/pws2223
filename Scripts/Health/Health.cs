using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    //Hoeveel gezondheid we hebben aan het begin
    [SerializeField] private float startingHealth;
    //Hoeveel gezondheid je op dit moment hebt
    //Hier gebruiken we get zodat we deze float kunnen krijgen in andere scripts en private set gebruiken we zodat we de waardes alleen hier kunnen veranderen
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    //hoe lang is de speler onkwetsbaar
    [SerializeField] private float iFramesDuration;
    //Hoe vaak geeft de speler rood flashlight af voordat hij weer in zijn normale staat is
    [SerializeField] private int numberOfFlashes;
    //We veranderen de kleur van de speler wanneer die onkwetsbaar is
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth;
        //We hebben een referentie nodig naar de animator om de animaties af te kunnen spelen.
        anim = GetComponent<Animator>();
        //De volgende methode is nodig om de sprites te laten werken voor het veranderen van de kleur van de speler
        spriteRend = GetComponent<SpriteRenderer>();

    }

    public void TakeDamage(float _damage)
    {
        //Minimale waarde van gezondheid moet 0 zijn, en we kunnen niet meer gezondheid hebben dan in het begin van de game.
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        //We checken of de gezondheid groter is dan 0
        if(currentHealth > 0)
        {
            //Speler leeft nog, maar heeft pijn
            anim.SetTrigger("hurt");
            //We gebruiken de volgende om de IEnumerator methode te laten werken
            StartCoroutine(Invunerability());
        }
        //Gezondheid is niet groter dan 0, speler is dood
        else
        {
            if(!dead)
            {
                //Speler is dood
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;

            }

        }
    }
    //We verhogen de gezondheid
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        //We geven de juiste layers voor de speler en de enemies, 10 voor speler en 11 voor enemy
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            //We veranderen de kleur naar rood. De cijfers geven de waarde van RGB kleur
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            //We veranderen de kleur weer terug naar wit
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        //
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}