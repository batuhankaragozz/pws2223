using UnityEngine;

public class Projectile : MonoBehaviour
{
    //snelheid kan aangepast worden in Unity
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;


    private void Awake()
    {
        //Met GetComponent krijgen we verwijzingen naar de animator en de box collider.
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //We checken of onze fireball iets heeft geraakt
        if (hit) return;
        //De bewegings snelheid is gelijk aan de snelheid maal de verstreken tijd
        float movementSpeed = speed * Time.deltaTime * direction;
        //We gebruiken transform.Translate om de object te laten bewegen. x en y as zijn gelijk aan 0.
        transform.Translate(movementSpeed, 0, 0);

        //We gebruiken lifetime om de fireball inactief te maken als hij geen object hit.
        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }
    
    //We checken of onze fireball een andere object heeft geraakt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        //We zetten de boxCollider uit.
        boxCollider.enabled = false;
        //We spelen de explosie animatie af.
        anim.SetTrigger("explode");
    }

    //We gebruiken deze methode elke keer wanneer we schieten om de fireball naar links of naar rechts te sturen en we resetten de staat van de fireball met deze methode.
    public void SetDirection(float _direction)
    {
        //Elke keer dat we de richting van de fireball instellen, wordt de lifetime gereset op 0.
        lifetime = 0;
        direction = _direction;
        //We zorgen ervoor dat de game object actief is
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        //We gaan ervoor zorgen dat de fireball echt eruitziet als naar welke kant hij op gaat, dus als hij naar links gaat moet het niet lijken alsof hij naar rechts gaat en andersom.
        float localScaleX = transform.localScale.x;
        //We checken of de localScaleX niet gelijk is aan de richting, dus niet de goede kant opgaat
        if (Mathf.Sign(localScaleX) != _direction)
            //Als de richting niet klopt met de localScaleX, veranderen we de richting van de fireball.
            localScaleX = -localScaleX;

        //voor y en z houden we dezelfde waardes, voor de x as hebben we localScaleX als waarde.
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //Met Deactivate deactiveren we de fireball nadat de explosie animatie klaar is.
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

