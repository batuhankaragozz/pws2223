using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //serializefield zorgt ervoor dat je de waarde van speed kan veranderen in unity zelf.
    //float moet voor waarden die in unity alle getallen kunnen zijn.
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        //Pakt referenties voor rigidbody en animator van object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //slaat waarde van horizontaal waarde op in horizontaalInput. Hiermee wordt code schrijven makkelijker want dan hoef je alleen horizontaalInput te schrijven inplaats van helemaal Input.GetAxis("Horizontal").
        horizontalInput = Input.GetAxis("Horizontal");

       

        //animatie
        //Draait de speler om van richting.
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

 


        //Zet animator parameters
        //horizontalInput is niet gelijk aan 0, want als het 0 is zijn er geen horizontalebeweegknoppen ingedrukt. Dus als er wel een horizontalebeweegknop is ingedrukt dan gaat de animatie naar rennnen.
        //Het geeft een boolean antwoord terug, dus waar of niet waar. Als horizontalInput gelijk is aan 0 dan is het antwoord terug niet waar. Als horizontalInput niet gelijk is aan 0 dan is het antwoord terug waar.
        //De animator krijgt de informatie over of de speler geland is of niet
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //springen (Wall jump)
        if (wallJumpCooldown < 0.2f)
        {
            //lopen
            //Als de knop is ingevoerd van een horizontale as zoals a of d. Deze waarde kan 1, 0 of -1 zijn. De waarde wordt dan vermenigvuldigd met de snelheid dat is aangegeven in unity.
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);


            if (onWall() && !isGrounded())
            {
                //Als de speler aan de muur zit, gaat hij vastzitten en niet van de muur kunnen vallen.
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;
            //Deze if statement kijkt als de spacebar wordt ingedrukt. Als dat zo is gaat de speler springen.
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        //Als de space bar wordt ingedrukt, gaat de speler met de snelheid dat is ingevoerd in unity omhoog en de horizontale as wordt bepaalt door bodyvelocity.
        //Als de speler is geland, kan hij springen
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;

        }
    }

    //Deze methode verteld of de speler geland is of niet
    private bool isGrounded()
    {
        //Deze methode gebruikt raycasting om een virtuele laser te richten in een bepaald punt. Als het een object raakt, krijgen we informatie over dat object door BoxCast.
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        //Deze methode gebruikt raycasting om een virtuele laser te richten in een bepaald punt. Als het een object raakt, krijgen we informatie over dat object
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        //Aanvallen kan alleen als  horizontalInput = 0 en de speler is op de grond en niet vast aan de muur.
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}


