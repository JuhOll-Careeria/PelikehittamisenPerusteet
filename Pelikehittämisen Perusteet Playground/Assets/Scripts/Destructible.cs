using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    // Objektin max HP
    public int maxHealth = 10;


    private int currentHealth;     // Nykyinen hp
    SpriteRenderer renderer;     // Renderer referenssi v‰rin vaihtoa varten
    private Color defaultColor;  // SpriteRenderiin asetettu v‰ri (default v‰ri)

    private void Start()
    {
        currentHealth = maxHealth; // Annetaan current hp arvoksi maxhealth
        renderer = GetComponent<SpriteRenderer>(); // Haetaan objektist SpriteRenderer
        defaultColor = renderer.color;  // tallennetaan alkuper‰inen v‰ri defaultColor muuttujaan
    }

    // T‰t‰ kutsutaan Pelaaja skriptist‰ kun pelaaja ampuu Fire toiminnolla
    public void TakeDamage(int dmg)
    {
        // V‰hennett‰‰n otettu damage (dmg) currentHealthist‰
        currentHealth -= dmg;

        // Jos health on alle 0 niin tuhotaan objekti
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        // K‰ynnistet‰‰n coroutine "Damage Blink" toimintoa varten
        StartCoroutine(DamageColorBlink());
    }

    IEnumerator DamageColorBlink()
    {
        renderer.color = Color.red; // Kun objekti ottaa damagea, muutetaan v‰ri punaiseksi
        yield return new WaitForSeconds(0.1f); // Odotetaan 0.1 sekuntia
        renderer.color = defaultColor; // vaihdetaan v‰ri takaisin alkuper‰iseen v‰riin
    }
}
