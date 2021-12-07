using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private EnemySpawnPoint[] _spawnPoints;
    private List<Enemy> _enemies;
    private Enemy _enemyPrefab;
    private CharactorMoving _moving;
    private int _platformID;

    private void SpawnEnemies()
    {
        _spawnPoints = GetComponentsInChildren<EnemySpawnPoint>();

        _enemies = new List<Enemy>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            var enemy = (Instantiate(_enemyPrefab, _spawnPoints[i].transform.position,
                _spawnPoints[i].transform.rotation));

            enemy.Init(this);

            _enemies.Add(enemy);
        }
    }

    private void MoveToNextPlatform()
    {
        _moving.MoveToNextPoint(_platformID);
    }

    public void DeliteEnemyFromList(Enemy enemy)
    {
        _enemies.FirstOrDefault(enemy => _enemies.Remove(enemy));

        CheckCurrentEnemiesCount();
    }

    public void CheckCurrentEnemiesCount()
    {
        if (_enemies.Count == 0)
        {
            MoveToNextPlatform();
        }
    }

    public void Init(int platformID, Enemy enemyPrefab, CharactorMoving moving)
    {
        _platformID = platformID;
        _enemyPrefab = enemyPrefab;
        _moving = moving;
        SpawnEnemies();
    }
}