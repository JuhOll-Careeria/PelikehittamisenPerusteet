using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float respawnTime = 4;
    public AudioClip onDeathSound;
    public float onDeathSoundVolume;

    PlayerContoller PC;
    Animator anim;
    int currentHealth;
    AudioSource AS;

    [HideInInspector] public Transform spawnPoint;

    private void Start()
    {
        PC = GetComponent<PlayerContoller>();
        AS = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;


        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        PC.isDead = true;
        AS.PlayOneShot(onDeathSound, onDeathSoundVolume);
        anim.PlayInFixedTime("Dead", 0);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime / 2);
        this.gameObject.transform.position = spawnPoint.position;
        anim.Play("Respawn", 0);
        yield return new WaitForSeconds(respawnTime / 2);
        PC.isDead = false;
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DeathCollider>())
        {
            TakeDamage(100);
        }
    }

}
