using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerPrefab = Instantiate(playerPrefab, this.transform.position, this.transform.rotation);
        playerPrefab.GetComponent<PlayerHealth>().spawnPoint = this.transform;
    }
}
