using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;

        //We stellen de positie in als de firepoint positie
        arrows[FindArrow()].transform.position = firePoint.position;
        //We stellen de richting van de projectile in 
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    //Deze methode helpt ons de projectiles te trekken
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            //return: de eerste fireball die niet actief is
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        //Als de methode geen inactieve fireball vindt, gebruikt hij de eerste fireball
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //We controleren of de cooldowntimer groter of gelijk is aan de attackCooldown
        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}