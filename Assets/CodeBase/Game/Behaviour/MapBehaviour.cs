using CodeBase.Game.Map;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game
{
    [System.Serializable]
    public class MapBehaviour : IRestartable
    {
        [SerializeField] private Board _board;
        [Space]
        [SerializeField] private Vector2Int _boardSize;

        private Camera _camera;
        private TowerType _towerType;

        private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

        public List<ITile> EnemySpawnTiles => _board.EnemySpawnTiles;

        public void Awake()
        {
            _camera = Camera.main;
        }

        public void Start(ProjectileGameBehaviour projectiles)
        {
            _board.Initialize(_boardSize, projectiles);
            _board.FindPaths();
        }

        public void Restart()
        {
            _board.Restart();
        }

        public void PlacementUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _towerType = TowerType.Laser;
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                _towerType = TowerType.Mortar;

            if (Input.GetMouseButtonUp(0))
                HandleTouch();
            else if (Input.GetMouseButtonUp(1))
                HandleAlternativeTouch();
        }

        public void GameUpdate()
        {
            _board.GameUpdate();
        }

        private void HandleTouch()
        {
            var tile = _board.GetTile(TouchRay);
            if (tile != null)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    _board.ToggleWall(tile);
                else
                    _board.ToggleTower(tile, _towerType);
            }
        }

        private void HandleAlternativeTouch()
        {
            var tile = _board.GetTile(TouchRay);
            if (tile != null)
            {
                if(Input.GetKey(KeyCode.LeftShift))
                    _board.ToggleDestination(tile);
                else
                    _board.ToggleEnemySpawnPoint(tile);
            }
        }
    }
}