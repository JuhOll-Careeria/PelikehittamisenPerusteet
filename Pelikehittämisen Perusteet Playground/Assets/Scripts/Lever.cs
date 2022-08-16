using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : BaseInteractable
{
    [Header("Lever")]
    public bool isActivated = false;
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;

    Animator anim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        base.OnInteract.AddListener(ToggleLever);

        if (isActivated)
        {
            onActivate.Invoke();
            anim.Play("LeverOn");
        }
        else
        {
            onDeactivate.Invoke();
            anim.Play("LeverOff");
        }
    }

    public void ToggleLever()
    {
        if (isActivated)
        {
            isActivated = false;
            anim.Play("LeverOff");
            onDeactivate.Invoke();
        }
        else
        {
            isActivated = true;
            anim.Play("LeverOn");
            onActivate.Invoke();
        }
    }
}
