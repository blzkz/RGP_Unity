using UnityEngine;
using System.Collections;

public class personaje : MonoBehaviour {

    Rigidbody2D personajeRb;
    Animator personajeAnimator;

	// Use this for initialization
	void Start () {
        personajeRb = GetComponent<Rigidbody2D>();
        personajeAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 moviento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moviento != Vector2.zero)
        {
            personajeAnimator.SetBool("enMovimiento", true);
            personajeAnimator.SetFloat("x", moviento.x);
            personajeAnimator.SetFloat("y", moviento.y);
        }
        else
        {
            personajeAnimator.SetBool("enMovimiento", false);
        }

        personajeRb.MovePosition(personajeRb.position + moviento * Time.deltaTime * 2);
	}
}
