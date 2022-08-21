using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : BaseInteractable
{

    Animator anim;

    public override void Start()
    {
        anim = GetComponent<Animator>();
        base.Start();
        base.OnInteract.AddListener(SetPlayerRespawn);
    }

    public void SetPlayerRespawn()
    {
        FindObjectOfType<PlayerHealth>().spawnPoint = this.transform;
        anim.Play("CheckpointActivate");
    }
}
