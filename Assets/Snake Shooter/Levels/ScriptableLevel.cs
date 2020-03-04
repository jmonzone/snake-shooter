using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyWave
{
    [SerializeField] private ScriptableEnemy enemy;
    [SerializeField] private int count;

    public ScriptableEnemy Enemy => enemy;
    public int Count => count;
}

[Serializable]
public struct Round
{
    [SerializeField] private List<EnemyWave> waves;

    public List<EnemyWave> Waves => waves;
    public int EnemyCount
    {
        get
        {
            var enemyCount = 0;
            waves.ForEach((wave) =>
            {
                enemyCount += wave.Count;
            });
            return enemyCount;
        }
    }
}

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 5)]
public class ScriptableLevel : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Color backgroundColor;
    [SerializeField] private int reward;
    [SerializeField] private List<Round> rounds;

    public int ID => id;
    public int LevelIndex => ID - 1;
    public Sprite Sprite => sprite;
    public int RoundCount => rounds.Count;
    public List<Round> Rounds => rounds;
    public int Reward => reward;
    public Color BackgroundColor => backgroundColor;

}


