using UnityEngine;
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
		private	float		_initialTimer = 5f;
		[SerializeField, Tooltip("")]
		private float		_interval = 3f;

		public float Timer { get; private set; } = 0f;

		public	GameObjectPool	Pool { get; private set; } = null;

		#endregion

		#region MonoBehaviour's Methods

		private void Start()
		{
			Pool = new GameObjectPool(_prefab, _parent, _initialAmount);

			Timer = _initialTimer;
		}

		private void Update()
		{
			Timer -= Time.deltaTime;

			if (Timer <= 0f)
			{
				Timer += _interval;

				Spawn(transform.position);
			}
		}

		#endregion

		#region Spawn

		public void Spawn(Vector3 position)
		{
			Transform spawn = _spawns[Random.Range(0, _spawns.Length)];

			Assert.IsNotNull(spawn, nameof(EnemiesSpawner) + ": Spawn(), spawn should not be null.");

			GameObject go = Pool.Use();

			go.transform.position = spawn.position;

			EnemyMovements movements = go.GetComponent<EnemyMovements>();

			Assert.IsNotNull(movements, nameof(EnemiesSpawner) + ": Spawn(), movements should not be null.");

			movements.SetTarget(_target);

			EnemyAnimator animator = go.GetComponent<EnemyAnimator>();

			Assert.IsNotNull(animator, nameof(EnemiesSpawner) + ": Spawn(), animator should not be null.");

			animator.onEnemyEndDisappearing += EnemyAnimator_OnEnemyDisappear;

			animator.Appear();
		}

		private void EnemyAnimator_OnEnemyDisappear(EnemyAnimator animator)
		{
			animator.onEnemyEndDisappearing -= EnemyAnimator_OnEnemyDisappear;

			Pool.Unused(animator.gameObject);
		}

		#endregion
	}
}
