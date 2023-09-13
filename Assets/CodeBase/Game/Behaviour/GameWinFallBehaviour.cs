﻿using System;
using UnityEngine;

namespace CodeBase.Game
{
    [Serializable ]
    public class GameWinFallBehaviour : IRestartable
    {
        [SerializeField, Range(1, 10)] private int _maxLives;

        private int _currentLives;

        public  event Action EndGame;

        public void OnEnable(EnemySpawnBehaviour enemySpawn)
        {
            enemySpawn.EnemiesAreOver += Win;
        }

        public void OnDisable(EnemySpawnBehaviour enemySpawn)
        {
            enemySpawn.EnemiesAreOver -= Win;
        }

        public void TakeDamage(int damage)
        {
            _currentLives -= damage;

            if (_currentLives <= 0)
                Fall();
        }

        public void Restart()
        {
            _currentLives = _maxLives;
        }

        public void Win()
        {
            Debug.Log("Win");
            EndGame?.Invoke();
        }

        private void Fall()
        {
            Debug.Log("Fall");
            EndGame?.Invoke();
        }
    }
}