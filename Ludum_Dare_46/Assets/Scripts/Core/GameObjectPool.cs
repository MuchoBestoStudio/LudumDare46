using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace MuchoBestoStudio.LudumDare.Core
{
	public class GameObjectPool
	{
		#region Variables

		public Transform Parent { get; private set; } = null;

		public GameObject Prefab { get; private set; } = null;

		private List<GameObject> _used = new List<GameObject>();

		private List<GameObject> _unused = new List<GameObject>();

		#endregion

		#region Constructor

		private GameObjectPool() { }

		public GameObjectPool(GameObject prefab, Transform parent, uint amount)
		{
			Prefab = prefab;
			Parent = parent;

			Grow(amount);
		}

		#endregion

		#region Grow

		private void Grow(uint amount)
		{
			GameObject go = null;

			for (uint index = 0; index < amount; ++index)
			{
				go = MonoBehaviour.Instantiate(Prefab, Parent);

				Assert.IsNotNull(go, nameof(GameObjectPool) + ": Grow(), instantiated game object should not be null.");

				go.SetActive(false);

				_unused.Add(go);
			}
		}

		#endregion

		#region Use / Unused

		public GameObject Use()
		{
			if (_unused.Count == 0)
			{
				Grow((uint)_used.Count);
			}

			GameObject go = _unused[0];

			_used.Add(go);

			_unused.Remove(go);

			return go;
		}

		public void Unused(GameObject go)
		{
			Assert.IsNotNull(go, nameof(GameObjectPool) + ": Unused(), go should not be null.");

			if (!_used.Contains(go))
			{
				return;
			}

			go.SetActive(false);

			_used.Remove(go);
			_unused.Add(go);
		}

		#endregion
	}
}
