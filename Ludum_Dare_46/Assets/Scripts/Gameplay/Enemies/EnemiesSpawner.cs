﻿using UnityEngine;
using UnityEngine.Assertions;
using MuchoBestoStudio.LudumDare.Core;
using MuchoBestoStudio.LudumDare.Map;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	public class EnemiesSpawner : MonoBehaviour
	{
		#region Variables

		[Header("Pool' settings")]
		[SerializeField, Tooltip("")]
		private	Transform	_parent = null;
		[SerializeField, Tooltip("")]
		private	GameObject	_prefab = null;
		[SerializeField, Tooltip("")]
		private	uint		_initialAmount = 4;

		[Header("Spawn settings")]
		[SerializeField, Tooltip("")]
		private	Transform[]	_spawns = new Transform[0];
		[SerializeField, Tooltip("")]
		private Tile		_target = null;
		[SerializeField, Tooltip("")]
		private	int _initialTick = 5;
		[SerializeField, Tooltip("")]
		private int		_interval = 5;

		public int Timer { get; private set; } = 0;

		public	GameObjectPool	Pool { get; private set; } = null;

        private Transform _spawnTransform = null;
        private GameObject _spawnedObject = null;
        private EnemyMovements _spawnedMovements = null;
        private EnemyAnimator _spawnedAnimator = null;

        #endregion

        #region MonoBehaviour's Methods

        private void Start()
		{
			Pool = new GameObjectPool(_prefab, _parent, _initialAmount);

			GameManager.Instance.onGameOver += OnGameOver;
			GameManager.Instance.onTimeUpdated += OnTickUpdate;

			Timer = _initialTick;
		}

		private void OnDestroy()
		{
			GameManager.Instance.onTimeUpdated -= OnTickUpdate;
			GameManager.Instance.onGameOver -= OnGameOver;
		}

		private void OnTickUpdate()
		{
			--Timer;

			if (Timer == 0)
			{
				Timer += _interval;

				Spawn(transform.position);
			}
		}

		#endregion

		#region Spawn

		private void OnGameOver()
		{
			enabled = false;

			Pool.Unused();
		}

		public void Spawn(Vector3 position)
		{
            _spawnTransform = _spawns[Random.Range(0, _spawns.Length)];

			Assert.IsNotNull(_spawnTransform, nameof(EnemiesSpawner) + ": Spawn(), spawn should not be null.");

            _spawnedObject = Pool.Use();

            _spawnedObject.transform.position = _spawnTransform.position;

			_spawnedMovements = _spawnedObject.GetComponent<EnemyMovements>();

			Assert.IsNotNull(_spawnedMovements, nameof(EnemiesSpawner) + ": Spawn(), movements should not be null.");

            _spawnedMovements.SetTarget(_target);

            _spawnedAnimator = _spawnedObject.GetComponent<EnemyAnimator>();

			Assert.IsNotNull(_spawnedAnimator, nameof(EnemiesSpawner) + ": Spawn(), animator should not be null.");

            _spawnedAnimator.onEnemyEndDisappearing += EnemyAnimator_OnEnemyDisappear;

            _spawnedAnimator.Appear();
		}

		private void EnemyAnimator_OnEnemyDisappear(EnemyAnimator animator)
		{
			animator.onEnemyEndDisappearing -= EnemyAnimator_OnEnemyDisappear;

			Pool.Unused(animator.gameObject);
		}

		#endregion
	}
}
