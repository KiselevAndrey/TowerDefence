using CodeBase.Game.Map;
using CodeBase.Infrastructure.Game;
using CodeBase.Utility.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game
{
    [System.Serializable]
    public class EnemySpawnBehaviour
    {
        [SerializeField] private EnemyFactorySO _enemyFactory;
        [SerializeField, Range(0.1f, 5f)] private float _enemySpawnSpeed;

        private List<ITile> _spawnTiles;

        private float _spawnProgress;

        public void Init(List<ITile> spawnTiles)
        {
            _spawnTiles = spawnTiles;
        }

        public void OnUpdate()
        {
            _spawnProgress += Time.deltaTime * _enemySpawnSpeed;
            while(_spawnProgress >= 1f)
            {
                _spawnProgress -= 1f;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            if (_spawnTiles.Count == 0)
                return;

            var spawnTile = _spawnTiles.Random();
            var enemy = _enemyFactory.Spawn();
            enemy.SpawnOn(spawnTile);
        }
    }
}