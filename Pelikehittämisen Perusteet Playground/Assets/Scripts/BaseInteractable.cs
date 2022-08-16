using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseInteractable : MonoBehaviour
{
    public UnityEvent OnInteract;
    public bool isOnEnterInteraction = false;

    [Header("Audio")]
    public AudioClip soundEffect;
    public float volume;

    AudioSource AS;

    public virtual void Start()
    {
        if (!GetComponent<AudioSource>())
        {
            this.gameObject.AddComponent<AudioSource>();
        }

        AS = GetComponent<AudioSource>();
    }

    public virtual void Interact()
    {
        OnInteract.Invoke();
        AS.PlayOneShot(soundEffect, volume);
    }
}
