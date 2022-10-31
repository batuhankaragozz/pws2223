using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
    //Currenthealthbar (=huidige gezondheid) moet in update methode, omdat je gezondheid verandert in de game
    private void Update()
    {
        //we delen door 10 omdat we een deel van de 10 mogelijke harten hebben, wij hebben namelijk maar 3 levens in de game.
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}