using UnityEngine;
using UnityEngine.Assertions;
using MuchoBestoStudio.LudumDare.Core;

namespace MuchoBestoStudio.LudumDare.Gameplay.Enemies
{
	public class EnemiesSpawner : MonoBehaviour
	{
		#region Variables

		[SerializeField, Tooltip("")]
		private	Transform	_parent = null;
		[SerializeField, Tooltip("")]
		private Map.Tile _target = null;
		[SerializeField, Tooltip("")]
		private	GameObject	_prefab = null;
		[SerializeField, Tooltip("")]
		private	uint		_initialAmount = 5;

		[SerializeField, Tooltip("")]
		private float duration = 3f;

		private float _time  = 0f;

		public	GameObjectPool	Pool { get; private set; } = null;

		#endregion

		#region MonoBehaviour's Methods

		private void Start()
		{
			Pool = new GameObjectPool(_prefab, _parent, _initialAmount);
		}

		private void Update()
		{
			_time -= Time.deltaTime;

			if (_time <= 0f)
			{
				_time += duration;

				Spawn(transform.position);
			}
		}

		#endregion

		#region Spawn

		public void Spawn(Vector3 position)
		{
			GameObject go = Pool.Use();

			go.transform.position = position;

			EnemyMovements movements = go.GetComponent<EnemyMovements>();
			movements.SetTarget(_target);

			Assert.IsNotNull(movements, nameof(EnemiesSpawner) + ": Spawn(), movements should not be null.");

			movements.onEnemyReachEnd += EnemyMovements_OnEnemyReachEnd;

			go.SetActive(true);
		}

		private void EnemyMovements_OnEnemyReachEnd(EnemyMovements movements)
		{
			movements.onEnemyReachEnd -= EnemyMovements_OnEnemyReachEnd;

			Pool.Unused(movements.gameObject);
		}

		#endregion
	}
}
