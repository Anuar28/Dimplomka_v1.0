using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="ScriptableObject/PowerUpSpawner",fileName = "Spawner")]

public class ScriptableObjExample : ScriptableObject 
{
    public int spwThreshold;
    public GameObject[] powerUp;

    public void SpawnPowerUp(Vector3 spawnPos)
    {
        int randomChance = Random.Range(0, 100);
        if (randomChance > spwThreshold)
        {
            int randomPowerUp = Random.Range(0, powerUp.Length);
            Instantiate(powerUp[randomPowerUp], spawnPos, Quaternion.identity);
        }


    }
}
