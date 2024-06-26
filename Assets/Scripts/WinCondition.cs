using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private float timer;
    [SerializeField] private float possibleWinTime;
    [SerializeField] private GameObject[] spawner;
    [SerializeField] private bool hasBoss;
    public bool canSpawnBoss = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EndGameManager.endManager.gameOver == true)
            return;
        timer += Time.deltaTime;
        if (timer >= possibleWinTime) 
        {
            if (hasBoss == false)
            {
                EndGameManager.endManager.possibleWin = true;
                EndGameManager.endManager.StartResolveSequence();
            }
            else
            {
                canSpawnBoss = true;
            }

            for (int i = 0; i< spawner.Length; i++) 
            {
                spawner[i].SetActive(false);
            }
            
            gameObject.SetActive(false);
            //EndGameManager.endManager.ResolveGame();
        }
    }
}
