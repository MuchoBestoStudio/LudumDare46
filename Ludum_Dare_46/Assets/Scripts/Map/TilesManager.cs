using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Map
{
	public class TilesManager : MonoBehaviour
	{
		public static TilesManager	Instance { get; private set; } = null;

		[SerializeField]
		private uint _row;
		[SerializeField]
		private uint _column;

		private Tile[,] _tilesArray;

		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(this);
				return;
			}

			Instance = this;
		}

		private void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}

		void Start()
		{
			CreateArena();
		}

		[ContextMenu("CreateArena")]
		private void CreateArena()
		{
			Tile[] tiles = GetComponentsInChildren<Tile>();
			Vector3 mapSize = GetTileSize(tiles);

			_column = (uint)(mapSize.x / Tile.SIZE);
			_row = (uint)(mapSize.z / Tile.SIZE);
			_tilesArray = new Tile[_column, _row];

			foreach (Tile tile in tiles)
			{
				Vector3 tilePosition = tile.transform.position;
				_tilesArray[(uint)(tilePosition.x / Tile.SIZE), (uint)(tilePosition.z / Tile.SIZE)] = tile;
			}
		}

		public Tile GetTile(int row, int column)
		{
			if (row < 0 || column < 0 ||
				row >= _row || column >= _column)
			{
				return null;
			}

			return _tilesArray[column, row];
		}

		public Tile GetTile(Vector3 position, Vector3 direction)
		{
			Vector3 destination = position + direction;

			return GetTile(Mathf.RoundToInt(destination.z / Tile.SIZE), Mathf.RoundToInt(destination.x / Tile.SIZE));
		}

		private Vector3 GetTileSize(Tile[] tileArray)
		{
			if (tileArray.Length == 0)
			{
				return Vector3.zero;
			}
			Vector3 size = Vector3.zero;

			foreach (Tile tile in tileArray)
			{
				if (tile.Center.x > size.x)
					size.x = tile.Center.x;
				if (tile.Center.z > size.z)
					size.z = tile.Center.z;
			}

			return size + new Vector3(Tile.SIZE, 0f, Tile.SIZE);
		}
	}
}