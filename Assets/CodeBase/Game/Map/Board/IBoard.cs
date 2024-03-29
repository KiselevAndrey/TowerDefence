using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Map
{
    public interface IBoard : IBoardConstructor, IBoardSwitcher
    {
        public void Initialize(Vector2Int size, ProjectileGameBehaviour projectiles);
        public void GameUpdate();
    }

    public interface IBoardConstructor : IRestartable
    {
        public bool FindPaths();
        public ITile GetTile(Ray ray);
        public void ToggleEnemySpawnPoint(ITile tile);
    }

    public interface IBoardSwitcher : IRestartable
    {
        public List<ITile> EnemySpawnTiles { get; }
        public void ToggleDestination(ITile tile);
        public void ToggleWall(ITile tile);
        public void ToggleTower(ITile tile, TowerType towerType);
        public void ToggleEnemySpawnPoint(ITile tile);
    }
}