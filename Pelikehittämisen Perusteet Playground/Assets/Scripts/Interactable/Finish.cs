using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : BaseInteractable
{
    [Header("Finish")]
    public bool loadSameLevel = true;
    public int nextLevel;
    public float changeLevelTimer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        base.OnInteract.AddListener(OnFinish);
    }

    public void OnFinish()
    {
        Invoke("ChangeLevel", changeLevelTimer);
    }

    void ChangeLevel()
    {
        if (loadSameLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
