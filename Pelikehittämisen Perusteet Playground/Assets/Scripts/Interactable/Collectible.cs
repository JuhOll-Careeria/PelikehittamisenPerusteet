using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : BaseInteractable
{
    public override void Start()
    {
        base.Start();
        base.OnInteract.AddListener(AddPoint);
    }

    public void AddPoint()
    {
        // ASD
        Destroy(this.gameObject, 0.1f);
    }
}
