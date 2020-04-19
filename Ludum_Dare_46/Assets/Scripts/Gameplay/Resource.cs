using UnityEngine;
using System;

using MuchoBestoStudio.LudumDare.Gameplay;
using MuchoBestoStudio.LudumDare.Gameplay._3C;

public class Resource : MonoBehaviour, IInteractable
{
    [SerializeField]
    private uint _combustibleAmount = 0;
    public uint CombustibleAmount => _combustibleAmount;

    [Header("Visual")]
    [SerializeField]
    private GameObject _basicVisual = null;
    [SerializeField]
    private GameObject _emptyVisual = null;

    public Action<uint> onCombustibleAmountChanged = null;

    private void Awake()
    {
        if (_basicVisual == null)
        {
            Debug.LogError("No \"_basicVisual\" set for the resource : " + gameObject.name);
        }
    }

    public void SetCombustibleAmount(uint value)
    {
        _combustibleAmount = value;
        if (value > 1)
        {
            _basicVisual.SetActive(true);
        }
    }

    public void AddCombustible(uint value) { SetCombustibleAmount(_combustibleAmount + value); }
    public void RemoveCombustible(uint value)
    {
        if (_combustibleAmount - value <= _combustibleAmount)
        {
            SetCombustibleAmount(_combustibleAmount - value);
            onCombustibleAmountChanged?.Invoke(_combustibleAmount);
            if (_combustibleAmount == 0)
            {
                if( _emptyVisual != null)
                {
                    _emptyVisual.SetActive(true);
                    _basicVisual.SetActive(false);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void TakeCombustible()
    {
        if (_combustibleAmount > 0)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            if (inventory && inventory.CanAddCombustible(1))
            {
                RemoveCombustible(1);
                inventory.AddCombustible(1);
            }
        }
    }

    public void Interact(ECharacter character)
    {
		if (character == ECharacter.PLAYER)
		{
			TakeCombustible();
		}
    }
}
