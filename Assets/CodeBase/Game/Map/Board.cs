using System;
using System.Collections.Generic;
using UnityEngine;

using static Lean.Pool.LeanPool;

namespace CodeBase.Game.Map
{
    [System.Serializable]
    public class Board
    {
        [SerializeField] private Transform _ground;
        [SerializeField] private Transform _tileParent;
        [SerializeField] private Tile _tilePrefab;

        private Tile[] _tiles;
        private Queue<Tile> _serchFrontier = new();

        public void Initialize(Vector2Int size)
        {
            _ground.localScale = new(size.x, size.y, 1f);

            _tiles = new Tile[size.x * size.y];

            var offset = new Vector2((size.x - 1) * 0.5f, (size.y - 1) * 0.5f);
            for (int i = 0, y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++, i++)
                {
                    var tile = _tiles[i] = Spawn(_tilePrefab);
                    tile.transform.SetParent(_tileParent, false);
                    tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);

                    if(x > 0)
                        Tile.MakeWeastEastNeibours(tile, _tiles[i - 1]);
                    if (y > 0)
                        Tile.MakeNorthSouthNeibours(tile, _tiles[i - size.x]);

                    tile.IsAlternative = (x & 1) == 0;
                    if((y & 1) == 0)
                        tile.IsAlternative = !tile.IsAlternative;
                }
            }
        }

        public void FindPathToRandomPoint()
        {
            FindPath(UnityEngine.Random.Range(0, _tiles.Length));
        }

        public void FindPathToCenter()
        {
            FindPath(_tiles.Length / 2);
        }

        private void FindPath(int destinationIndex)
        {
            foreach (var tile in _tiles)
                tile.ClearPath();

            _tiles[destinationIndex].BecameDestination();
            _serchFrontier.Enqueue(_tiles[destinationIndex]);

            while(_serchFrontier.Count > 0)
            {
                var tile = _serchFrontier.Dequeue();
                if(tile != null)
                {
                    if (tile.IsAlternative)
                    {
                        _serchFrontier.Enqueue(tile.GrowPathNorth());
                        _serchFrontier.Enqueue(tile.GrowPathSouth());
                        _serchFrontier.Enqueue(tile.GrowPathEast());
                        _serchFrontier.Enqueue(tile.GrowPathWest());
                    }
                    else
                    {
                        _serchFrontier.Enqueue(tile.GrowPathWest());
                        _serchFrontier.Enqueue(tile.GrowPathEast());
                        _serchFrontier.Enqueue(tile.GrowPathSouth());
                        _serchFrontier.Enqueue(tile.GrowPathNorth());
                    }
                }
            }

            foreach (var tile in _tiles)
                tile.ShowPath();
        }
    }
}