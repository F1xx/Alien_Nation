using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawnerbase
{
    public Enemy[] SmallEnemies = null;
    public Enemy[] MediumEnemies = null;
    public Enemy[] BigEnemies = null;

    public int[] Weights = { 20, 10, 3 };
    float[] FWeights = { 20.0f, 10.0f, 3.0f };

    public static int WaveAmount = 3;

    float TimeUntilAdditionalDifficulty = 5.0f;
    static int Difficulty = 0;
    Timer DifficultyIncreaseTimer = null;

    private void Start()
    {
        for(int i = 0; i < Weights.Length; i++)
        {
            FWeights[i] = (float)Weights[i];
        }

        DifficultyIncreaseTimer = new Timer(TimeUntilAdditionalDifficulty, IncreaseDificulty);
        DifficultyIncreaseTimer.Start();
        //start with a wave
        StartCoroutine("WaveSpawn");
    }

    protected override void Update()
    {
        base.Update();
        DifficultyIncreaseTimer.Update();
        UpdateWeights();
    }

    IEnumerator WaveSpawn()
    {
        for (int x = 0; x < WaveAmount + Difficulty; x++)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(.15f);
        }
    }

    void UpdateWeights()
    {
        int[] weights = { 0, 0, 0 };
        int[] newWeights = { 0, 0, 0 };

        for(int i = 0; i < FWeights.Length; i++)
        {
            //get the weights before they change
            weights[i] = (int)FWeights[i];

            switch (i)
            {
                case 0:
                    FWeights[i] -= (Time.deltaTime / 10);
                    FWeights[i] = Mathf.Clamp(FWeights[i], 1.0f, 100.0f);
                    break;
                case 1:
                    FWeights[i] += (Time.deltaTime / 15);
                    FWeights[i] = Mathf.Clamp(FWeights[i], 1.0f, 100.0f);
                    break;
                case 2:
                    FWeights[i] += (Time.deltaTime / 25);
                    FWeights[i] = Mathf.Clamp(FWeights[i], 1.0f, 100.0f);
                    break;
            }

            //have the weights after they change
            newWeights[i] = (int)FWeights[i];

            if(newWeights[i] != weights[i])
            {
                Weights[i] = newWeights[i];
            }
        }
    }

    public static void IncreaseDificulty()
    {
        IncreaseDificulty(1);
    }

    public static void IncreaseDificulty(int amount)
    {
        Difficulty += amount;
        ClampDifficulty();
    }

    public static void DecreaseDifficulty()
    {
        DecreaseDifficulty(1);
    }

    public static void DecreaseDifficulty(int amount)
    {
        Difficulty -= amount;

        ClampDifficulty();
    }

    static void ClampDifficulty()
    {
        Difficulty = Mathf.Clamp(Difficulty, 0, 20);
    }

    protected Enemy GetRandomEnemyFromArray(Enemy[] enemies)
    {
        Debug.Assert(enemies.Length > 0, "Attempting to spawn enemy from empty array.");

        int rand = Random.Range(0, enemies.Length);

        return enemies[rand];
    }

    protected Enemy[] GetRandomEnemyArray()
    {
        //int rand = Random.Range(0, 3);
        int rand = GetRandomWeightedIndex(Weights);

        switch (rand)
        {
            case 0:
                return SmallEnemies;
            case 1:
                return MediumEnemies;
            case 2:
                return BigEnemies;
            default:
                Debug.LogError("Went out of range when finding random enemy array with a number of: " + rand);
                break;
        }

        return null;
    }

    protected override void Spawn()
    {
        StartCoroutine("WaveSpawn");
    }

    void SpawnEnemy()
    {
        Enemy[] enemies = GetRandomEnemyArray();

        Enemy enemy = GetRandomEnemyFromArray(enemies);

        Spawn(enemy.gameObject);
    }

    public int GetRandomWeightedIndex(int[] weights)
    {
        // Get the total sum of all the weights.
        int weightSum = 0;
        for (int i = 0; i < weights.Length; ++i)
        {
            weightSum += weights[i];
        }

        // Step through all the possibilities, one by one, checking to see if each one is selected.
        int index = 0;
        int lastIndex = weights.Length - 1;
        while (index < lastIndex)
        {
            // Do a probability check with a likelihood of weights[index] / weightSum.
            if (Random.Range(0, weightSum) < weights[index])
            {
                return index;
            }

            // Remove the last item from the sum of total untested weights and try again.
            weightSum -= weights[index++];
        }

        // No other item was selected, so return very last index.
        return index;
    }
}
