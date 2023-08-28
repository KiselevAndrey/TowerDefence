using CodeBase.Game.Map;
using UnityEngine;

namespace CodeBase.Infrastructure.Game
{
    [CreateAssetMenu(menuName = nameof(CodeBase) + "/" + nameof(Infrastructure)  + "/" + nameof(Game) + "/" + nameof(TileContentFactorySO))]
    public class TileContentFactorySO : GameObjectFactorySO
    {
        [SerializeField] private TileContent _emptyPrefab;
        [SerializeField] private TileContent _destinationPrefab;
        [SerializeField] private TileContent _wallPrefab;
        [SerializeField] private TileContent _spawnPointPrefab;
        [SerializeField] private TileContent _towerPrefab;

        public void Despawn(TileContent content) =>
            Lean.Pool.LeanPool.Despawn(content);

        public TileContent Spawn(TileType type)
        {
            return type switch
            {
                TileType.Empty => SpawnInPool(_emptyPrefab),
                TileType.Destination => SpawnInPool(_destinationPrefab),
                TileType.Wall => SpawnInPool(_wallPrefab),
                TileType.EnemySpawnPoint => SpawnInPool(_spawnPointPrefab),
                TileType.Tower => SpawnInPool(_towerPrefab),
                _ => null,
            };
        }

        private TileContent SpawnInPool(TileContent prefab)
        {
            var instance = SpawnGameObject(prefab);
            return instance;
        }
    }
}